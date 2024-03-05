using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matModelirovanie
{
    internal class SimpleTable
    {

        int[,] simpleTable = { {2, -1, 1, 1, 1}, {-4, 2, -1, 1, 2}, {3, 0, 1, 1, 5} };
        int[,] tableAll;
        public SimpleTable()
        {
            InputSystem();
        }
        public void InputSystem()
        {
            //Console.WriteLine("Введите количество уравнений в системе");
            //int countYrav = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Введите количество основых переменных");
            //int countXValue = Convert.ToInt32(Console.ReadLine());

            //int countI = 1;
            //simpleTable = new int[countYrav, countXValue + 2];

            //for (int i = 0; i < simpleTable.GetLength(0); i++)
            //{

            //    int countJ = 1;
            //    for (int j = 0; j < simpleTable.GetLength(1) - 2; j++)
            //    {
            //        Console.WriteLine($"Введите значение коэффициент переменной x{countJ++} уравнения {countI}");
            //        simpleTable[i, j] = Convert.ToInt32(Console.ReadLine());
            //    }
            //    countI++;
            //}

            //for (int i = 0; i < simpleTable.GetLength(0); i++)
            //{
            //    simpleTable[i, simpleTable.GetLength(1) - 2] = 1;
            //}

            //countI = 1;
            //for (int i = 0; i < simpleTable.GetLength(0); i++)
            //{
            //    Console.WriteLine($"Введите значение уравнения {countI}");
            //    simpleTable[i, simpleTable.GetLength(1) - 1] = Convert.ToInt32(Console.ReadLine());
            //}
            OutputSystem();
        }
        public void OutputSystem()
        {
            int countBasicVariables = (simpleTable.GetLength(1) - 2) + 1;

            Console.WriteLine($"Система уравнений:\n");
            for (int i = 0; i < simpleTable.GetLength(0); i++)
            {
                int countJ = 1;
                
                for (int j = 0; j < simpleTable.GetLength(1); j++)
                {

                    if (j == simpleTable.GetLength(1) - 1)
                    {
                        Console.Write($" = {simpleTable[i, j]}");
                    }
                    else if (j == simpleTable.GetLength(1) - 2)
                    {
                        Console.Write($"{simpleTable[i, j]}x{countBasicVariables}"); countBasicVariables++;
                    }
                    else Console.Write($"{simpleTable[i, j]}x{countJ} + "); countJ++;
                }
                Console.WriteLine();
            }
        }
        public void CreateTable()
        {
            tableAll = new int[simpleTable.GetLength(0) + 2, ((simpleTable.GetLength(1) - 2) + simpleTable.GetLength(0)) + 2];
            int xValue = simpleTable.GetLength(1) - 1;

            Console.WriteLine("Введите значения переменных из выражения задачи");
            
            for (int i = 1; i < simpleTable.GetLength(1) - 1; i++)
            {
                int index = Convert.ToInt32(Console.ReadLine());
                tableAll[tableAll.GetLength(0) - 1, i] = index * -1;
            }
            for (int i = 1; i < tableAll.GetLength(1) - 1; i++)
            {
                tableAll[0, i] = i;
            }
            for (int i = 1; i <= tableAll.GetLength(0) - 2; i++)
            {
                tableAll[i, 0] = xValue++;
            }
            for (int i = 1; i <= tableAll.GetLength(0) -2; i++)
            {
                tableAll[i, tableAll.GetLength(1) - 1] = simpleTable[i-1, simpleTable.GetLength(1) - 1];
            }
            for (int i = 0;i < tableAll.GetLength(0);i++)
            {
                for(int j = 0;j < tableAll.GetLength(1); j++)
                {
                    Console.Write("{0,4:0}", tableAll[i, j]);
                }
                Console.WriteLine() ;
            }
            WorkWithTable();
        }
        private void WorkWithTable()
        {
            List<int> arrayIndex = new List<int>();
            for (int i = 1; i < tableAll.GetLength(1) - 1 ; i++)
            {
                arrayIndex.Add(tableAll[tableAll.GetLength(0) - 1,i]) ;
            }
        }

    
    }
}
