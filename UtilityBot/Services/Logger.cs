﻿namespace UtilityBot.Services
{
    public class Logger : ISimpleLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
