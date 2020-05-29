using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenraz.ShiftPayment
{
    class Worker
    {
        string Name { get; set; }
        int Shifts { get; set; } // Количество отработанных смен
        double Payment { get; set; } // Выплата после разделения

        public Worker(string n, int s)
        {
            Name = n;
            Shifts = s;
        }

        // Отобразить данные по сотруднику
        public void ShowWorker(Worker w)
        {
            Console.WriteLine("Имя: {0}, отработал смен: {1}", w.Name, w.Shifts);
        }

        // Отобразить данные после расчетов
        public void ShowPayments(Worker w)
        {
            Console.WriteLine("Сотрудник: {0}, выплата: {1:f3}", w.Name, w.Payment);
        }

        // Добавление суммы
        public void AddPayment(double sum)
        {
            Payment += sum;
        }

        // Возвратить количество смен (для расчетов)
        public int ReturnShifts(Worker w)
        {
            return w.Shifts;
        }
    }



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

            // Добавление сотрудников
            List<Worker> workersList = new List<Worker>();

            for (int i = 0; i < workers; i++)
            {
                string n;
                int s;
                Console.WriteLine("Добавление сотрудника #" + i);

                Console.WriteLine("Введите имя сотрудника:");
                n = Console.ReadLine();

                Console.WriteLine("Введите количество смен, отработанных " +
                    " сотрудником за месяц: ");
                s = Int32.Parse(GetLineNoLetters());

                workersList.Add(new Worker(n, s));

                Console.WriteLine();
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

            // Добавление общего числа смен в месяце
            int shifts;
            Console.WriteLine("Введите общее количество смен в месяце.");

            shifts = Int32.Parse(GetLineNoLetters());

            Console.WriteLine("Вы ввели: " + shifts + "\n");

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
