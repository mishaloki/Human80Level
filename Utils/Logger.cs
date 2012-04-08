using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Human80Level.Utils
{
    public static  class Logger
    {
        private static readonly string LoggerMessageFormat = "method: {0}, message: {1}";
        
        public static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("----------DEBUG------------");
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine("-------------");
        }

        public static void Debug(string method, string message)
        {
            System.Diagnostics.Debug.WriteLine("----------DEBUG------------");
            System.Diagnostics.Debug.WriteLine(string.Format(LoggerMessageFormat,method,message));
            System.Diagnostics.Debug.WriteLine("-------------");
        }

        public static void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine("----------ERROR------------");
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine("-------------");
        }

        public static void Error(string method, string message)
        {
            System.Diagnostics.Debug.WriteLine("----------ERROR------------");
            System.Diagnostics.Debug.WriteLine(string.Format(LoggerMessageFormat, method, message));
            System.Diagnostics.Debug.WriteLine("-------------");
        }

        public static void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine("----------INFO------------");
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine("-------------");
        }

        public static void Info(string method, string message)
        {
            System.Diagnostics.Debug.WriteLine("----------INFO------------");
            System.Diagnostics.Debug.WriteLine(string.Format(LoggerMessageFormat, method, message));
            System.Diagnostics.Debug.WriteLine("-------------");
        }
    }
}
