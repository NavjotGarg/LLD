using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorExample
{
    class ILogger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    class ConsoleLogger : ILogger
    {
        public override void Log(string message)
        {
            Console.WriteLine($"ConsoleLogger: {message}");
        }
    }

    class FileLogger : ILogger
    {
        public override void Log(string message)
        {
            // Simulate writing to a file
            Console.WriteLine($"FileLogger: {message}");
        }
    }

    abstract class LoggerDecorator : ILogger
    {
        protected ILogger _logger;
        public LoggerDecorator(ILogger logger)
        {
            _logger = logger;
        }
        public override void Log(string message)
        {
            _logger.Log(message);
        }
    }

    class TimestampLogger : LoggerDecorator
    {
        public TimestampLogger(ILogger logger) : base(logger) { }
        public override void Log(string message)
        {
            string timestampedMessage = $"[{DateTime.Now}] {message}";
            base.Log(timestampedMessage);
        }
    }
    internal class DecoratorExample
    {
        static void Main(string[] args)
        {
            ILogger logger = new TimestampLogger(new ConsoleLogger());

            logger.Log("This is a log message with a timestamp.");
        }
    }
}
