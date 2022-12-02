
//AdventOfCode2022.Day01();
AdventOfCode2022.Day02();
Console.ReadLine();

public static class AdventOfCode2022
{
    private static string _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

    public static void Day01()
    {
        var input = File.ReadAllText(Path.Combine(_dataDirectory, "day01.txt"));
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

    private enum Shape
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
    }

    private enum RockPaperScissorsResult
    {
        Loss = 0,
        Draw = 1,
        Win = 2,
    }

    private static int ToPoints(this RockPaperScissorsResult rockPaperScissorsResult)
    {
        return rockPaperScissorsResult switch
        {
            RockPaperScissorsResult.Loss => 0,
            RockPaperScissorsResult.Draw => 3,
            RockPaperScissorsResult.Win => 6,
            _ => throw new Exception("Merry Christmas!"),
        };
    }

    private static int ToPoints(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => throw new Exception("Merry Christmas!"),
        };
    }

    private static RockPaperScissorsResult RockPaperScissorsResultFromString(string c)
    {
        return c switch
        {
            "X" => RockPaperScissorsResult.Loss,
            "Y" => RockPaperScissorsResult.Draw,
            "Z" => RockPaperScissorsResult.Win,
            _ => throw new Exception("Merry Christmas!"),
        };
    }

    private static Shape ShapeFromString(string c)
    {
        return c switch
        {
            "A" or "X" => Shape.Rock,
            "B" or "Y" => Shape.Paper,
            "C" or "Z" => Shape.Scissors,
            _ => throw new Exception("Merry Christmas!"),
        };
    }

    private static readonly int[,] _roundToPoints = new int[3, 3]
    {
   /*     R  P  S <-- you*/ 
   /*R*/{ 3, 6, 0 },
   /*P*/{ 0, 3, 6 },
   /*S*/{ 6, 0, 3 },
    };

    private static readonly Shape[,] _shapeFromStrategy = new Shape[3, 3]
    {
   /*       Lose             Draw             Win         <-- required result*/ 
   /*R*/{ Shape.Scissors, Shape.Rock,     Shape.Paper },
   /*P*/{ Shape.Rock,     Shape.Paper,    Shape.Scissors },
   /*S*/{ Shape.Paper,    Shape.Scissors, Shape.Rock },
    };

    private static int RoundToPoints(Shape elf, Shape me)
    {
        return _roundToPoints[(int)elf, (int)me];
    }

    private static int StrategyToShapePoints(Shape elf, RockPaperScissorsResult requiredResult)
    {
        return _shapeFromStrategy[(int)elf, (int)requiredResult].ToPoints();
    }

    public static void Day02()
    {
        var input = File.ReadAllText(Path.Combine(_dataDirectory, "day02.txt"));
        var strategyGuide1 = input.Split("\r\n").Select(x => x.Split(" ").Select(x => ShapeFromString(x)).ToArray());
        var totalPoints = strategyGuide1.Aggregate(0, (totalPoints, round) => totalPoints = totalPoints + RoundToPoints(round[0], round[1]) + round[1].ToPoints());
        Console.WriteLine($"day2 part1 {totalPoints}");

        var strategyGuide2 = input.Split("\r\n").Select(x => x.Split(" ")).Select(x => (ShapeFromString(x[0]), RockPaperScissorsResultFromString(x[1])));
        var totalPoints2 = strategyGuide2.Aggregate(0, (totalPoints, strategy) => totalPoints = totalPoints + StrategyToShapePoints(strategy.Item1, strategy.Item2) + strategy.Item2.ToPoints());
        Console.WriteLine($"day2 part2 {totalPoints2}");
    }
}


//day2


