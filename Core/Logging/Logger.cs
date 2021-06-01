using System;
using System.IO;
using System.Collections.Generic;
using PixelGame.Core.Enums;
using System.ComponentModel;

namespace PixelGame.Core.Logging
{
    public static class Logger
    {
        public static readonly Dictionary<LogPriority, ConsoleColor> colours = new Dictionary<LogPriority, ConsoleColor>()
        {
            { LogPriority.Message, ConsoleColor.White },
            { LogPriority.Warning, ConsoleColor.Yellow },
            { LogPriority.Error, ConsoleColor.Red }
        };
        public static readonly string defaultDirectory;
        public static readonly string defaultName;
        public static string DefaultPath
        {
            get => $"{defaultDirectory}/{defaultName}";
        }
        public static string TargetDirectory { get; set; } = defaultDirectory;
        public static string TargetName { get; set; } = defaultName;
        public static string TargetPath { get; set; } = DefaultPath;

        static Logger()
        {
            defaultDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            defaultName = "logs.txt";
        }

        private static string GetFormattedMessage(string message, LogPriority priority)
        {
            return $"[{EnumHelper.GetDescription(priority)}] ({DateTime.Now.TimeOfDay}): {message}";
        }
        private static string GetFormattedMessage(Exception exception, LogPriority priority)
        {
            return $"[{EnumHelper.GetDescription(priority)}] ({DateTime.Now.TimeOfDay}): {exception.Message}\n{exception.StackTrace}";
        }
        private static void LogToConsole(string formattedMessage, LogPriority priority)
        {
            Console.ForegroundColor = colours[priority];
            Console.WriteLine(formattedMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void LogToFile(string formattedMessage)
        {
            if (!String.IsNullOrWhiteSpace(TargetPath))
            {
                if (!File.Exists(TargetPath))
                {
                    StreamWriter createdFileStream = File.CreateText(TargetPath);
                    createdFileStream.Close();
                }

                try
                {
                    using StreamWriter stream = File.AppendText(TargetPath);
                    stream.WriteLine(formattedMessage);
                }
                catch 
                {
                    // failed to write 
                }
            }
        }


        public static void Log(string message, LogPriority priority = LogPriority.Message)
        {
            string formattedMessage = GetFormattedMessage(message, priority);
            LogToConsole(formattedMessage, priority);
            LogToFile(formattedMessage);
        }
    
        public static void Log(Exception exception)
        {
            LogPriority priority = LogPriority.Error;
            if (exception is WarningException)
                priority = LogPriority.Warning;

            string formattedMessage = GetFormattedMessage(exception, priority);
            LogToConsole(formattedMessage, priority);
            LogToFile(formattedMessage);
        }

        public static void LogConsole(string message, LogPriority priority = LogPriority.Message)
        {
            string formattedMessage = GetFormattedMessage(message, priority);
            LogToConsole(formattedMessage, priority);
        }
        public static void LogConsole(Exception exception)
        {
            LogPriority priority = LogPriority.Error;
            if (exception is WarningException)
                priority = LogPriority.Warning;

            string formattedMessage = GetFormattedMessage(exception, priority);
            LogToConsole(formattedMessage, priority);
        }

        public static void LogFile(string message, LogPriority priority = LogPriority.Message)
        {
            LogToFile(GetFormattedMessage(message, priority));
        }
        public static void LogFile(Exception exception)
        {
            LogPriority priority = LogPriority.Error;
            if (exception is WarningException)
                priority = LogPriority.Warning;

            LogToFile(GetFormattedMessage(exception, priority));
        }
    }
}
