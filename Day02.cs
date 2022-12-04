using static aoc2022.Day02;

namespace aoc2022
{
    public class Day02 : AdventOfCodeDay
    {
        public enum Shape
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2,
        }

        public enum RockPaperScissorsResult
        {
            Loss = 0,
            Draw = 1,
            Win = 2,
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

        public override void Run(string input)
        {
            var strategyGuide1 = input.Split("\r\n").Select(x => x.Split(" ").Select(x => ShapeFromString(x)).ToArray());
            var totalPoints = strategyGuide1.Aggregate(0, (totalPoints, round) => totalPoints = totalPoints + RoundToPoints(round[0], round[1]) + round[1].ToPoints());
            Console.WriteLine($"day2 part1 {totalPoints}");

            var strategyGuide2 = input.Split("\r\n").Select(x => x.Split(" ")).Select(x => (ShapeFromString(x[0]), RockPaperScissorsResultFromString(x[1])));
            var totalPoints2 = strategyGuide2.Aggregate(0, (totalPoints, strategy) => totalPoints = totalPoints + StrategyToShapePoints(strategy.Item1, strategy.Item2) + strategy.Item2.ToPoints());
            Console.WriteLine($"day2 part2 {totalPoints2}");
        }
    }

    public static class Day02Extensions
    {
        public static int ToPoints(this RockPaperScissorsResult rockPaperScissorsResult)
        {
            return rockPaperScissorsResult switch
            {
                RockPaperScissorsResult.Loss => 0,
                RockPaperScissorsResult.Draw => 3,
                RockPaperScissorsResult.Win => 6,
                _ => throw new Exception("Merry Christmas!"),
            };
        }

        public static int ToPoints(this Shape shape)
        {
            return shape switch
            {
                Shape.Rock => 1,
                Shape.Paper => 2,
                Shape.Scissors => 3,
                _ => throw new Exception("Merry Christmas!"),
            };
        }
    }
}
