﻿using System;

namespace EasyDeploy.Core.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Exception(Exception exception)
        {
            Log(exception.Message); // TODO: better :-)
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
