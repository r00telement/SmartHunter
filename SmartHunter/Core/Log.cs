using System;
using System.IO;

namespace SmartHunter.Core
{
    public static class Log
    {
        static string s_FileName = "Log.txt";

        public static event EventHandler<GenericEventArgs<string>> LineReceived;

        public static void WriteLine(string message)
        {
            string line = String.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}", DateTimeOffset.Now.ToUniversalTime(), message);
            Console.WriteLine(line);

            if (LineReceived != null)
            {
                LineReceived(null, new GenericEventArgs<string>(line));
            }

            bool isDesignInstance = System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime;
            if (!isDesignInstance)
            {
                File.AppendAllText(s_FileName, $"{line}\r\n");
            }
        }

        public static void WriteException(Exception exception)
        {
            WriteLine($"{exception.GetType().Name}: {exception.Message}\r\n{exception.StackTrace}");
        }
    }
}