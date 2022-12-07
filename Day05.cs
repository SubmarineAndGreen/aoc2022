namespace aoc2022
{
    public class Day05 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            var splitInput = input.Split("\r\n\r\n");
            var stacksInput = splitInput[0].Split("\r\n");

            var stacks1 = Enumerable.Range(0, 9).Select(_ => new Stack<char>()).ToArray();
            
            for(int row = stacksInput.Length - 2; row >= 0; row--)
            {
                const int distanceBetweenColumns = 4;
                for (int column = 1, stackIndex = 0; column < stacksInput[row].Length; column += distanceBetweenColumns, stackIndex++)
                {
                    if(!char.IsWhiteSpace(stacksInput[row][column]))
                    {
                        stacks1[stackIndex].Push(stacksInput[row][column]);
                    }
                }
            }

            var stacks2 = stacks1.Select(x => new Stack<char>(x.Reverse())).ToArray();

            var moves = splitInput[1].Split("\r\n")
                                     .Select(x => x.Split(" "))
                                     .Select(x => new string[] { x[1], x[3], x[5] }.Select(x => int.Parse(x)).ToArray())
                                     .Select(x => new Move
                                     {
                                         Amount = x[0],
                                         Source = x[1],
                                         Target = x[2]
                                     }); 

            foreach(var move in moves)
            {
                var sourceStack = stacks1[move.Source - 1];
                var targetstack = stacks1[move.Target - 1];

                for(int i = 0; i < move.Amount; i++)
                {
                    targetstack.Push(sourceStack.Pop());
                }
            }

            foreach (var move in moves)
            {
                var sourceStack = stacks2[move.Source - 1];
                var targetstack = stacks2[move.Target - 1];
                var liftedCrates = new Stack<char>();

                for (int i = 0; i < move.Amount; i++)
                {
                    liftedCrates.Push(sourceStack.Pop());
                }

                while(liftedCrates.TryPop(out var crate))
                {
                    targetstack.Push(crate);
                }
            }

            var tops1 = string.Join(string.Empty, stacks1.Select(x => x.Peek()));
            var tops2 = string.Join(string.Empty, stacks2.Select(x => x.Peek()));
            Console.WriteLine($"day5, part1, {tops1}");
            Console.WriteLine($"day5, part1, {tops2}");
        }

        private class Move
        {
            public int Amount;
            public int Source;
            public int Target;
        }
    }
}
