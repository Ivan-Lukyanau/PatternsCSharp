namespace LockIt
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {

            int[] b = { 1, 2, 3, 4, 5 };
            MyThread mt1 = new MyThread("Сумма", b);
            MyThread mt2 = new MyThread("Произведение", b);

            mt1.thrd.Join();
            mt2.thrd.Join();

            Console.ReadKey();
        }
    }

    class MyMeths
    {
        int summa;
        int multiply;
        public int SumArr(int[] nums)
        {
            object aa = new object();
            lock (aa)
            {
                summa = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    Console.WriteLine("Промежуточная сумма для потока <" + Thread.CurrentThread.Name + "> равна :" + summa);
                    summa += nums[i];
                }
                return summa;
            }
        }

        public int MultiplyArr(int[] nums)
        {
            object bb = new object();
            lock (bb)
            {
                multiply = 1;
                for (int i = 1; i < nums.Length + 1; i++)
                {
                    Console.WriteLine("Промежуточное произведение для потока <" + Thread.CurrentThread.Name + "> равно :" + multiply);
                    multiply *= i;
                }
                return multiply;
            }
        }
    }

    class MyThread
    {
        int[] a;
        int answer; //сумма
        int mylt; //произведение
        public Thread thrd;
        MyMeths mymet = new MyMeths();

        public MyThread(string name, int[] nums)
        {
            thrd = new Thread(new ThreadStart(Run));
            thrd.Name = name;
            a = nums;
            thrd.Start();
        }

        void Run()
        {
            Console.WriteLine("<" + thrd.Name + "> стартовал");
            if (thrd.Name == "Сумма")
            {
                answer = mymet.SumArr(a);
                Console.WriteLine("Сумма для потока <" + thrd.Name + "> равна " + answer);
            }
            if (thrd.Name == "Произведение")
            {
                mylt = mymet.MultiplyArr(a);
                Console.WriteLine("Произведние для потока <" + thrd.Name + "> равно " + mylt);
            }
            Console.WriteLine("<" + thrd.Name + "> завершён");
        }
    }
}
