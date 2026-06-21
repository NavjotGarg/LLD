using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerSingltonMultithread
{
    public class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();
        private int lineCounter = 0;
        private Logger()
        {
            // Private constructor to prevent instantiation
            Console.WriteLine("Logger object created");
        }
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }
        public void Log(string message)
        {
            int line = Interlocked.Increment(ref lineCounter) - 1;
            Console.WriteLine($"Log: {message} on line:{lineCounter}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    Logger logger = Logger.Instance;
                    Console.WriteLine($"Logger instance in thread {Thread.CurrentThread.ManagedThreadId}");
                    logger.Log($"Logging from thread {Thread.CurrentThread.ManagedThreadId}");
                });
                threads[i].Start();
            }
        }
    }
}
