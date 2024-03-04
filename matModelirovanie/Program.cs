using matModelirovanie;
using Modelirovanie;

//Console.WriteLine("Введите номер метода решения транспортной задачи:");

////int[,] FinalPlan = Methods.Create_Array();
////Methods.Input_Date(FinalPlan);
//bool all = true;
//while (all)
//{
//    int[,] FinalPlan =
//    { { 0, 100, 120, 70, 110, 130},
//    { 100, -3, -5, -11, -10, -5},
//    { 250, -5, -10, -15, -3, -2},
//    { 180, -4, -8, -6, -12, -10} };
//    Console.WriteLine("1 - Метод минимального элемента" +
//        "\n2 - Метод двойного предпочтения" +
//        "\n3 - Метод Фогеля" +
//        "\n4 - Метод северо-западного угла" +
//        "\nn - выйти из программы");
//    char Choise = Convert.ToChar(Console.ReadLine());
//    if (Methods.CheckCloseTask(FinalPlan))
//    {
//        switch (Choise)
//        {
//            case '1':
//                Methods.MinimalElement(FinalPlan);
//                break;
//            case '2':
//                Methods.DoubleLike(FinalPlan);
//                break;
//            case '3':
//                Methods.Fogeli(FinalPlan);
//                break;
//            case '4':
//                Methods.SeveroZapad(FinalPlan);
//                break;
//            case 'n':
//                Console.WriteLine("Удачи!");
//                all = false;
//                break;
//            default:
//                Console.WriteLine("Такого номера нет");
//                break;
//        }
//        Console.WriteLine();
//    }
//    else Console.WriteLine("Задача открытая");
//}

SimpleTable tab = new SimpleTable();
tab.WorkWithTable();



