using System.Linq;

namespace aoc2022
{
    public class Day03 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            var rucksacks = input.Split("\r\n");
            int totalDuplicateItemPriorities = 0;

            foreach (var rucksack in rucksacks)
            {
                int leftPointer = 0;
                int rightPointer = rucksack.Length - 1;
                var leftCompartmentItemTypes = new HashSet<char>();
                var rightCompartmentItemTypes = new HashSet<char>();

                while (leftPointer <= rightPointer)
                {
                    leftCompartmentItemTypes.Add(rucksack[leftPointer]);
                    rightCompartmentItemTypes.Add(rucksack[rightPointer]);

                    leftPointer += 1;
                    rightPointer -= 1;
                }

                leftCompartmentItemTypes.IntersectWith(rightCompartmentItemTypes);

                totalDuplicateItemPriorities += leftCompartmentItemTypes.Sum(x => GetItemPriority(x));
            }

            Console.WriteLine($"day3, part1 {totalDuplicateItemPriorities}");

            int totalBadgePriorities = 0;

            for (int i = 0; i < rucksacks.Length; i += 3)
            {
                var commonType = rucksacks[i].Intersect(rucksacks[i + 1]).Intersect(rucksacks[i + 2]).Single();
                totalBadgePriorities += GetItemPriority(commonType);
            }

            Console.WriteLine($"day3, part2 {totalBadgePriorities}");
        }

        public static int GetItemPriority(char item)
        {
            return char.IsLower(item) ? 1 + item - 'a' : 27 + item - 'A';
        }
    }
}
