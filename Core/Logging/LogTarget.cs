using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PixelGame.Core.Enums;

namespace PixelGame.Core.Logging
{
    public struct LogTarget
    {
        public LogTargetType targetType;
        public string fileDirectory;

        static LogTarget()
        {
            defaultDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            defaultName = "logs.txt";
        }

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
    }

}
