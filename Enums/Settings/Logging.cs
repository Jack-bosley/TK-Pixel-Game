using System;
using System.ComponentModel;

namespace PixelGame.Core.Enums
{
    public enum LogTargetType
    {
        None = 0,
        File = 1,
        Console = 2,
        Both = 3,
    }

    public enum LogPriority
    {
        [Description("Msg")]
        Message = 0,
        [Description("Wng")]
        Warning = 1,
        [Description("Err")]
        Error = 2,
    }
}
