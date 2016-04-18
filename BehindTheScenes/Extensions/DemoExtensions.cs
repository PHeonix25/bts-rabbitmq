using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BehindTheScenes.Extensions
{
    public static class DemoExtensions
    {
        public static void LogToConsole(this Exception exception, string prefix = "")
            => Console.WriteLine($">> {prefix} Exception caught: {exception.GetType()}: {exception.Message}");

        public static string SignificantTicks(this DateTime currentDateTime)
            => new string(currentDateTime.Ticks.ToString()
                                         .Reverse().Take(4)
                                         .Reverse().ToArray());

        public static void PrintNiceMessage(this string message, string direction)
            => Console.WriteLine(
                $"{direction} \t\"{message}\" at " +
                $"{DateTime.UtcNow.SignificantTicks().PadLeft(4, '0')}");
        
    }
}