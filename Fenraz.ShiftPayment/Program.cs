using System;
using System.Collections.Generic;
using System.Text;


namespace Fenraz.ShiftPayment
{

    class Program
    {
        // Метод, запрещающий ввод каких-либо символов кроме цифр
        private static string GetLineNoLetters()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return sb.ToString();
                    case ConsoleKey.Backspace:
                        if (sb.Length > 0)
                        {
                            sb.Length -= 1;
                            Console.Write("\b \b");
                        }
                        break;
                    default:
                        if (!char.IsLetter(key.KeyChar))
                        {
                            sb.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                        }
                        break;
                }
            }
        }

        // Метод для ввода числа типа Double.
        // Выполняется повторно в случае появления исключения.
        private static double GetLineDouble()
        {

            double result = 0;
            while (true)
            {


                try
                {
                    Console.WriteLine("Число должно быть " +
                "в формате <12345,124>");
                    result = double.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Вы неверно ввели число. Попробуйте еще раз.");

                }
            }
            return result;
            

            
        }

        static void Main(string[] args)
        {
            // Определение количества сотрудников
            int workers;

            Console.WriteLine("Введите количество рабочих, " +
                "для которых будет производиться расчет");

            workers = Int32.Parse(GetLineNoLetters());

            Console.WriteLine("Вы ввели: " + workers + "\n");

            // Добавление общего числа смен в месяце
            int shifts;
            Console.WriteLine("Введите общее количество смен в месяце.");

            shifts = Int32.Parse(GetLineNoLetters());

            Console.WriteLine("Вы ввели: " + shifts + "\n");

            // Добавление сотрудников
            List<Worker> workersList = new List<Worker>();

            for (int i = 1; i < workers+1; i++)
            {
                string n;
                uint s = 0;
                Console.WriteLine("Добавление сотрудника #" + i);

                Console.WriteLine("Введите имя сотрудника:");
                n = Console.ReadLine();

                // Ввод количества смен, отработанных сотрудником
                while (s == 0 || s > shifts)
                {
                    try
                    {
                        Console.WriteLine("Введите количество смен, отработанных " +
                            " сотрудником за месяц: ");
                        s = UInt32.Parse(GetLineNoLetters());

                        if (s > shifts)
                        {
                            Console.WriteLine("Ошибка! Число смен, отработанных сотрудником" +
                                " превышает общее количество смен.\n");
                        }
                        else if (s == 0)
                        {
                            Console.WriteLine("Ошибка! Число смен не может быть равно нулю.\n" +
                                "Если сотрудник в этом месяце не работал, " +
                                "Перезапустите программу и не добавляйте его в список.\n");
                        }
                        else
                        {
                            workersList.Add(new Worker(n, s));
                            Console.WriteLine();
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка! Введенное значение не является числом.\n");
                    }
                }
            }

            // Вывод списка сотрудников на экран
            Console.WriteLine("Составлен следующий список сотрудников:");
            foreach (Worker w in workersList)
            {
                w.ShowWorker(w);
            }

            Console.WriteLine();

            //Добавление суммы, полученной за совмещение
            Console.WriteLine("Введите общую сумму выплат, полученных " +
                "за совмещение профессий.");
            double overallSum = GetLineDouble();

            Console.WriteLine("Вы ввели: " + overallSum + "\n");

            //Блок расчета. По Окончанию тестов вынести в отдельный метод
            while(overallSum > 1)
            {
                double payPerShift = (overallSum / workers) / shifts;

                foreach (Worker w in workersList)
                {
                    double sum = payPerShift * w.ReturnShifts(w);
                    w.AddPayment(sum);
                    overallSum -= sum;
                }

            }

            double finalPayment = overallSum / workers;
            foreach(Worker w in workersList)
            {
                w.AddPayment(finalPayment);
            }

            // Список выплат
            Console.WriteLine("Список выплат сотрудникам:");
            foreach(Worker w in workersList)
            {
                w.ShowPayments(w);
            }
        }
    }
}
