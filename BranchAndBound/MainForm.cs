using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BranchAndBound
{
    public partial class MainForm : Form
    {
        private Graph graph;
        private string lastResult; // Хранение последнего результата для сохранения

        public MainForm()
        {
            InitializeComponent();
        }

        public Graph Graph
        {
            get => default;
            set
            {
            }
        }

        public TSPSolverBranchAndBound TSPSolverBranchAndBound
        {
            get => default;
            set
            {
            }
        }

        public MatrixForm MatrixForm
        {
            get => default;
            set
            {
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            graph = new Graph(4);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(1, 2, 15);
            graph.AddEdge(2, 3, 20);
            graph.AddEdge(3, 0, 25);

            this.BackColor = Color.MintCream;
            CenterTitleLabel();
        }

        private void CenterTitleLabel()
        {
            labelTitle.Left = (this.ClientSize.Width - labelTitle.Width) / 2;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterTitleLabel();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (graph == null || graph.VertexCount == 0) return;

            int R = 20;
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int radius = Math.Min(centerX, centerY) - 100;

            // Позиции вершин
            PointF[] vertexPositions = new PointF[graph.VertexCount];
            for (int i = 0; i < graph.VertexCount; i++)
            {
                float angle = (float)(2 * Math.PI * i / graph.VertexCount);
                float x = centerX + radius * (float)Math.Cos(angle);
                float y = centerY + radius * (float)Math.Sin(angle);
                vertexPositions[i] = new PointF(x, y);
            }

            // Рисуем рёбра и веса
            using (Pen edgePen = new Pen(Color.DarkBlue, 2))
            using (Font edgeFont = new Font("Arial", 10))
            using (Brush edgeBrush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < graph.VertexCount; i++)
                {
                    for (int j = i + 1; j < graph.VertexCount; j++)
                    {
                        int weight = graph.AdjMatrix[i, j];
                        if (weight != graph.GetINF())
                        {
                            e.Graphics.DrawLine(edgePen, vertexPositions[i], vertexPositions[j]);

                            // Координаты для подписи веса ребра (середина линии)
                            float midX = (vertexPositions[i].X + vertexPositions[j].X) / 2f;
                            float midY = (vertexPositions[i].Y + vertexPositions[j].Y) / 2f;

                            // Вектор ребра
                            float dx = vertexPositions[j].X - vertexPositions[i].X;
                            float dy = vertexPositions[j].Y - vertexPositions[i].Y;

                            // Находим перпендикуляр (-dy, dx)
                            float px = -dy;
                            float py = dx;

                            // Нормируем перпендикуляр
                            float length = (float)Math.Sqrt(px * px + py * py);
                            if (length > 0)
                            {
                                px /= length;
                                py /= length;
                            }

                            // Смещаем подпись на 8 пикселей перпендикулярно, чтобы избежать наложений
                            float offset = 8f;
                            midX += px * offset;
                            midY += py * offset;

                            e.Graphics.DrawString(weight.ToString(), edgeFont, edgeBrush, midX, midY);
                        }
                    }
                }
            }

            // Рисуем вершины
            using (Brush vertexBrush = new SolidBrush(Color.LightYellow))
            using (Pen vertexPen = new Pen(Color.Black, 2))
            using (Font vertexFont = new Font("Arial", 12))
            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < graph.VertexCount; i++)
                {
                    e.Graphics.FillEllipse(vertexBrush, vertexPositions[i].X - R, vertexPositions[i].Y - R, 2 * R, 2 * R);
                    e.Graphics.DrawEllipse(vertexPen, vertexPositions[i].X - R, vertexPositions[i].Y - R, 2 * R, 2 * R);
                    e.Graphics.DrawString((i + 1).ToString(), vertexFont, textBrush, vertexPositions[i].X - 10, vertexPositions[i].Y - 10);
                }
            }

            // Если есть лучший путь, выделяем его
            if (lastResult != null)
            {
                try
                {
                    string[] parts = lastResult.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    string pathLine = parts.FirstOrDefault(p => p.StartsWith("Кратчайший путь"));
                    if (!string.IsNullOrEmpty(pathLine))
                    {
                        string pathStr = pathLine.Split(':')[1].Trim();
                        string[] path = pathStr.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < path.Length - 1; i++)
                        {
                            if (int.TryParse(path[i], out int from) && int.TryParse(path[i + 1], out int to))
                            {
                                from -= 1; // Преобразование обратно к индексам
                                to -= 1;
                                if (from >= 0 && from < graph.VertexCount && to >= 0 && to < graph.VertexCount)
                                {
                                    e.Graphics.DrawLine(new Pen(Color.Red, 3), vertexPositions[from], vertexPositions[to]);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Игнорируем любые ошибки при рисовании лучшего пути
                }
            }
        }

        private async void buttonSolve_Click(object sender, EventArgs e)
        {
            try
            {
                if (graph.VertexCount <= 1)
                {
                    throw new Exception("Недостаточно вершин для решения задачи.");
                }

                // Создаём экземпляр решателя
                var solver = new TSPSolverBranchAndBound(graph);

                // Создаём и запускаем Stopwatch
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Отображаем индикатор прогресса
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.Visible = true;

                // Запускаем решение задачи асинхронно
                await Task.Run(() => solver.Solve());

                // Останавливаем Stopwatch
                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                // Скрываем индикатор прогресса
                progressBar.Visible = false;

                // Проверяем, найдено ли решение
                if (solver.BestPath != null)
                {
                    string pathStr = string.Join("->", solver.BestPath.Select(x => (x + 1).ToString())) + "->" + (solver.BestPath[0] + 1);
                    string result = $"Кратчайший путь Коммивояжёра: {pathStr}{Environment.NewLine}Вес: {solver.BestCost}{Environment.NewLine}Время выполнения: {elapsedMilliseconds} мс";
                    MessageBox.Show(result, "Результат");
                    lastResult = result; // Сохраняем результат для сохранения

                    // Записываем данные в файл или таблицу для анализа
                    LogExecutionTime(graph.VertexCount, elapsedMilliseconds);
                }
                else
                {
                    throw new Exception("Решение не найдено. Возможно, граф не полный или отсутствуют необходимые рёбра.");
                }

                // Перерисовываем граф для отображения лучшего пути
                Invalidate();
            }
            catch (Exception ex)
            {
                // Скрываем индикатор прогресса в случае ошибки
                progressBar.Visible = false;
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для записи времени выполнения
        private void LogExecutionTime(int numberOfCities, long elapsedMilliseconds)
        {
            // Здесь вы можете реализовать сохранение данных, например, в файл или базу данных
            // Для простоты будем сохранять в текстовый файл в формате CSV
            string logLine = $"{numberOfCities},{elapsedMilliseconds}";
            string logFilePath = "execution_times.csv";

            try
            {
                // Проверяем, существует ли файл
                if (!File.Exists(logFilePath))
                {
                    // Если нет, создаём заголовок
                    File.WriteAllText(logFilePath, "NumberOfCities,ElapsedMilliseconds\n");
                }

                // Добавляем строку с данными
                File.AppendAllText(logFilePath, logLine + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи логов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddEdge_Click(object sender, EventArgs e)
        {
            try
            {
                string fromStr = InputDialog.ShowDialog("Введите номер начальной вершины (с 1):", "Добавить ребро", "1");
                if (string.IsNullOrWhiteSpace(fromStr)) throw new Exception("Номер начальной вершины не может быть пустым.");
                if (!int.TryParse(fromStr, out int from) || from <= 0) throw new Exception("Номер начальной вершины должен быть положительным числом.");
                from--;

                string toStr = InputDialog.ShowDialog("Введите номер конечной вершины (с 1):", "Добавить ребро", "2");
                if (string.IsNullOrWhiteSpace(toStr)) throw new Exception("Номер конечной вершины не может быть пустым.");
                if (!int.TryParse(toStr, out int to) || to <= 0) throw new Exception("Номер конечной вершины должен быть положительным числом.");
                to--;

                if (from < 0 || from >= graph.VertexCount || to < 0 || to >= graph.VertexCount)
                    throw new Exception("Одна из указанных вершин не существует.");

                string weightStr = InputDialog.ShowDialog("Введите вес ребра (0 для удаления):", "Добавить ребро", "10");
                if (string.IsNullOrWhiteSpace(weightStr)) throw new Exception("Вес ребра не может быть пустым.");
                if (!int.TryParse(weightStr, out int weight) || weight < 0) throw new Exception("Вес ребра должен быть неотрицательным числом.");

                if (weight == 0) weight = graph.GetINF();

                graph.AddEdge(from, to, weight);
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonAddVertex_Click(object sender, EventArgs e)
        {
            try
            {
                int newCount = graph.VertexCount + 1;
                var newGraph = new Graph(newCount);

                for (int i = 0; i < graph.VertexCount; i++)
                    for (int j = 0; j < graph.VertexCount; j++)
                        newGraph.AdjMatrix[i, j] = graph.AdjMatrix[i, j];

                graph = newGraph;
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении вершины: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveVertex_Click(object sender, EventArgs e)
        {
            try
            {
                string vertexStr = InputDialog.ShowDialog("Введите номер вершины для удаления (с 1):", "Удалить вершину", "1");
                if (string.IsNullOrWhiteSpace(vertexStr)) throw new Exception("Номер вершины не может быть пустым.");
                if (!int.TryParse(vertexStr, out int vertex) || vertex <= 0) throw new Exception("Номер вершины должен быть положительным числом.");
                vertex--;

                if (vertex >= 0 && vertex < graph.VertexCount)
                {
                    graph.RemoveVertex(vertex);
                    Invalidate();
                }
                else
                {
                    throw new Exception("Неверный номер вершины.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonShowMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                string matrixStr = "Матрица смежности:\n\n";
                matrixStr += "   ";

                for (int i = 0; i < graph.VertexCount; i++)
                    matrixStr += $"{i + 1,4}";

                matrixStr += "\n";

                for (int i = 0; i < graph.VertexCount; i++)
                {
                    matrixStr += $"{i + 1,2} ";
                    for (int j = 0; j < graph.VertexCount; j++)
                    {
                        int value = graph.AdjMatrix[i, j];
                        matrixStr += value == graph.GetINF() ? "  ∞ " : $"{value,4}";
                    }
                    matrixStr += "\n";
                }

                MessageBox.Show(matrixStr, "Матрица смежности");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка отображения матрицы", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSetMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                var matrixForm = new MatrixForm(graph);
                if (matrixForm.ShowDialog() == DialogResult.OK)
                {
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Выберите файл с данными"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);

                    if (lines.Length == 0)
                        throw new FormatException("Файл пустой или некорректный.");

                    // Первая строка — количество вершин
                    if (!int.TryParse(lines[0], out int vertexCount) || vertexCount <= 0)
                        throw new FormatException("Некорректное число вершин.");

                    graph = new Graph(vertexCount);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string line = lines[i].Trim();
                        if (string.IsNullOrWhiteSpace(line))
                            continue; // пропускаем пустые строки

                        string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 3)
                            throw new FormatException("Некорректный формат данных в строке: " + line);

                        if (!int.TryParse(parts[0], out int from) ||
                            !int.TryParse(parts[1], out int to) ||
                            !int.TryParse(parts[2], out int weight))
                            throw new FormatException("Некорректный формат числа в строке: " + line);

                        from--;
                        to--;

                        if (from < 0 || from >= vertexCount || to < 0 || to >= vertexCount)
                            throw new FormatException("Указаны вершины вне диапазона в строке: " + line);

                        graph.AddEdge(from, to, weight);
                    }

                    MessageBox.Show("Данные успешно загружены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastResult))
            {
                MessageBox.Show("Нет данных для сохранения. Пожалуйста, выполните решение задачи сначала.", "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить результаты"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, lastResult);
                    MessageBox.Show("Результаты успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
