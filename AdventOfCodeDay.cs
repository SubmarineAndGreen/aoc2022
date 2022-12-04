namespace aoc2022
{
    public abstract class AdventOfCodeDay
    {
        private static readonly string _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        public abstract void Run(string input);

        public void Run()
        {
            Run(File.ReadAllText(Path.Combine(_dataDirectory, $"{GetType().Name.ToLower()}.txt")));
        }
    }
}
