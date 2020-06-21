using System;


namespace Fenraz.ShiftPayment
{
    class Worker
    {
        string Name { get; set; }
        uint Shifts { get; set; } // Количество отработанных смен
        double Payment { get; set; } // Выплата после разделения

        public Worker(string n, uint s)
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
        public uint ReturnShifts(Worker w)
        {
            return w.Shifts;
        }

        // Данные о сотруднике (для сохранения в файл)
        public string WorkerData(Worker w)
        {
            string wrkrData = $"Сотрудник: {w.Name}, отработал смен: {w.Shifts}, выплата: {w.Payment:f3}";
            return wrkrData;
        }
    }
}
