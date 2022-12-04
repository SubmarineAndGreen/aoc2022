namespace aoc2022
{
    public class Day04 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            int oneRangeFullyContainsOtherCount = 0;
            int oneRangePartiallyContainsOtherCount = 0;

            foreach (var pair in input.Split("\r\n"))
            {
                var ranges = pair.Split(",").Select(range => range.Split("-").Select(x => int.Parse(x)).ToArray()).ToArray();

                if(((ranges[0][0] - ranges[1][0]) * (ranges[0][1] - ranges[1][1])) <= 0)
                {
                    oneRangeFullyContainsOtherCount += 1;
                }

                if (((ranges[0][0] - ranges[1][1]) * (ranges[0][1] - ranges[1][0])) <= 0)
                {
                    oneRangePartiallyContainsOtherCount += 1;
                }
            }

            Console.WriteLine($"day4, part1 {oneRangeFullyContainsOtherCount}");
            Console.WriteLine($"day4, part2 {oneRangePartiallyContainsOtherCount}");
        }
    }
}
