namespace PixAutoKill
{
    public static class Logger
    {
        public static bool Enabled { get; set; } = true;

        private const string s_Filename = "PixAutoKill.log";

        public static void Debug(string message)
        {
            string text = $"Debug: {DateTime.Now}: {message}\n";
            Write(text);

            if (!Enabled)
            {
                return;
            }
            Console.Write(text);
        }
        public static void Info(string message)
        {
            string text = $"Info: {DateTime.Now}: {message}\n";
            Write(text);

            if (!Enabled)
            {
                return;
            }
            Console.Write(text);
        }
        public static void Warn(string message)
        {
            string text = $"Warn: {DateTime.Now}: {message}\n";
            Write(text);

            if (!Enabled)
            {
                return;
            }
            Console.Write(text);
        }
        public static void Error(string message)
        {
            string text = $"Error: {DateTime.Now}: {message}\n";
            Write(text);

            if (!Enabled)
            {
                return;
            }
            Console.Write(text);
        }

        private static void Write(string message)
        {
            try
            {
                File.AppendAllText(s_Filename, message);
            }
            catch (Exception ex)
            {
                Enabled = false;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
