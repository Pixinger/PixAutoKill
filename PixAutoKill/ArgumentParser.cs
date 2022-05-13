
namespace PixAutoKill
{
    public class ArgumentParser
    {
        public static string ProcessName(string[] args)
        {
            try
            {
                int c = args.Length;
                for (int i = 0; i < c; i++)
                {
                    if (args[i].ToLower().StartsWith("/name:"))
                    {
                        return args[i].Remove(0, 6);
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static long MemoryLimit(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    if (arg.ToLower().StartsWith("/maxmem:"))
                    {
                        string maxmem = arg.Remove(0, 8);
                        return long.Parse(maxmem);
                    }
                }
                return long.MaxValue;
            }
            catch (Exception)
            {
                return long.MaxValue;
            }
        }
        public static int Timeout(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    if (arg.ToLower().StartsWith("/timeout:"))
                    {
                        string timeout_ms = arg.Remove(0, 9);
                        return int.Parse(timeout_ms);
                    }
                }
                return 5000;
            }
            catch (Exception)
            {
                return 5000;
            }
        }
        public static int Retry(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    if (arg.ToLower().StartsWith("/retry:"))
                    {
                        string retry = arg.Remove(0, 7);
                        return int.Parse(retry);
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static bool Log(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    if (arg.ToLower().StartsWith("/log"))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
