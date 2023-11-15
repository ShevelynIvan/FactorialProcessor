using FactorialProcessorAPI;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace FactorialProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            FactorialProcessorAPI.FactorialProcessor fp = new FactorialProcessorAPI.FactorialProcessor();

            Task.Run(() => fp.GoAsync(10, true, source.Token), source.Token);
            Task.Run(() => fp.GoAsync(10, false, source.Token), source.Token);

            //for more examples uncomment this lines
            //Task.Run(() => fp.GoAsync(4, false, source.Token), source.Token);
            //Task.Run(() => fp.GoAsync(10, true, source.Token), source.Token);
            
            //for cancellation uncomment this 
            source.CancelAfter(3000);

            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main is Working...");
            }
        }
    }
}