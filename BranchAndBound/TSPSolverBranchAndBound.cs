using System;
using System.Collections.Generic;
using System.Linq;

namespace BranchAndBound
{
    public class TSPSolverBranchAndBound
    {
        private readonly Graph _graph;
        private readonly int _n;
        private readonly int _INF;

        public int[] BestPath { get; private set; }
        public int BestCost { get; private set; }

        public Graph Graph
        {
            get => default;
            set
            {
            }
        }

        public TSPSolverBranchAndBound(Graph graph)
        {
            _graph = graph;
            _n = graph.VertexCount;
            _INF = graph.GetINF();
        }

        public void Solve()
        {
            BestCost = _INF;
            BestPath = null;

            bool[] visited = new bool[_n];
            visited[0] = true; // Начинаем с вершины 0
            List<int> currentPath = new List<int> { 0 };

            BranchAndBound(0, 1, 0, visited, currentPath);
        }

        private void BranchAndBound(int currentVertex, int visitedCount, int currentCost, bool[] visited, List<int> path)
        {
            // Если все вершины посещены и есть путь обратно в начальную вершину
            if (visitedCount == _n && _graph.AdjMatrix[currentVertex, 0] != _INF)
            {
                int totalCost = currentCost + _graph.AdjMatrix[currentVertex, 0];
                if (totalCost < BestCost)
                {
                    BestCost = totalCost;
                    BestPath = path.ToArray();
                }
                return;
            }

            // Вычисляем нижнюю границу для текущего состояния
            int lowerBound = currentCost + CalculateLowerBound(currentVertex, visited);
            if (lowerBound >= BestCost)
            {
                // Отсекаем эту ветвь, так как она не может привести к лучшему решению
                return;
            }

            // Получаем список всех непосещённых вершин, соединённых с текущей
            List<int> candidates = new List<int>();
            for (int v = 0; v < _n; v++)
            {
                if (!visited[v] && _graph.AdjMatrix[currentVertex, v] != _INF)
                {
                    candidates.Add(v);
                }
            }

            // Сортируем кандидатов по возрастанию веса ребра для улучшения эффективности отсечения
            candidates = candidates.OrderBy(v => _graph.AdjMatrix[currentVertex, v]).ToList();

            foreach (int nextVertex in candidates)
            {
                // Вычисляем потенциальную новую стоимость маршрута
                int newCost = currentCost + _graph.AdjMatrix[currentVertex, nextVertex];

                // Если потенциальная стоимость уже превышает текущий лучший результат, пропускаем
                if (newCost >= BestCost)
                {
                    continue;
                }

                // Помечаем вершину как посещённую и добавляем её в текущий путь
                visited[nextVertex] = true;
                path.Add(nextVertex);

                // Рекурсивно продолжаем поиск
                BranchAndBound(nextVertex, visitedCount + 1, newCost, visited, path);

                // Отмена изменений для следующей итерации
                path.RemoveAt(path.Count - 1);
                visited[nextVertex] = false;
            }
        }

        /// <summary>
        /// Вычисляет нижнюю границу стоимости маршрута для текущего состояния.
        /// </summary>
        /// <param name="currentVertex">Текущая вершина.</param>
        /// <param name="visited">Массив посещённых вершин.</param>
        /// <returns>Нижняя граница стоимости.</returns>
        private int CalculateLowerBound(int currentVertex, bool[] visited)
        {
            int lowerBound = 0;

            // Для каждой непосещённой вершины добавляем минимальный вес исходящего из неё ребра
            for (int v = 0; v < _n; v++)
            {
                if (!visited[v])
                {
                    int minEdge = _INF;
                    for (int u = 0; u < _n; u++)
                    {
                        if (u != v && _graph.AdjMatrix[v, u] < minEdge)
                        {
                            minEdge = _graph.AdjMatrix[v, u];
                        }
                    }
                    lowerBound += minEdge;
                }
            }

            // Добавляем минимальный вес из текущей вершины
            int minFromCurrent = _INF;
            for (int v = 0; v < _n; v++)
            {
                if (!_graph.AdjMatrix[currentVertex, v].Equals(_INF) && _graph.AdjMatrix[currentVertex, v] < minFromCurrent)
                {
                    minFromCurrent = _graph.AdjMatrix[currentVertex, v];
                }
            }
            lowerBound += minFromCurrent;

            // Добавляем минимальный вес обратно в начальную вершину
            int minToStart = _INF;
            for (int v = 0; v < _n; v++)
            {
                if (!_graph.AdjMatrix[v, 0].Equals(_INF) && _graph.AdjMatrix[v, 0] < minToStart)
                {
                    minToStart = _graph.AdjMatrix[v, 0];
                }
            }
            lowerBound += minToStart;

            return lowerBound;
        }
    }
}
