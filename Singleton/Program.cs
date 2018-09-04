namespace Singleton
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            Singleton instance1 = Singleton.GetInstance();
            Singleton instance2 = Singleton.GetInstance();
            Singleton instance3 = DerivedSingleton.GetInstance();
            Singleton instance4 = DerivedSingleton.GetInstance();

            Console.WriteLine("let's compare references for both instances: derived and derived");
            Console.WriteLine(ReferenceEquals(instance3, instance4));


            Console.ReadKey();
        }
    }

    /*
     * Если в программе имеется несколько производных классов-одиночек (DerivedSingleton1, DerivedSingleton2, …)
     *  и далее потребуется организовать выбор использования определенного производного класса-одиночки,
     *   то для выбора нужного экземпляра-одиночки удобно будет воспользоваться реестром одиночек,
     *    который представляет собой ассоциативный массив (ключ-значение).
     *     В качестве ключа может выступать имя одиночки, а в качестве значения – ссылка на экземпляр определенного одиночки.
     */

    internal class Singleton // может быть запечатанным дабы избежать наследования
    {
        protected static Singleton instance;

        private string singletonData;

        protected Singleton() { }

        public static Singleton GetInstance()
        {
            return instance ?? (instance = new Singleton());
        }

        public void DoSingletonOperation()
        {
            this.singletonData = "SingletonData";
        }

        public string GetSingletonData()
        {
            return this.singletonData;
        }
    }

    internal sealed class DerivedSingleton : Singleton
    {
        public new static DerivedSingleton GetInstance()
        {
            if (instance == null) instance = new DerivedSingleton();
            return instance as DerivedSingleton;
        }
    }

    /*
     * Потокобезопасный Singleton Может возникнуть ситуация, когда из разных потоков происходит
     *  обращение к свойству Singleton Instance, которое должно вернуть ссылку на экземпляр-одиночку 
     *  и при этом экземпляр-одиночка еще не был создан и его поле instance равно null. 
     *  Таким образом, существует риск, что в каждом отдельном потоке будет создан свой экземпляр-одиночка, 
     *  а это противоречит идеологии паттерна Singleton. Избежать такого эффекта возможно через 
     *  инстанцирование класса Singleton в теле критической секции (конструкции lock),
     *   или через использование «объектов уровня ядра операционной системы типа WaitHandle для синхронизации доступа к разделяемым ресурсам».
     */

    internal class MultithreadedSingleton
    {
        // volatile необходим для того, чтобы мы значение не сохранялось в кэше процессора
        // в таком случае у нас всегда будет обращение к памяти напрямую, минуя те данные которые могли закэшироваться

        private static volatile MultithreadedSingleton instance = null;

        private static object syncRoot = new Object();

        private MultithreadedSingleton()
        {
        }

        public static MultithreadedSingleton Instance
        {
            get
            {
                Thread.Sleep(500);
                if (instance == null)
                {
                    lock (syncRoot) // // Закомментировать lock для проверки.
                    {
                        if (instance == null) instance = new MultithreadedSingleton();
                    }
                }
                return instance;
            }
        }
    }

    // что внутри конструкции lock?
    // как устроен внутки yield?

    /*
     * Singleton с отложенной инициализацией.
        Чаще всего метод Instance использует отложенную (ленивую) инициализацию,
        т.е., экземпляр не создается и не хранится вплоть до первого вызова метода
        Instance. Для реализации техники отложенной инициализации в C# рекомендуется воспользоваться классом Lazy<T>, 
        причем по умолчанию экземпляры класса Lazy<T> являются потокобезопасными.
     */


    internal sealed class LazySingleton
    {
        static Lazy<LazySingleton> _instance = new Lazy<LazySingleton>();
        public static LazySingleton Instance => _instance.Value;
    }
}
