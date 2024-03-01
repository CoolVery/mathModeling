using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    internal class Methods
    {
        public static int[,] Create_Array()
        {

            Console.WriteLine("Введите количество складов");
            int CountStorage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество потребностей");
            int CountNeeds = Convert.ToInt32(Console.ReadLine());


            int[,] FinalPlay = new int[++CountStorage, ++CountNeeds];
            return FinalPlay;
        }

        public static int[,] Input_Date(int[,] FinalPlay)
        {

            for (int i = 0; i < FinalPlay.GetLength(0); i++)
            {
                if (i == 0)
                {
                    for (int j = 1; j < FinalPlay.GetLength(1); j++)
                    {
                        Console.WriteLine("Введите необходимую потребность: ");
                        FinalPlay[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                else
                {
                    for (int j = 0; j < FinalPlay.GetLength(1); j++)
                    {
                        if (j == 0)
                        {
                            Console.WriteLine("Введите количество продукции на складе: ");
                            FinalPlay[i, j] = Convert.ToInt32(Console.ReadLine());
                        }
                        else
                        {
                            Console.WriteLine("Введите тарифы: ");
                            FinalPlay[i, j] = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                }
            }

            for (int i = 0; i < FinalPlay.GetLength(0); i++)
            {
                for (int j = 0; j < FinalPlay.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(FinalPlay[i, j]) + " ");
                }
                Console.WriteLine();
            }

            return FinalPlay;
        }
        //==============================
        public static bool CheckCloseTask(int[,] finalPlan)
        {
            int sumI = 0;
            int sumJ = 0;
 
            for (global::System.Int32 j = 1; j < finalPlan.GetLength(1); j++)
            {
                sumI = sumI + finalPlan[0, j];
            }

            for(global::System.Int32 i = 1; i < finalPlan.GetLength(0); i++)
            {
                sumJ = sumJ + finalPlan[i, 0];
            }
            if (sumI == sumJ)
            {
                return true;
            }
            else return false;
        }

        public static bool CheckVirozTask(int[,] ArrayOfCost, int[,] finalPlan)
        {
            int count = (finalPlan.GetLength(0)-1) + (finalPlan.GetLength(1)-1);
            int countNotNull = 0;
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    if (ArrayOfCost[i,j] != 0)
                    {
                        countNotNull++;
                    }
                }
            }
            if (count-1 == countNotNull)
            {
                return true;
            }
            else return false;

        }

        private static int CountCelFunction(int[,] ArrayOfCost, int[,] Tarifs)
        {
            int resultFunction = 0;
            for (int i = 0; i < Tarifs.GetLength(0); i++)
            {
                for (int j = 0; j < Tarifs.GetLength(1); j++)
                {
                    if (ArrayOfCost[i,j] != 0) 
                    {
                        resultFunction += ArrayOfCost[i, j] * Tarifs[i, j];
                    }
                }
            }
            return resultFunction;
        }
        //==============================
        private static int[,] GetTarifs(int[,] finalPlan)
        {
            int[,] Tariffs = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            int StepI = 0;
            int StepJ = 0;
            for (int i = 1; i < finalPlan.GetLength(0); i++)
            {
                for (int j = 1; j < finalPlan.GetLength(1); j++)
                {
                    Tariffs[StepI, StepJ] = finalPlan[i, j];
                    StepJ++;
                }
                StepI++;
                StepJ = 0;
            }

            return Tariffs;
        }

        private static bool CheckArray(bool[,] BoolArray)
        {
            for (int i = 0; i < BoolArray.GetLength(0); i++)
            {
                for (int j = 0; j < BoolArray.GetLength(1); j++)
                {
                    if (BoolArray[i, j] == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private static int MinValue(int[,] Tarif, bool[,] BoolArray)
        {
            int Minimal = 0;

            for (int i = 0; i < Tarif.GetLength(0); i++)
            {
                for (int j = 0; j < Tarif.GetLength(1); j++)
                {
                    if (BoolArray[i, j] == false)
                    {
                        Minimal = Tarif[i, j];
                        break;
                    }
                }
            }

            for (int i = 0; i < Tarif.GetLength(0); i++)
            {
                for (int j = 0; j < Tarif.GetLength(1); j++)
                {
                    if (Tarif[i, j] < Minimal && BoolArray[i, j] == false)
                    {
                        Minimal = Tarif[i, j];
                    }
                }
            }
            return Minimal;
        }

        public static int[,] MinimalElement(int[,] finalPlan)
        {

            int[,] Tariffs = GetTarifs(finalPlan);
            int[,] ArrayOfCost = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            bool[,] BoolArray = new bool[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];

            while (CheckArray(BoolArray))
            {

                int minValue = MinValue(Tariffs, BoolArray);
                for (int i = 1; i < finalPlan.GetLength(0); i++)
                {
                    for (int j = 1; j < finalPlan.GetLength(1); j++)
                    {
                        if (finalPlan[i, j] == minValue && i - 1 >= 0 && j - 1 >= 0 && BoolArray[i - 1, j - 1] == false)
                        {
                            ArrayOfCost[i - 1, j - 1] = Math.Min(finalPlan[0, j], finalPlan[i, 0]);

                            int RestStorage = finalPlan[i, 0] - Math.Min(finalPlan[0, j], finalPlan[i, 0]);
                            int RestNeeds = finalPlan[0, j] - (Math.Min(finalPlan[0, j], finalPlan[i, 0]));

                            finalPlan[i, 0] = RestStorage;
                            finalPlan[0, j] = RestNeeds;
                            if (finalPlan[i, 0] == 0)
                            {
                                for (int k = 0; k < finalPlan.GetLength(1) - 1; k++)
                                {
                                    BoolArray[i - 1, k] = true;
                                }
                            }
                            else if (finalPlan[0, j] == 0)
                            {
                                for (int k = 0; k < finalPlan.GetLength(0) - 1; k++)
                                {
                                    BoolArray[k, j - 1] = true;

                                }
                            }
                        }

                    }

                }
            }
            Console.WriteLine();
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(ArrayOfCost[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Целевая функция равна: " + Convert.ToString(CountCelFunction(ArrayOfCost, Tariffs)));

            if (CheckVirozTask(ArrayOfCost, finalPlan))
            {
                Console.WriteLine("План невырожденный");
            }
            else
            {
                Console.WriteLine("План вырожденный");
                return Tariffs;
            }
            Methods.PotenchialCount(Tariffs, ArrayOfCost);
            return Tariffs;
        }
        //===========================================================
        private static int[,] FillArrayPlus(int[,] Tariffs, int[,] arrayPlus)
        {


            for (int i = 0; i < Tariffs.GetLength(0); i++)
            {
                int Minimal = Tariffs[i, 0];
                for (int j = 0; j < Tariffs.GetLength(1); j++)
                {

                    if (Tariffs[i, j] < Minimal)
                    {
                        Minimal = Tariffs[i, j];

                    }

                }
                for (int j = 0; j < Tariffs.GetLength(1); j++)
                {
                    if (Tariffs[i, j] == Minimal)
                    {
                        arrayPlus[i, j]++;
                    }
                }


            }
            for (int i = 0; i < Tariffs.GetLength(1); i++)
            {
                int Minimal = Tariffs[0, i];
                for (int j = 0; j < Tariffs.GetLength(0); j++)
                {

                    if (Tariffs[j, i] < Minimal)
                    {
                        Minimal = Tariffs[j, i];

                    }

                }
                for (int j = 0; j < Tariffs.GetLength(0); j++)
                {
                    if (Tariffs[j, i] == Minimal)
                    {
                        arrayPlus[j, i]++;
                    }
                }
            }



            return arrayPlus;
        }

        private static int CountTwoInArray(int[,] arrayPlus)
        {
            int count = 0;
            for (int i = 0; i < arrayPlus.GetLength(0); i++)
            {
                for (int j = 0; j < arrayPlus.GetLength(1); j++)
                {
                    if (arrayPlus[i, j] == 2)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static int CountOneInArray(int[,] arrayPlus)
        {
            int count = 0;
            for (int i = 0; i < arrayPlus.GetLength(0); i++)
            {
                for (int j = 0; j < arrayPlus.GetLength(1); j++)
                {
                    if (arrayPlus[i, j] == 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static int[] ArrayMinTwoPlus(int[,] arrayPlus, int[,] Tarif)
        {
            int count = 0;
            int[] ArrayTwo = new int[CountTwoInArray(arrayPlus)];
            for (int i = 0; i < Tarif.GetLength(0); i++)
            {
                for (int j = 0; j < Tarif.GetLength(1); j++)
                {
                    if (arrayPlus[i, j] == 2)
                    {
                        ArrayTwo[count] = Tarif[i, j];
                        count++;
                    }
                }
            }
            Array.Sort(ArrayTwo);
            return ArrayTwo;
        }
        private static int[] ArrayMinOnePlus(int[,] arrayPlus, int[,] Tarif)
        {
            int count = 0;
            int[] ArrayOne = new int[CountOneInArray(arrayPlus)];
            for (int i = 0; i < Tarif.GetLength(0); i++)
            {
                for (int j = 0; j < Tarif.GetLength(1); j++)
                {
                    if (arrayPlus[i, j] == 1)
                    {
                        ArrayOne[count] = Tarif[i, j];
                        count++;
                    }
                }
            }
            Array.Sort(ArrayOne);
            return ArrayOne;
        }
        public static int[,] DoubleLike(int[,] finalPlan)
        {
            int countWhile = 0;
            int[,] Tariffs = GetTarifs(finalPlan);
            int[,] ArrayOfCost = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            int[,] ArrayPlus = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            bool[,] BoolArray = new bool[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            ArrayPlus = FillArrayPlus(Tariffs, ArrayPlus);

            int[] ArrayTwoPlus = ArrayMinTwoPlus(ArrayPlus, Tariffs);
            int[] ArrayOnePlus = ArrayMinOnePlus(ArrayPlus, Tariffs);
            while (countWhile < ArrayTwoPlus.Length)
            {
                int minValue = ArrayTwoPlus[countWhile];

                for (int i = 0; i < Tariffs.GetLength(0); i++)
                {
                    for (int j = 0; j < Tariffs.GetLength(1); j++)
                    {
                        if (Tariffs[i, j] == minValue && BoolArray[i, j] == false)
                        {
                            ArrayOfCost[i, j] = Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);

                            int RestStorage = finalPlan[i + 1, 0] - Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);
                            int RestNeeds = finalPlan[0, j + 1] - (Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]));

                            finalPlan[i + 1, 0] = RestStorage;
                            finalPlan[0, j + 1] = RestNeeds;
                            if (finalPlan[i + 1, 0] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(1); k++)
                                {
                                    BoolArray[i, k] = true;
                                }

                            }
                            else if (finalPlan[0, j + 1] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(0); k++)
                                {
                                    BoolArray[k, j] = true;
                                }

                            }
                        }
                    }
                }
                countWhile++;
            }
            countWhile = 0;
            while (countWhile < ArrayOnePlus.Length)
            {
                int minValue = ArrayOnePlus[countWhile];

                for (int i = 0; i < Tariffs.GetLength(0); i++)
                {
                    for (int j = 0; j < Tariffs.GetLength(1); j++)
                    {
                        if (Tariffs[i, j] == minValue && BoolArray[i, j] == false)
                        {
                            ArrayOfCost[i, j] = Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);

                            int RestStorage = finalPlan[i + 1, 0] - Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);
                            int RestNeeds = finalPlan[0, j + 1] - (Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]));

                            finalPlan[i + 1, 0] = RestStorage;
                            finalPlan[0, j + 1] = RestNeeds;
                            if (finalPlan[i + 1, 0] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(1); k++)
                                {
                                    BoolArray[i, k] = true;
                                }

                            }
                            else if (finalPlan[0, j + 1] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(0); k++)
                                {
                                    BoolArray[k, j] = true;
                                }

                            }
                        }
                    }
                }
                countWhile++;
            }


            while (CheckArray(BoolArray))
            {
                int minValue = MinValue(Tariffs, BoolArray);


                for (int i = 0; i < Tariffs.GetLength(0); i++)
                {
                    for (int j = 0; j < Tariffs.GetLength(1); j++)
                    {
                        if (Tariffs[i, j] == minValue && BoolArray[i, j] == false)
                        {
                            ArrayOfCost[i, j] = Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);

                            int RestStorage = finalPlan[i + 1, 0] - Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]);
                            int RestNeeds = finalPlan[0, j + 1] - (Math.Min(finalPlan[0, j + 1], finalPlan[i + 1, 0]));

                            finalPlan[i + 1, 0] = RestStorage;
                            finalPlan[0, j + 1] = RestNeeds;
                            if (finalPlan[i + 1, 0] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(1); k++)
                                {
                                    BoolArray[i, k] = true;
                                }
                            }
                            else if (finalPlan[0, j + 1] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(0); k++)
                                {
                                    BoolArray[k, j] = true;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(ArrayOfCost[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Целевая функция равна: " + Convert.ToString(CountCelFunction(ArrayOfCost, Tariffs)));

            if (CheckVirozTask(ArrayOfCost, finalPlan))
            {
                Console.WriteLine("План невырожденный");
            }
            else
            {
                Console.WriteLine("План вырожденный");
                return Tariffs;
            }
            return finalPlan;
        }

        //===========================
        private static int MaksStrafh(int[,] FinalPlanAll)
        {
            int MaksValue = FinalPlanAll[0, FinalPlanAll.GetLength(1) - 1];
            for (int i = 0; i < FinalPlanAll.GetLength(0); i++)
            {
                for (int j = 0; j < FinalPlanAll.GetLength(1); j++)
                {
                    if (MaksValue < FinalPlanAll[i, j])
                    {
                        MaksValue = FinalPlanAll[i, j];
                    }
                }
            }
            return MaksValue;
        }
        private static int CountRowTrue(bool[,] BoolArray)
        {
            int MaxCount = 0;
            int countInRow = 0;
            for (int i = 0; i < BoolArray.GetLength(0); i++)
            {
                countInRow = 0;
                for (global::System.Int32 j = 0; j < BoolArray.GetLength(1); j++)
                {
                    
                    if (BoolArray[i, j] == false)
                    {
                        countInRow++;
                    }
                }
                if (countInRow > MaxCount)
                {
                    MaxCount = countInRow;
                    countInRow = 0;
                }
                
            }
            return MaxCount;
        }
        private static int CountColumnTrue(bool[,] BoolArray)
        {
            int MaxCount = 0; 
            int countInColumn = 0;
            for (int i = 0; i < BoolArray.GetLength(1); i++)
            {
                countInColumn = 0;
                for (global::System.Int32 j = 0; j < BoolArray.GetLength(0); j++)
                {
                    
                    if (BoolArray[j, i] == false)
                    {
                        countInColumn++;
                    }
                }
                if (countInColumn > MaxCount)
                {
                   MaxCount = countInColumn;
                   countInColumn = 0;
                }
                
            }
            return MaxCount;
        }
        private static int[,] Shtrafi(int[,] finalplan, bool[,] BoolArray, int[,] Tariffs, int[,] FinalPlanAll, int[,] ArrayOfCost)
        {
            while (CheckArray(BoolArray))
            {
                for (int i = 1; i < finalplan.GetLength(0); i++)
                {
                    int CountRow = 0;
                    int[] Row = new int[CountRowTrue(BoolArray)];
                    for (int j = 1; j < finalplan.GetLength(1); j++)
                    {
                        if (BoolArray[i-1,j-1] == false)
                        {
                            Row[CountRow] = finalplan[i, j];
                            CountRow++;
                        }
                        
                    }
                    
                    Array.Sort(Row);
                    if (Row.Length != 1)
                    {
                        if (Row[1] == Row[0])
                        {
                            int SecondMin = 0;
                            for (int j = 0; j < Row.Length; j++)
                            {
                                if (Row[0] != Row[j])
                                {
                                    SecondMin = Row[j]; break;
                                }
                            }
                            FinalPlanAll[i, FinalPlanAll.GetLength(1) - 1] = SecondMin - Row[0];
                        }
                        else FinalPlanAll[i, FinalPlanAll.GetLength(1) - 1] = Row[1] - Row[0];
                    }
                    else FinalPlanAll[i, FinalPlanAll.GetLength(1) - 1] = Row[0];
                }
                for (int i = 1; i < finalplan.GetLength(1); i++)
                {
                    int CountColumn = 0;
                    int[] Column = new int[CountColumnTrue(BoolArray)];
                    for (int j = 1; j < finalplan.GetLength(0); j++)
                    {
                        if (BoolArray[j - 1, i - 1] == false)
                        {
                            Column[CountColumn] = finalplan[j, i];
                            CountColumn++;
                        }

                    }
                    Array.Sort(Column);
                    if (Column.Length != 1)
                    {
                        if (Column[1] == Column[0])
                        {
                            int SecondMin = 0;
                            for (int j = 0; j < Column.Length; j++)
                            {
                                if (Column[0] != Column[j])
                                {
                                    SecondMin = Column[j]; break;
                                }
                            }
                            FinalPlanAll[FinalPlanAll.GetLength(0) - 1, i] = SecondMin - Column[0];
                        }
                        else FinalPlanAll[FinalPlanAll.GetLength(0) - 1, i] = Column[1] - Column[0];
                    }
                    else FinalPlanAll[FinalPlanAll.GetLength(0) - 1, i] = Column[0];
                }

                int MaksValue = MaksStrafh(FinalPlanAll);
                int IndexIMaks = 0;
                int IndexJMaks = 0;
                for (int i = 0; i < FinalPlanAll.GetLength(0); i++)
                {
                    for (int j = 0; j < FinalPlanAll.GetLength(1); j++)
                    {
                        if (FinalPlanAll[i, j] == MaksValue)
                        {
                            IndexIMaks = i; IndexJMaks = j;
                            break;
                        }
                    }
                }
                int minValue = 0;
                int IndexIMin = 0;
                int IndexJMin = 0;
                if (IndexIMaks == FinalPlanAll.GetLength(0) - 1)
                {
                    int countColumn = 0;
                    int[] Column = new int[CountColumnTrue(BoolArray)];
                    for (int i = 1; i < finalplan.GetLength(0); i++)
                    {
                        if (BoolArray[ i - 1,IndexJMaks - 1] == false)
                        {
                            Column[countColumn] = finalplan[i, IndexJMaks];
                            countColumn++;
                        }
                    }
                    Array.Sort(Column);
                    minValue = Column[0];
                    for (int i = 1; i < finalplan.GetLength(0); i++)
                    {
                        if (finalplan[i, IndexJMaks] == minValue && BoolArray[i-1, IndexJMaks-1] == false)
                        {
                            IndexIMin = i; IndexJMin = IndexJMaks;
                            break;
                        }


                    }

                }
                else if (IndexJMaks == FinalPlanAll.GetLength(1) - 1)
                {
                    int countRow = 0;
                    int[] Row = new int[CountRowTrue(BoolArray)];
                    for (int i = 1; i < finalplan.GetLength(1); i++)
                    {
                        if (BoolArray[IndexIMaks-1,i-1] == false)
                        {
                            Row[countRow] = finalplan[IndexIMaks, i];
                            countRow++;
                        }
                        
                    }
                    Array.Sort(Row);
                    minValue = Row[0];
                    for (int i = 1; i < finalplan.GetLength(1); i++)
                    {
                        if (finalplan[IndexIMaks, i] == minValue && BoolArray[IndexIMaks-1,i-1] == false)
                        {
                            IndexIMin = IndexIMaks; IndexJMin = i;
                            break;
                        }


                    }
                }


                for (int i = 0; i < Tariffs.GetLength(0); i++)
                {
                    for (int j = 0; j < Tariffs.GetLength(1); j++)
                    {
                        if (BoolArray[i, j] == false && i == IndexIMin-1 && j == IndexJMin-1)
                        {
                            ArrayOfCost[i, j] = Math.Min(finalplan[0, j +1], finalplan[i+1 , 0]);

                            int RestStorage = finalplan[i+1 , 0] - Math.Min(finalplan[0, j +1], finalplan[i+1, 0]);
                            int RestNeeds = finalplan[0, j +1] - (Math.Min(finalplan[0, j+1], finalplan[i+1 , 0]));

                            finalplan[i +1, 0] = RestStorage;
                            finalplan[0, j+1 ] = RestNeeds;
                            if (finalplan[i+1 , 0] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(1); k++)
                                {
                                    BoolArray[i, k] = true;
                                }
                            }
                            else if (finalplan[0, j+1] == 0)
                            {
                                for (int k = 0; k < Tariffs.GetLength(0); k++)
                                {
                                    BoolArray[k, j] = true;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(ArrayOfCost[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Целевая функция равна: " + Convert.ToString(CountCelFunction(ArrayOfCost, Tariffs)));

            if (CheckVirozTask(ArrayOfCost, finalplan))
            {
                Console.WriteLine("План невырожденный");
            }
            else
            {
                Console.WriteLine("План вырожденный");
                return FinalPlanAll;
            }

            return finalplan;
        }
        public static int[,] Fogeli(int[,] finalPlan)
        {
            int[,] FinalPlanAll = new int[finalPlan.GetLength(0) + 1, finalPlan.GetLength(1) + 1];
            int[,] Tariffs = GetTarifs(finalPlan);
            int[,] ArrayOfCost = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            bool[,] BoolArray = new bool[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            Shtrafi(finalPlan, BoolArray, Tariffs, FinalPlanAll, ArrayOfCost);
            return finalPlan;
        }
        //==============================
        public static int[,] SeveroZapad(int[,] finalPlan)
        {

            int[,] Tariffs = GetTarifs(finalPlan);
            int[,] ArrayOfCost = new int[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];
            bool[,] BoolArray = new bool[finalPlan.GetLength(0) - 1, finalPlan.GetLength(1) - 1];

            while (CheckArray(BoolArray))
            {

                for (int i = 1; i < finalPlan.GetLength(0); i++)
                {
                    for (int j = 1; j < finalPlan.GetLength(1); j++)
                    {
                        if (i - 1 >= 0 && j - 1 >= 0 && BoolArray[i - 1, j - 1] == false)
                        {
                            ArrayOfCost[i - 1, j - 1] = Math.Min(finalPlan[0, j], finalPlan[i, 0]);

                            int RestStorage = finalPlan[i, 0] - Math.Min(finalPlan[0, j], finalPlan[i, 0]);
                            int RestNeeds = finalPlan[0, j] - (Math.Min(finalPlan[0, j], finalPlan[i, 0]));

                            finalPlan[i, 0] = RestStorage;
                            finalPlan[0, j] = RestNeeds;
                            if (finalPlan[i, 0] == 0)
                            {
                                for (int k = 0; k < finalPlan.GetLength(1) - 1; k++)
                                {
                                    BoolArray[i - 1, k] = true;
                                }
                            }
                            else if (finalPlan[0, j] == 0)
                            {
                                for (int k = 0; k < finalPlan.GetLength(0) - 1; k++)
                                {
                                    BoolArray[k, j - 1] = true;

                                }
                            }
                        }

                    }

                }
            }
            Console.WriteLine();
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (int j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(ArrayOfCost[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Целевая функция равна: " + Convert.ToString(CountCelFunction(ArrayOfCost, Tariffs)));

            if (CheckVirozTask(ArrayOfCost, finalPlan))
            {
                Console.WriteLine("План невырожденный");
            }
            else
            {
                Console.WriteLine("План вырожденный");
                return Tariffs;
            }
            return Tariffs;
        }
        //===================================
        private static bool CheckBoolArray(bool[,] BoolPotencial)
        {
            for (int i = 0; i < BoolPotencial.GetLength(1); i++)
            {
                if (i == BoolPotencial.GetLength(1) - 1)
                {
                    continue;
                }
                if (BoolPotencial[BoolPotencial.GetLength(0) - 1, i] == false) return true;
            }
            for (int j = 0; j < BoolPotencial.GetLength(0); j++)
            {
                if (j == BoolPotencial.GetLength(0) - 1)
                {
                    continue;
                }
                if (BoolPotencial[j, BoolPotencial.GetLength(1) - 1] == false) return true;
            }
            return false;
        }
        public static int[,] PotenchialCount( int[,] Tarifs, int[,] ArrayOfCost)
        {
            int[,] Potencial = new int[ArrayOfCost.GetLength(0) + 1, ArrayOfCost.GetLength(1) + 1];
            bool[,] BoolPotencial = new bool[Potencial.GetLength(0), Potencial.GetLength(1)];

            Potencial[0, ArrayOfCost.GetLength(1)] = 0;
            BoolPotencial[0, ArrayOfCost.GetLength(1)] = true;
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                    for (int j = 0; j < ArrayOfCost.GetLength(1); j++)
                    {
                        if (ArrayOfCost[i, j] != 0)
                        {
                            Potencial[ArrayOfCost.GetLength(0), j] = Tarifs[i, j] - Potencial[i, ArrayOfCost.GetLength(1)];
                            BoolPotencial[ArrayOfCost.GetLength(0), j] = true;
                        }
                    }
                break;
            }

            while (CheckBoolArray(BoolPotencial))
            {
                for (global::System.Int32 i = 0; i < ArrayOfCost.GetLength(0); i++)
                {
                    for (global::System.Int32 j = 0; j < ArrayOfCost.GetLength(1); j++)
                    {
                        if (ArrayOfCost[i,j] != 0 )
                        {
                            if (BoolPotencial[ArrayOfCost.GetLength(0), j] == true && BoolPotencial[i, ArrayOfCost.GetLength(1)] == false)
                            {
                                Potencial[i, ArrayOfCost.GetLength(1)] = Tarifs[i,j] - Potencial[ArrayOfCost.GetLength(0), j];
                                BoolPotencial[i, ArrayOfCost.GetLength(1)] = true;
                            }
                            else if (BoolPotencial[i, ArrayOfCost.GetLength(1)] == true && BoolPotencial[ArrayOfCost.GetLength(0), j] == false)
                            {
                                Potencial[ArrayOfCost.GetLength(0), j] = Tarifs[i, j] - Potencial[i, ArrayOfCost.GetLength(1)];
                                BoolPotencial[ArrayOfCost.GetLength(0), j] = true;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Potencial.GetLength(0); i++)
            {
                for (int j = 0; j < Potencial.GetLength(1); j++)
                {
                    Console.Write(Convert.ToString(Potencial[i, j]) + " ");
                }
                Console.WriteLine();
            }
            OptimalPlan(Potencial, ArrayOfCost, Tarifs);
            return Potencial;
        }
        private static int[,] OptimalPlan(int[,] Potencial, int[,] ArrayOfCost, int[,] Tarifs)
        {
            string resultOptimal = "";
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    if (ArrayOfCost[i,j] == 0)
                    {
                        int result = Potencial[i, ArrayOfCost.GetLength(1)] + Potencial[ArrayOfCost.GetLength(0), j] - Tarifs[i,j];
                        Console.WriteLine("Оценка ячейки с индексом (" + i + " ; " + j + ") равна = " + result);
                        if (result > 0)
                        {
                            resultOptimal = "Опорный план не оптимальный";
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(resultOptimal))
            {
                Console.WriteLine("Опорный план оптимальный");
            }
            else
            {
                Console.WriteLine(resultOptimal);
                AgainArrayOfCost(ArrayOfCost, Tarifs);
            }
            return Potencial;
        }
        private static int[,] AgainArrayOfCost(int[,] ArrayOfCost, int[,] Tarifs)
        {

            
            Console.WriteLine("Введите переопределенный опорный план:");
            for (int i = 0; i < ArrayOfCost.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < ArrayOfCost.GetLength(1); j++)
                {
                    Console.WriteLine("Ячейка с индексом ( " + i + " ; " + j + " )");
                    ArrayOfCost[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            PotenchialCount(Tarifs, ArrayOfCost);
            return ArrayOfCost;
        }
    }
    

}
