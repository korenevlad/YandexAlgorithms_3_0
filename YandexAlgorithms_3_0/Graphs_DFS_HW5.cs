using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexAlgorithms_3_0
{
    public class Graphs_DFS_HW5
    {
        //Задача 1
        //Поиск в глубину

        //Формат ввода
        //В первой строке записаны два целых числа N(1 ≤ N ≤ 103) и M(0 ≤ M ≤ 5 * 105) —
        //количество вершин и ребер в графе.В последующих M строках перечислены ребра — пары чисел,
        //определяющие номера вершин, которые соединяют ребра.

        //Формат вывода
        //В первую строку выходного файла выведите число K — количество вершин в компоненте связности.
        //Во вторую строку выведите K целых чисел — вершины компоненты связности,
        //перечисленные в порядке возрастания номеров.
        public static void  A_Solution_DFS(string a)
        {
            int[] nm = a.Split(' ').Select(int.Parse).ToArray();
            int n = nm[0];
            int m = nm[1];
            List<List<int>> adjacencyList = new List<List<int>>();  //создали список смежности
            List<bool> visited = new List<bool>(); //список, отображающий посещённые вершины
            for (int i = 0; i <= n; i++)
            {
                adjacencyList.Add(new List<int>());     //заполнили списками список смежности
                visited.Add(false);
            }
            for (int i = 0; i < m; i++) //заполнение матрицы смежности
            {
                string edge = Console.ReadLine();
                int[] vertexes = edge.Split(' ').Select(int.Parse).ToArray();
                int vertex1 = vertexes[0];
                int vertex2 = vertexes[1];
                adjacencyList[vertex1].Add(vertex2);
                adjacencyList[vertex2].Add(vertex1);
            }

            int count = 0;  //количество вершин в компоненте связности
            List<int> result = new List<int>(); //список вершин компоненты связности
            DFS(adjacencyList, visited, 1, ref count, result);  //вызов DFS
            result.Sort();  //сортировка
            Console.WriteLine(count);
            for(int i=0; i < result.Count; i++)
            {
                Console.Write(result[i]);
                if (i != result.Count - 1)
                {
                    Console.Write(" ");
                }
            }

            void DFS(List<List<int>> adjacencyList, List<bool> visited, int now, ref int count, List<int> result)    //описание функции DFS
            {
                visited[now] = true;
                count+= 1;
                result.Add(now);
                foreach(var neigh in adjacencyList[now])
                {
                    if (!visited[neigh])
                    {
                        DFS(adjacencyList,visited,neigh, ref count, result);
                    }
                }
            }
        }


        public static void Main(string[] args)
        {
            A_Solution_DFS(Console.ReadLine());
        }
    }
}
