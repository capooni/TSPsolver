using System;
using System.Windows.Forms;

namespace BranchAndBound
{
    public partial class MatrixForm : Form
    {
        private Graph _graph;

        public MatrixForm(Graph graph)
        {
            InitializeComponent();
            _graph = graph;
        }

        public Graph Graph
        {
            get => default;
            set
            {
            }
        }

        private void MatrixForm_Load(object sender, EventArgs e)
        {
            int n = _graph.VertexCount;
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = n;

            for (int i = 0; i < n; i++)
            {
                dataGridView1.Columns[i].HeaderText = (i + 1).ToString();
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView1.Columns[i].Width = 50;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1[j, i].Value = _graph.AdjMatrix[i, j] == _graph.GetINF() ? "" : _graph.AdjMatrix[i, j].ToString();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int n = _graph.VertexCount;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;

                        string cellVal = dataGridView1[j, i].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(cellVal))
                        {
                            _graph.AdjMatrix[i, j] = _graph.GetINF();
                        }
                        else if (!int.TryParse(cellVal, out int weight) || weight < 0)
                        {
                            throw new Exception($"Некорректное значение в ячейке ({i + 1}, {j + 1}). Должно быть неотрицательным целым числом.");
                        }
                        else
                        {
                            _graph.AdjMatrix[i, j] = weight;
                        }
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму без сохранения
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
