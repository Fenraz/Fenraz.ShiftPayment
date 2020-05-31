using System;
using System.Collections.Generic;
using System.Text;


namespace Fenraz.ShiftPayment
{
    class Payment
    {
        uint Workers{ get; set; }

        uint Shifts{ get; set; }

        double OverallSum { get; set; }

        public void AddWrkrsAmount(Payment obj)
        {
            Console.WriteLine("Введите количество рабочих, " +
                "для которых будет производиться расчет");
            obj.Workers = uint.Parse(GetLineNoLetters());
            Console.WriteLine("Вы ввели: " + obj.Workers + "\n");
        }

        public void AddShiftsAmount(Payment obj)
        {
            Console.WriteLine("Введите общее количество смен в месяце.");

            obj.Shifts = UInt32.Parse(GetLineNoLetters());

            Console.WriteLine("Вы ввели: " + obj.Shifts + "\n");
        }

        public void AddOverallSum(Payment obj)
        {
            Console.WriteLine("Введите общую сумму выплат, полученных " +
                "за совмещение профессий.");
            obj.OverallSum = GetLineDouble();

            Console.WriteLine("Вы ввели: " + obj.OverallSum + "\n");
        }

        public List<Worker> CreateWrkrsList()
        {
            // Добавление сотрудников
            List<Worker> workersList = new List<Worker>();

            for (int i = 1; i < Workers + 1; i++)
            {
                string n;
                uint s = 0;
                Console.WriteLine("Добавление сотрудника #" + i);

                Console.WriteLine("Введите имя сотрудника:");
                n = Console.ReadLine();

                // Ввод количества смен, отработанных сотрудником
                while (s == 0 || s > Shifts)
                {
                    try
                    {
                        Console.WriteLine("Введите количество смен, отработанных " +
                            " сотрудником за месяц: ");
                        s = UInt32.Parse(GetLineNoLetters());

                        if (s > Shifts)
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

            return workersList;
        }

        public void ShowWrkrsList(List<Worker> list)
        {
            // Вывод списка сотрудников на экран
            Console.WriteLine("Составлен следующий список сотрудников:");
            foreach (Worker w in list)
            {
                w.ShowWorker(w);
            }

        }

        public void CalculatePayment(List<Worker> list)
        {
            //Блок расчета. По Окончанию тестов вынести в отдельный метод
            while (OverallSum > 1)
            {
                double payPerShift = (OverallSum / Workers) / Shifts;

                foreach (Worker w in list)
                {
                    double sum = payPerShift * w.ReturnShifts(w);
                    w.AddPayment(sum);
                    OverallSum -= sum;
                }

            }

            double finalPayment = OverallSum / Workers;
            foreach (Worker w in list)
            {
                w.AddPayment(finalPayment);
            }

            // Список выплат
            Console.WriteLine("Список выплат сотрудникам:");
            foreach (Worker w in list)
            {
                w.ShowPayments(w);
            }
        }

        public void MakePayment()
        {
            

            AddWrkrsAmount(this);

            AddShiftsAmount(this);

            List<Worker> workersList = CreateWrkrsList();

            ShowWrkrsList(workersList);

            AddOverallSum(this);

            Console.WriteLine("Произвести расчет");
            CalculatePayment(workersList);
        }

        // Метод, запрещающий ввод каких-либо символов кроме цифр
        public static string GetLineNoLetters()
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
    }
}
