using FactorialProcessorAPI;
using System.Diagnostics;

namespace FactorialProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consistently:");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            FactorialProcessorAPI.FactorialProcessor.Go(5, false);
            FactorialProcessorAPI.FactorialProcessor.Go(10, false);
            FactorialProcessorAPI.FactorialProcessor.Go(15, false);
            
            sw.Stop();
            Console.WriteLine($"ALL TICKS: {sw.ElapsedTicks} ticks");
            Console.WriteLine($"ALL TIME: {sw.Elapsed.TotalMilliseconds} milliseconds");
            Console.WriteLine();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Parallel:");

            sw.Reset();
            sw.Start();
            FactorialProcessorAPI.FactorialProcessor.Go(5, true);
            FactorialProcessorAPI.FactorialProcessor.Go(10, true);
            FactorialProcessorAPI.FactorialProcessor.Go(15, true);

            sw.Stop();
            Console.WriteLine($"ALL TICKS IN PARALLEL: {sw.ElapsedTicks} ticks");
            Console.WriteLine($"ALL TIME IN PARALLEL: {sw.Elapsed.TotalMilliseconds} milliseconds");
        }
    }
}