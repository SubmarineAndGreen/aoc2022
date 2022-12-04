namespace aoc2022
{
    public class Day01 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            var totalCaloriesPerElf = input.Split("\r\n\r\n")
                                           .Select(x => x.Split("\r\n")
                                                         .Select(x => int.Parse(x)))
                                           .Select(x => x.Sum());

            var mostCalories = totalCaloriesPerElf.Max();
            var top3sum = totalCaloriesPerElf.OrderBy(x => x).TakeLast(3).Sum();

            Console.WriteLine($"day1 part1: {mostCalories}");
            Console.WriteLine($"day1 part2: {top3sum}");

            //alternate solution no sorting
            var top3 = new int[3];
            var leastCaloriesInTop3Index = 0;

            foreach (var totalCalories in totalCaloriesPerElf)
            {
                if (totalCalories > top3[leastCaloriesInTop3Index])
                {
                    top3[leastCaloriesInTop3Index] = totalCalories;

                    var leastCaloriesInTop3 = top3[0];
                    for (int i = 0; i < top3.Length; i++)
                    {
                        if (top3[i] <= leastCaloriesInTop3)
                        {
                            leastCaloriesInTop3 = top3[i];
                            leastCaloriesInTop3Index = i;
                        }
                    }
                }
            }

            var top3sum2 = top3.Sum();
        }
    }
}
