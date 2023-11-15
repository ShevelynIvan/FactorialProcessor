using System.Diagnostics;
using System.Threading.Tasks;

namespace FactorialProcessorAPI
{
    public class FactorialProcessor
    {
        /// <summary>
        /// Asynchronously calculates factorials of numbers from 1 to YOUR NUMBER.
        /// </summary>
        /// <param name="param">Number to calculating factorial</param>
        /// <param name="parallelMode">if true - numbers calculating parallel; if false - consistently</param>
        /// <param name="token">Cancellation token</param>
        public async Task GoAsync(int param, bool parallelMode, CancellationToken token)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (param < 1)
                param = 1;

            else if (param > 15)
                param = 15;

            if (parallelMode)
            {
                List<Task<string>> tasks = new List<Task<string>>();

                for (int i = 1; i <= param; i++)
                {
                    int a = i;
                    tasks.Add(Task.Run(() => PrintFactorialAsync(a, token)));
                }

                try
                {
                    await Task.WhenAll(tasks.ToArray());
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Parallel calculating canceled!");
                }
                finally
                {
                    foreach (var task in tasks)
                    {
                        if (task.IsCompletedSuccessfully)
                        {
                            Console.WriteLine($"Task: {task.Result}");
                        }
                        else if (task.IsCanceled)
                        {
                            Console.WriteLine("Task was canceled!");
                        }
                    }
                }

                ShowTimeStatistic(sw);
            }
            else
            {
                for (int i = 1; i <= param; i++)
                {
                    try
                    {
                        var result = await PrintFactorialAsync(i, token);
                        Console.WriteLine(result);
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Calculating factorial for {i} is canceled!");
                    }
                }

                ShowTimeStatistic(sw);
            }
        }

        /// <summary>
        /// Asynchronously calculates factorial of number
        /// </summary>
        /// <param name="param">Number to calculating factorial</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Task<string> String result of factorial</returns>
        private async Task<string> PrintFactorialAsync(int param, CancellationToken token)
        {
            var result = await Task.Run( () => CalculateFactorial(param, token));
            return $"Factorial ({param}) is {result}";
        }

        /// <summary>
        /// Main method that calculates factorial of number 
        /// </summary>
        /// <param name="param">Number to calculating factorial</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Factorial number</returns>
        private int CalculateFactorial(int param, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            Thread.Sleep(1000); 

            if (param == 1)
                return 1;

            return param * CalculateFactorial(param - 1, token);
        }

        /// <summary>
        /// Shows time statistic 
        /// </summary>
        /// <param name="sw">Stopwatch to show data</param>
        private void ShowTimeStatistic(Stopwatch sw)
        {
            sw.Stop();
            Console.WriteLine($"\nTicks: {sw.ElapsedTicks} ticks");
            Console.WriteLine($"Time: {sw.Elapsed.TotalMilliseconds} milliseconds\n");
        }
    }
}