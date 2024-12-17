using System;

namespace BranchAndBound
{
    public class Graph
    {
        private const int INF = 999999;
        public int[,] AdjMatrix { get; private set; }
        public int VertexCount { get; private set; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            AdjMatrix = new int[vertexCount, vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    AdjMatrix[i, j] = (i == j) ? 0 : INF;
                }
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            if (from < 0 || from >= VertexCount || to < 0 || to >= VertexCount || from == to)
                throw new ArgumentException("Недопустимые параметры для добавления ребра.");
            AdjMatrix[from, to] = weight;
            AdjMatrix[to, from] = weight;
        }

        public void RemoveVertex(int v)
        {
            int n = VertexCount - 1;
            int[,] newMatrix = new int[n, n];
            int r = 0, c = 0;
            for (int i = 0; i < VertexCount; i++)
            {
                if (i == v) continue;
                c = 0;
                for (int j = 0; j < VertexCount; j++)
                {
                    if (j == v) continue;
                    newMatrix[r, c] = AdjMatrix[i, j];
                    c++;
                }
                r++;
            }

            AdjMatrix = newMatrix;
            VertexCount = n;
        }

        public int GetINF()
        {
            return INF;
        }
    }
}
