namespace Volatile.ReadWrite
{
    using System;
    using System.Threading;

    class Program
    {
        private static int stop;

        static void Main(string[] args)
        {
            Thread thread = new Thread(Function);
            thread.Start();

            Thread.Sleep(2000);

            Thread.VolatileWrite(ref stop, 1); // stop = 1;

            Console.WriteLine("Main: ожидание завершения вторичного потока.");
            thread.Join();
        }

        private static void Function()
        {
            int x = 0;

            while (Thread.VolatileRead(ref stop) != 1)
            {
                x++;
            }

            Console.WriteLine("Function: поток остаовлен при x = {0}.", x);
        }
    }
}
