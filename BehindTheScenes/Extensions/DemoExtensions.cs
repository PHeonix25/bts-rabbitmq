using System;
using System.Linq;

namespace BehindTheScenes.Extensions
{
    public static class DemoExtensions
    {
        public static int SignificantTicks(this DateTime currentDateTime)
            => int.Parse(new string(currentDateTime.Ticks.ToString().Reverse().Skip(2).Take(4).Reverse().ToArray()));

        public static void PrintNiceMessage(this string message, string direction)
            =>
                Console.WriteLine(
                    $" {direction} \t\"{message}\" \tat {DateTime.UtcNow.SignificantTicks().ToString().PadLeft(4, '0')}");
    }
}