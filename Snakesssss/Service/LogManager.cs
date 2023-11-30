using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Snakesssss.Service
{
    public static class LogMannager
    {
        public static Logger logger { get; } = NLog.LogManager.GetCurrentClassLogger();
    }
}
