using System.Diagnostics;

namespace PixAutoKill
{
    public class Program
    {
        // /name:Notepad
        // /maxmem:4000000  =>(bytes)
        // /timeout:5000
        // /retry:3
        // /log
        public static int Main(string[] args)
        {
            if (ArgumentParser.Log(args))
            {
                Logger.Enabled = true;
            }

            long maxbytes = ArgumentParser.MemoryLimit(args);
            string processName = ArgumentParser.ProcessName(args);
            if (string.IsNullOrWhiteSpace(processName))
            {
                Console.WriteLine("Parameter '/name' is invalid.");
                Console.WriteLine("Sample: PixAutoKill /name:Notepad /maxmem:5000 /timeout:5000 /retry:3");
                return -1;
            }

            int retryCount = ArgumentParser.Retry(args);
            if (retryCount <= 0)
            {
                retryCount = 0;
            }

            int timeout_ms = ArgumentParser.Timeout(args);
            if (timeout_ms < 100)
            {
                timeout_ms = 100;
            }

            Console.WriteLine($"Searching for processes: {processName}. MaxSize={maxbytes}bytes, Retry={retryCount}, Timeout={timeout_ms}ms.");

            Process[] processes = Process.GetProcessesByName(processName);

            List<Process> processList = new List<Process>();
            foreach (Process process in processes)
            {
                if (process.WorkingSet64 > maxbytes)
                {
                    processList.Add(process);
                }
            }

            if (processList.Count > 0)
            {
                Terminator? terminator = new Terminator(retryCount, timeout_ms);
                int countFailed = terminator.Terminate(processList);
                Console.WriteLine($"Terminated {processList.Count - countFailed}/{processList.Count} processes. Have a nice day...");
                return countFailed;
            }

            Console.WriteLine($"No critical processes found. Have a nice day...");
            return 0;
        }
    }
}