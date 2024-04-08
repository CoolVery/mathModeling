using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matModelirovanie
{
    internal class SimpleTable
    {
        //= { { 2, -1, 1, 1, 1},{-4, 2, -1, 1, 2 },{ 3, 0, 1, 1, 5} }
        decimal[,] simpleTable;
        decimal[,] tableAll;
        decimal[] celFunction;
        string typeTask;

        public string TypeTask { get => typeTask; set => typeTask = value; }

        public SimpleTable()
        {
            InputSystem();
            OutputSystem();

        }
        public void InputSystem()
        {
            Console.WriteLine("Введите количество уравнений в системе");
            int countYrav = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество основых переменных");
            int countXValue = Convert.ToInt32(Console.ReadLine());
            celFunction = new decimal[countXValue];
            for (int i = 0; i < countXValue; i++)
            {
                Console.WriteLine($"Введите значение аргумента x{i + 1} в целевой функции:");
                celFunction[i] = Convert.ToDecimal(Console.ReadLine());
            }
            int countI = 1;
            simpleTable = new decimal[countYrav, countXValue + 2];

            for (int i = 0; i < simpleTable.GetLength(0); i++)
            {

                int countJ = 1;
                for (int j = 0; j < simpleTable.GetLength(1) - 2; j++)
                {
                    Console.WriteLine($"Введите значение коэффициент переменной x{countJ++} уравнения {countI}");
                    simpleTable[i, j] = Convert.ToDecimal(Console.ReadLine());
                }
                countI++;
            }

            for (int i = 0; i < simpleTable.GetLength(0); i++)
            {
                simpleTable[i, simpleTable.GetLength(1) - 2] = 1;
            }

            countI = 1;
            for (int i = 0; i < simpleTable.GetLength(0); i++)
            {
                Console.WriteLine($"Введите значение уравнения {countI}");
                simpleTable[i, simpleTable.GetLength(1) - 1] = Convert.ToDecimal(Console.ReadLine());
                countI++;
            }

        }
        private void OutputSystem()
        {
            int countBasicVariables = simpleTable.GetLength(1) - 1;

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
            StartOutputTable();
        }
        private void StartOutputTable()
        {
            tableAll = new decimal[simpleTable.GetLength(0) + 2, ((simpleTable.GetLength(1) - 2) + simpleTable.GetLength(0)) + 2];

            for (int i = 1; i <= tableAll.GetLength(1) - 2; i++)
            {
                tableAll[0, i] = i;
            }
            int countBasis = simpleTable.GetLength(1) - 2;
            for (int i = 1; i <= tableAll.GetLength(0) - 2; i++)
            {
                tableAll[i, 0] = ++countBasis;
            }
            InputDateInTable();
        }
        private void PrintTable()
        {
            Console.WriteLine();
            for (int i = 0; i < tableAll.GetLength(0); i++)
            {
                for (int j = 0; j < tableAll.GetLength(1); j++)
                {
                    Console.Write("{0,8:0.0}", tableAll[i, j]);
                }
                Console.WriteLine();
            }
        }
        private void InputDateInTable()
        {
            int countBasis = simpleTable.GetLength(1) - 1;
            for (int i = 1; i <= simpleTable.GetLength(0); i++)
            {
                for (int j = 1; j <= simpleTable.GetLength(1); j++)
                {
                    if (j == simpleTable.GetLength(1) - 1)
                    {
                        tableAll[i, countBasis] = simpleTable[i - 1, j - 1];
                        countBasis++;
                    }
                    else if (j == simpleTable.GetLength(1))
                    {
                        tableAll[i, tableAll.GetLength(1) - 1] = simpleTable[i - 1, j - 1];
                    }
                    else
                    {
                        tableAll[i, j] = simpleTable[i - 1, j - 1];
                    }
                }
            }
            for (int i = 1; i < simpleTable.GetLength(1) - 1; i++)
            {
                tableAll[tableAll.GetLength(0) - 1, i] = 0 - celFunction[i - 1];
            }
            PrintTable();

        }
        public bool CheckMinTask()
        {
            for (int i = 1; i < tableAll.GetLength(1) - 1; i++)
            {
                if (tableAll[tableAll.GetLength(0) - 1, i] > 0) return true;
            }
            return false;
        }
        public bool CheckMaxTask()
        {
            for (int i = 1; i < tableAll.GetLength(1) - 1; i++)
            {
                if (tableAll[tableAll.GetLength(0) - 1, i] < 0) return true;
            }
            return false;
        }
        public void StartWorkWithTable()
        {
            decimal maxElem = 0;
            int maxElemIndex = 0;
            decimal minElem = 100;
            int minElemIndex = 0;
            var arrayCelFunction = new Dictionary<decimal, decimal>();
            if (typeTask == "Минимум")
            {
                for (int i = 1; i < tableAll.GetLength(1) - 1; i++)
                {
                    if (tableAll[tableAll.GetLength(0) - 1, i] > maxElem)
                    {
                        maxElem = tableAll[tableAll.GetLength(0) - 1, i];
                        maxElemIndex = i;
                    }
                }

                for (int i = 1; i < tableAll.GetLength(0) - 1; i++)
                {
                    if (tableAll[i, maxElemIndex] != 0 && tableAll[i, tableAll.GetLength(1) - 1] / tableAll[i, maxElemIndex] > 0)
                    {
                        arrayCelFunction.Add(i, tableAll[i, tableAll.GetLength(1) - 1] / tableAll[i, maxElemIndex]);
                    }
                }
                Dictionary<decimal, decimal>.ValueCollection valueColl = arrayCelFunction.Values;
                decimal minElemInColumn = valueColl.Min();
                int key = Convert.ToInt32(arrayCelFunction.FirstOrDefault(x => x.Value == minElemInColumn).Key);
                decimal controlElement = tableAll[key, maxElemIndex];
                WorkWithTable(controlElement, key, maxElemIndex);
            }
            else if (typeTask == "Максимум")
            {
                for (int i = 1; i < tableAll.GetLength(1) - 1; i++)
                {
                    if (tableAll[tableAll.GetLength(0) - 1, i] < minElem)
                    {
                        minElem = tableAll[tableAll.GetLength(0) - 1, i];
                        minElemIndex = i;
                    }
                }
                for (int i = 1; i < tableAll.GetLength(0) - 1; i++)
                {
                    if (tableAll[i, minElemIndex] != 0 && tableAll[i, tableAll.GetLength(1) - 1] / tableAll[i, minElemIndex] > 0)
                    {
                        arrayCelFunction.Add(i, tableAll[i, tableAll.GetLength(1) - 1] / tableAll[i, minElemIndex]);
                    }
                }
                Dictionary<decimal, decimal>.ValueCollection valueColl = arrayCelFunction.Values;
                decimal minElemInColumn = valueColl.Min();
                int key = Convert.ToInt32(arrayCelFunction.FirstOrDefault(x => x.Value == minElemInColumn).Key);
                decimal controlElement = tableAll[key, minElemIndex];
                WorkWithTable(controlElement, key, minElemIndex);
            }
        }
        private void WorkWithTable(decimal controlElement, int indexRowControlElement, int indexColumnControlElement)
        {
            tableAll[indexRowControlElement, 0] = tableAll[0, indexColumnControlElement];
            for (int i = 1; i < tableAll.GetLength(1); i++)
            {
                tableAll[indexRowControlElement, i] /= controlElement;
            }
            for (int i = 1; i < tableAll.GetLength(0); i++)
            {
                if (i == indexRowControlElement) continue;
                decimal valueToNull = tableAll[indexRowControlElement, indexColumnControlElement] * -(tableAll[i, indexColumnControlElement]);

                for (global::System.Int32 j = 1; j < tableAll.GetLength(1); j++)
                {
                    tableAll[i, j] += tableAll[indexRowControlElement, j] * valueToNull;
                }
            }
            PrintTable();
        }
        public void PrintAnswer()
        {
            Console.WriteLine($"Целевая функция равна - {tableAll[(tableAll.GetLength(0) - 1), (tableAll.GetLength(1) - 1)]}");
            int countValue = celFunction.Length;
            for (int i = tableAll.GetLength(0) - 2; i > 0; i--)
            {
                if (tableAll[i, 0] <= countValue)
                {
                    Console.WriteLine($"Аргумент целевой функции x{tableAll[i, 0]} = {tableAll[i, tableAll.GetLength(1) - 1]}");

                }
            }

        }
    }
}

