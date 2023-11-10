using System.Diagnostics;

namespace FactorialProcessorAPI
{
    public static class FactorialProcessor
    {
        /// <summary>
        /// list of treads that calculating factorials of numbers
        /// </summary>
        private static List<Thread> _threads = new List<Thread>();

        /// <summary>
        /// Calculates factorials of numbers from 1 to YOUR NUMBER.
        /// </summary>
        /// <param name="param">Number to calculating factorial</param>
        /// <param name="parallelMode">if true - numbers calculating parallel; if false - consistently</param>
        public static void Go(int param, bool parallelMode)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (param < 1)
                param = 1;

            else if (param > 15)
                param = 15;


            if (parallelMode)
            {
                for (int i = 1; i <= param; i++)
                {
                    CalculateFactorialParallel(i);
                }

                WaitAll();
                ShowTimeStatistic(sw);
            }
            else
            {
                for (int i = 1; i <= param; i++)
                {
                    Console.Write($"{CalculateFactorial(i)}\t");
                }

                ShowTimeStatistic(sw);
            }
        }

        /// <summary>
        /// Creates new thread to calculate factorial of number
        /// </summary>
        /// <param name="param"></param>
        private static void CalculateFactorialParallel(int param)
        {
            Thread thread = new Thread(() => Console.Write($"{CalculateFactorial(param)}\t"));
            _threads.Add(thread);
            thread.Start();
        }

        /// <summary>
        /// Main method that calculates factorial of number 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static int CalculateFactorial(int param)
        {
            //Thread.Sleep(1); //it can show us, that if there was a lot of hard calculating,
                                //it would make sense for us to create a separate thread
                                //for each calculation of the factorial of a number
                                //but in our case we spend a lot of resources to create a new thread 
                                //and it not only doesn't make sense, it also makes things worse. 
                                //Consistent algorithm works faster than parallel in our case. 
            if (param == 1)
                return 1;

            return param * CalculateFactorial(param - 1);
        }

        /// <summary>
        /// Shows time statistic 
        /// </summary>
        /// <param name="sw"></param>
        private static void ShowTimeStatistic(Stopwatch sw)
        {
            sw.Stop();
            Console.WriteLine($"\nTicks: {sw.ElapsedTicks} ticks");
            Console.WriteLine($"Time: {sw.Elapsed.TotalMilliseconds} milliseconds\n");
        }

        /// <summary>
        /// Blocks main thread to wait all backgroung threads that calculating factorials. Used in Go(int ..., true)
        /// </summary>
        private static void WaitAll()
        {
            foreach (var thread in _threads)
            {
                thread.Join();
            }
        }
    }
}