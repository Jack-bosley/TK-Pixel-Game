using System;
using System.IO;
using System.Collections.Generic;
using PixelGame.Core.Enums;
using System.ComponentModel;

namespace PixelGame.Core.Logging
{
    public static class Logger
    {
        public static LogTarget target;

        static Logger()
        {
            target = new LogTarget()
            {
                targetType = LogTargetType.Console,
                fileDirectory = "",
            };
        }


        private static void LogToConsole(string formattedMessage, LogPriority priority)
        {
            Console.ForegroundColor = LogTarget.colours[priority];
            Console.WriteLine(formattedMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void LogToFile(string formattedMessage)
        {
            if (!String.IsNullOrWhiteSpace(target.fileDirectory))
            {
                if (!File.Exists(target.fileDirectory))
                {
                    StreamWriter createdFileStream = File.CreateText(target.fileDirectory);
                    createdFileStream.Close();
                }

                using StreamWriter stream = File.AppendText(target.fileDirectory);
                stream.WriteLine(formattedMessage);
            }
        }


        public static void Log(string message, LogPriority priority = LogPriority.Message)
        {
            string formattedMessage = $"[{EnumHelper.GetDescription(priority)}] ({DateTime.Now}): {message}";

            switch (target.targetType)
            {
                case LogTargetType.Console:
                    LogToConsole(formattedMessage, priority);
                    break;
                case LogTargetType.File:
                    LogToFile(formattedMessage);
                    break;
                case LogTargetType.Both:
                    LogToConsole(formattedMessage, priority);
                    LogToFile(formattedMessage);
                    break;
                case LogTargetType.None:
                default:
                    break;
            }
        }
    
        public static void LogException(Exception exception)
        {
            LogPriority priority = LogPriority.Error;
            if (exception is WarningException)
                priority = LogPriority.Warning;

            string formattedMessage = $"[{EnumHelper.GetDescription(priority)}] ({DateTime.Now}): {exception.Message}\n{exception.StackTrace}";

            switch (target.targetType)
            {
                case LogTargetType.Console:
                    LogToConsole(formattedMessage, priority);
                    break;
                case LogTargetType.File:
                    LogToFile(formattedMessage);
                    break;
                case LogTargetType.Both:
                    LogToConsole(formattedMessage, priority);
                    LogToFile(formattedMessage);
                    break;
                case LogTargetType.None:
                default:
                    break;
            }
        }
    }
}
