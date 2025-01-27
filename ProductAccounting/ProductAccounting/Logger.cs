using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting
{
    public class Logger
    {
        public event Action<string> OnLog;

        public void Log(string message) 
        {
            OnLog?.Invoke($"{DateTime.Now}: {message}");
        }
    }

    public static class PrintLog
    {
        private static readonly string PATH = "";
        public static void PrintToFile(string message, string PATH) 
        {
            //Запись в json файл
        }
    }
}
