using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace алгоритм_левита
{
    internal class Program
    {

        class Graph
        {
            public int V; 
            public List<List<(int, int)>> AdjList; 

            public Graph(int V)
            {
                this.V = V;
                AdjList = new List<List<(int, int)>>(V);
                for (int i = 0; i < V; i++)
                {
                    AdjList.Add(new List<(int, int)>());
                }
            }

            public void AddEdge(int u, int v, int weight)
            {
                AdjList[u].Add((v, weight));
            }
        }

        class LevitAlgorithm
        {
            public static (int[], int[]) LevitShortestPath(Graph graph, int source)
            {
                int V = graph.V;
                int[] dist = new int[V];
                int[] prev = new int[V];
                for (int i = 0; i < V; i++)
                {
                    dist[i] = int.MaxValue;
                    prev[i] = -1;
                }
                dist[source] = 0;

                HashSet<int> M0 = new HashSet<int>();
                Queue<int> M1 = new Queue<int>();
                Queue<int> M2 = new Queue<int>();

                for (int i = 0; i < V; i++)
                {
                    M2.Enqueue(i);
                }

                M1.Enqueue(source);
                M2.Dequeue();

                while (M2.Count > 0)
                {
                    int u = M1.Dequeue();
                    foreach (var (v, weight) in graph.AdjList[u])
                    {
                        if (M2.Contains(v) && dist[u] + weight < dist[v])
                        {
                            dist[v] = dist[u] + weight;
                            prev[v] = u;
                            M1.Enqueue(v);
                            M2.Dequeue();
                        }
                        else if (M1.Contains(v) && dist[u] + weight < dist[v])
                        {
                            dist[v] = dist[u] + weight;
                            prev[v] = u;
                        }
                        else if (M0.Contains(v) && dist[u] + weight < dist[v])
                        {
                            dist[v] = dist[u] + weight;
                            prev[v] = u;
                            M1.Enqueue(v);
                            M0.Remove(v);
                        }
                    }
                    M0.Add(u);
                }

                return (dist, prev);
            }
        }

        static void Main(string[] args)
        {
            // Пример графа
            int V = 4;
            Graph graph = new Graph(V);
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 4);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 7);
            graph.AddEdge(2, 3, 3);

            int source = 0;
            var (dist, prev) = LevitAlgorithm.LevitShortestPath(graph, source);

            Console.WriteLine("Расстояния:");
            for (int i = 0; i < V; i++)
            {
                
                if (dist[i] == int.MaxValue) { Console.WriteLine($"От {source} до {i}: нет пути"); }
                else { Console.WriteLine($"От {source} до {i}: {dist[i]}"); }
            }

            
        }
    }
}
