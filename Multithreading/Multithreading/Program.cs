namespace Multithreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var speedTest = new SpeedTest(5);;

            speedTest.Run(SequentiaSum);
            speedTest.Run(ThreadSum);
            speedTest.Run(ParalelePlinqSum);

            speedTest.PrintResults();

        }

        static long SequentiaSum(int[] arr)
        {
            long sum = 0;
            foreach (int i in arr)
            {
                sum += i;
            }
            return sum;
        }

        static long ParalelePlinqSum(int[] arr)
        {
            return arr.AsParallel().Sum(x => (long)x);
        }

        static long ThreadSum(int[] arr)
        {
            long totalSum = 0;
            object lockObject = new object();
            int numThreads = 4;
            int chunkSize = arr.Length / numThreads;
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numThreads; i++)
            {
                int start = i * chunkSize;
                int end = (i == numThreads - 1) ? arr.Length : start + chunkSize;

                Thread thread = new Thread(() => SumPartial(start, end));
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return totalSum;

            void SumPartial(int start, int end)
            {
                long localSum = 0;
                for (int i = start; i < end; i++)
                {
                    localSum += arr[i];
                }

                lock (lockObject)
                {
                    totalSum += localSum;
                }
            }
        }
    }
}
