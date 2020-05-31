using System;


namespace Fenraz.ShiftPayment
{

    class Program
    {              
        static void Main(string[] args)
        {
            //int count = 0;
            do
            {
                Console.WriteLine("Выберите действие:\n");
                Console.WriteLine("R - создать новый расчет");
                Console.WriteLine("Q - выход\n");

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.R:
                        Console.WriteLine("Создание расчета:\n");
                        Payment payment = new Payment();
                        payment.MakePayment();
                        break;
                    case ConsoleKey.Q:
                        return;

                }
            } while (true);
        }
    }
}
