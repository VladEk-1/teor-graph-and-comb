using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace объединить_найти
{

    public class UnionFind
    {
        private int[] parent;  // Массив родителей
        private int[] rank;    // Массив рангов

        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)  // Цикл для инициализации массивов
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);  // Сжатие пути
            }
            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int size = 100000000;  // Большой объем данных
            UnionFind uf = new UnionFind(size);

            // Пример объединения множеств
            for (int i = 100; i < size - 1; i++)
            {
                uf.Union(i, i + 1);
            }

            // Проверка принадлежности элементов к одному множеству
            Console.WriteLine(uf.Find(0) == uf.Find(size - 1));
            Console.WriteLine(uf.Find(12345) == uf.Find(size - 1));
            Console.WriteLine(uf.Find(1000));
        }
    }
}
