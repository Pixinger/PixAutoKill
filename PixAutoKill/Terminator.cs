using System.Diagnostics;

namespace PixAutoKill
{
    internal class Terminator
    {
        private int _retryCount;
        private readonly int _enforcedTerminationDelay_ms;

        public Terminator(int retryCount, int enforcedTerminationDelay_ms)
        {
            _retryCount = retryCount;
            if (_retryCount < 0)
            {
                _retryCount = 0;
            }

            _enforcedTerminationDelay_ms = enforcedTerminationDelay_ms;
        }

        public Task<bool> Terminate(Process process)
        {
            Console.WriteLine($"Process '{process.ProcessName}/{process.Id}' exceeded memory limit with {process.WorkingSet64} bytes.");

            return Task.Run<bool>(() =>
            {
                try
                {
                    string title = $"{process.ProcessName}/{process.Id}";

                    // Enforced termination
                    _retryCount++; // It's called "re"tryCount not "try"Count.
                    while (_retryCount-- > 0)
                    {
                        Console.WriteLine($"{title}: - Terminating ... ");
                        process.Kill(true);
                        long tick = Environment.TickCount64 + _enforcedTerminationDelay_ms;
                        while ((!process.HasExited) && (Environment.TickCount64 < tick))
                        {
                            Thread.Sleep(100);
                        }

                        if (process.HasExited)
                        {
                            Console.WriteLine($"{title}: - Terminating OK.");
                            return true;
                        }

                        Console.WriteLine($"{title}: - Terminating FAILED.");
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected exception: {ex.Message}");
                    return false;
                }
            });
        }

        public int Terminate(List<Process>? processes)
        {
            try
            {
                if ((processes is null) || (processes.Count == 0))
                {
                    return 0;
                }

                int i = 0;
                Task<bool>[] tasks = new Task<bool>[processes.Count];
                foreach (Process? process in processes)
                {
                    tasks[i++] = Terminate(process);
                }

                Task.WaitAll();

                int countFailed = 0;
                foreach (Task<bool>? task in tasks)
                {
                    if (!task.Result)
                    {
                        countFailed++;
                    }
                }

                return countFailed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: ", ex.Message);
                return 0;
            }
        }
    }
}
