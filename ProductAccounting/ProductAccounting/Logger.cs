using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting
{
    public class Logger
    {
        public delegate void Print(string message);
        public static event Print wasChanged;

        public static void PrintChangesToFile(string message) 
        {
           string path = "Changes.txt";
           File.AppendAllText(path, message);
        }

        public static void Log(string message)
        {
            wasChanged?.Invoke(message);
        } 
    }

}
