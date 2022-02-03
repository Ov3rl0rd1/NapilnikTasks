using System;
using System.Collections.Generic;
using System.IO;

namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstPathfinder = new Pathfinder(new FileLogWritter());
            var secondPathfinder = new Pathfinder(new ConsoleLogWritter());
            var thirdPathfinder = new Pathfinder(new SecureLogWritter(new FileLogWritter()));
            var fourthPathfinder = new Pathfinder(new SecureLogWritter(new ConsoleLogWritter()));
            var fifthPathfinder = new Pathfinder(ChainOfLoggers.Create(new ConsoleLogWritter(), new SecureLogWritter(new FileLogWritter())));
        }
    }

    class Pathfinder
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            _logger.WriteError(new Random().Next(0, 100).ToString());
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }

    class ConsoleLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureLogWritter : ILogger
    {
        private ILogger _baseLogger;

        public SecureLogWritter(ILogger baseLogger)
        {
            _baseLogger = baseLogger;
        }

        public virtual void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _baseLogger.WriteError(message);
            }
        }
    }

    class ChainOfLoggers : ILogger
    {
        private IEnumerable<ILogger> _loggers;

        public ChainOfLoggers(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void WriteError(string message)
        {
            foreach (var logger in _loggers)
                logger.WriteError(message);
        }

        public static ChainOfLoggers Create(params ILogger[] loggers)
        {
            return new ChainOfLoggers(loggers);
        }
    }
}
