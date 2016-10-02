namespace Volatile
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            //GC.Collect();
            // Create the worker thread object. This doesn't start the tread.
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("Main thread: Worker is starting.");

            // Loop until the worker thread activities.
            while (!workerThread.IsAlive) ;

            //Put the main thread to sleep for 1 sec to
            //allow the worker thread to do some work.
            Thread.Sleep(20);

            // Request the worker thread stops itself.
            workerObject.RequestStop();

            // Use the Thread.Join() method to block the current thread
            // until the object's thread terminates.
            //workerThread.Join();

            Console.WriteLine("Main thread: Worker thread has been terminated.");

            Console.ReadKey();
        }
    }

    internal class Worker
    {
        /*
            *this work is used as hint to the compiler that 
            *the current data member is accessed by multiply threads.
        */

        private volatile bool _shouldStop;
        //private bool _shouldStop;

        public void DoWork()
        {
            Console.WriteLine("Worker thread : Start DoWork method.");
            while (!_shouldStop)
            {
                //Console.WriteLine("Worker thread : working..."); 
                // when we use console withing the loop it blocks the logic standart behavior of the volatile KW

                Debug.Write("Worker works...");
            }
            Console.WriteLine("Worker thread : DoWork is terminating gracefully.");
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}
