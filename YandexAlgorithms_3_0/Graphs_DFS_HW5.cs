using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        //ЗАДАЧА НА ВАЯСЕНИЕ СВЯЗНОСТИ НЕОРИЕНТИРОВАННОГО ГРАФА
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

            //выводы в консоль
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


        //Дан неориентированный невзвешенный граф, состоящий из N вершин и M ребер.
        //Необходимо посчитать количество его компонент связности и вывести их.

        //Формат ввода
        //Во входном файле записано два числа N и M(0 < N ≤ 100000, 0 ≤ M ≤ 100000).
        //В следующих M строках записаны по два числа i и j(1 ≤ i, j ≤ N), которые означают, что вершины i и j соединены ребром.

        //Формат вывода
        //В первой строчке выходного файла выведите количество компонент связности.
        //Далее выведите сами компоненты связности в следующем формате: в первой строке количество вершин в компоненте,
        //во второй - сами вершины в произвольном порядке.

        //ЗАДАЧА НА ПОИСК КОМПОНЕНТ СВЯЗНОСТИ И ИХ КОЛИЧЕСТВО В НЕОРИЕНТИРОВАННОМ ГРАФЕ
        public static void B_Solution_СonnectivityСomponents(string a)
        {
            int[] nm = a.Split(' ').Select(int.Parse).ToArray();
            int n = nm[0];
            int m = nm[1];
            List<List<int>> graph = new List<List<int>>();
            List<bool> visited = new List<bool>();
            for(int i = 0; i <= n; i++)
            {
                graph.Add(new List<int>());
                visited.Add(false);
            }
            for(int i = 0; i < m; i++)
            {
                string edge = Console.ReadLine();
                int[] vertexes = edge.Split(' ').Select(int.Parse).ToArray();
                int vertex1 = vertexes[0];
                int vertexes2 = vertexes[1];
                graph[vertex1].Add(vertexes2);
                graph[vertexes2].Add(vertex1);
            }

            List<List<int>> fulRes = new List<List<int>>();

            for (int i = 1; i < visited.Count; i++)
            {
                List<int> temp_result = new List<int>();
                int temp_count = 0;
                if (!visited[i])
                {
                    DFS(graph, visited, i, ref temp_count, temp_result);
                    temp_result.Sort();
                    fulRes.Add(temp_result);
                }
            }
            Console.WriteLine(fulRes.Count);
            for(int i = 0; i < fulRes.Count; i++)
            {
                Console.WriteLine(fulRes[i].Count);
                for(int j = 0; j < fulRes[i].Count; j++)
                {
                    Console.Write(fulRes[i][j]);
                    if (j!= fulRes[i].Count)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            void DFS(List<List<int>> graph, List<bool> visited, int now, ref int temp_count, List<int> temp_result)
            {
                visited[now] = true;
                temp_count++;
                temp_result.Add(now);
                foreach(int neigh in graph[now])
                {
                    if (!visited[neigh])
                    {
                        DFS(graph, visited, neigh, ref temp_count, temp_result);
                    }
                }
            }
        }

        //Во время контрольной работы профессор Флойд заметил, что некоторые студенты обмениваются записками.
        //Сначала он хотел поставить им всем двойки, но в тот день профессор был добрым,
        //а потому решил разделить студентов на две группы:
        //списывающих и дающих списывать, и поставить двойки только первым.
        //У профессора записаны все пары студентов, обменявшихся записками.Требуется определить,
        //сможет ли он разделить студентов на две группы так,
        //чтобы любой обмен записками осуществлялся от студента одной группы студенту другой группы.

        //Формат ввода
        //В первой строке находятся два числа N и M — количество студентов и количество пар студентов,
        //обменивающихся записками(1 ≤ N ≤ 102, 0 ≤ M ≤ N(N−1)/2).
        //Далее в M строках расположены описания пар студентов: два числа, соответствующие номерам студентов, обменивающихся записками(нумерация студентов идёт с 1). Каждая пара студентов перечислена не более одного раза.

        //Формат вывода
        //Необходимо вывести ответ на задачу профессора Флойда.
        //Если возможно разделить студентов на две группы - выведите YES; иначе выведите NO.

        //ЗАДАЧА НА ДВУДОЛЬНЫЙ ГРАФ В НЕОРИЕНТИРОВАННОМ ГРАФЕ
        public static void C_Solution_BipartiteGraph(string a)
        {
            int[] nm = a.Split(' ').Select(int.Parse).ToArray();
            int n = nm[0];
            int m = nm[1];
            List<List<int>> graph = new List<List<int>>();  //создали список смежности
            List<int> visited = new List<int>(); //список, отображающий посещённые вершины
            for (int i = 0; i <= n; i++)
            {
                graph.Add(new List<int>());     //заполнили списками список смежности
                visited.Add(0);
            }
            for (int i = 0; i < m; i++) //заполнение матрицы смежности
            {
                string edge = Console.ReadLine();
                int[] vertexes = edge.Split(' ').Select(int.Parse).ToArray();
                int vertex1 = vertexes[0];
                int vertex2 = vertexes[1];
                graph[vertex1].Add(vertex2);
                graph[vertex2].Add(vertex1);
            }

            bool resMain = true;

            for (int i = 1; i < visited.Count; i++)
            {
                if (visited[i] == 0)        
                {
                    bool resDFS = DFS(graph, visited, i, 1); // проходим по всем вершинам, на случай, если есть несколько компонент связности
                    if (resDFS == false)
                    {
                        resMain = false;
                    }
                }
            }

            if (resMain == true)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }

            bool DFS(List<List<int>> adjacencyList, List<int> visited, int now, int color)    //описание функции DFS
            {
                visited[now] = 3 - color;   // сразу красим вершину в другой цвет, отличный от соседа
                foreach (var neigh in adjacencyList[now])   //начинаем пробегать по всем соседям вршины  
                {
                    if (visited[neigh] == 0)    //если сосед ещё не посещён, то заходим в дфс для этой вершины
                    {
                        bool mayBeTrable = DFS(adjacencyList, visited, neigh, 3 - color);
                        if (mayBeTrable == false)    //в случае, если там встретилась вершина-сосед такого же цвета - откатываем рекурсии
                        {
                            return false;
                        }
                    }
                    if (visited[neigh] == 3 - color)    //если сосед такого же цвета
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        //Дан ориентированный граф. Необходимо построить топологическую сортировку.

        //Формат ввода
        //В первой строке входного файла два натуральных числа N и M(1 ≤ N, M ≤ 100 000) —
        //количество вершин и рёбер в графе соответственно.Далее в M строках перечислены рёбра графа.
        //Каждое ребро задаётся парой чисел — номерами начальной и конечной вершин соответственно.

        //Формат вывода
        //Выведите любую топологическую сортировку графа в виде последовательности номеров вершин
        //(перестановка чисел от 1 до N). Если топологическую сортировку графа построить невозможно, выведите -1.

        //ЗАДАЧА НА ТОПОЛОГИЧЕСКУЮ СОРТИРОВКУ
        public static void D_Solution_TopologicalSorting(string a)
        {
            int [] nums = a.Split(' ').Select(int.Parse).ToArray();
            int n = nums[0];
            int m = nums[1];
            List<List<int>> graph = new List<List<int>>();
            List<int> visited = new List<int>();    // 0 - белый (не посещена),
                                                    // 1 - серый (посещена, но есть ещё соседи),
                                                    // 2 - чёрный (посещена, соседей больше нет) 
            for (int i = 0;  i <= n; i++)
            {
                graph.Add(new List<int>());
                visited.Add(0);
            }
            for (int i = 0; i < m; i++)
            {
                int [] vertex = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                graph[vertex[0]].Add(vertex[1]);
            }

            List<int> topological_way = new List<int>(); // путь, который мы собираем с конца
            bool mainResult = true; //результирующая переменная, от которой зависит ответ
            for (int i = 1; i < visited.Count; i ++)    
            {                                           
                if (visited[i] == 0)        // проходим по всем вершинам, на случай, если есть несколько компонент связности
                {
                    bool tempResult = DFS(graph, visited, i, topological_way);
                    if (tempResult == false)
                    {
                        mainResult = false;
                    }
                }
            }

            if (mainResult == false)
            {
                Console.WriteLine(-1);
            }
            else{
                topological_way.Reverse(); //истинный путь

                for (int i = 0; i < topological_way.Count; i++) // вывод топологической сортировки
                {
                    Console.Write(topological_way[i]);
                    if (i != topological_way.Count - 1)
                    {
                        Console.Write(" ");
                    }
                }

            }
            

            bool DFS(List<List<int>> graph, List<int> visited, int now, List<int> topological_way) 
            {
                visited[now] = 1;
                foreach(var neigh in graph[now])
                {
                    if (visited[neigh] == 0)
                    {
                        if(DFS(graph, visited, neigh, topological_way) == false)
                        {
                            return false;
                        }
                    }
                    if (visited[neigh] == 1)
                    {
                        return false;
                    }
                }
                visited[now] = 2;
                topological_way.Add(now);
                return true;
            }
        }


        public static void Main(string[] args)
        {
            //A_Solution_DFS(Console.ReadLine());

            //B_Solution_СonnectivityСomponents(Console.ReadLine());

            //C_Solution_BipartiteGraph(Console.ReadLine());

            //D_Solution_TopologicalSorting(Console.ReadLine());


        }
    }
}
