namespace aoc2022
{
    public class Day06 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            Console.WriteLine($"day6, part1, {FindMarkerEnd(input, 4)}");
            Console.WriteLine($"day6, part2, {FindMarkerEnd(input, 14)}");
        }

        private int FindMarkerEnd(string datastream, int markerLength)
        {
            //simple solution
            //for(int i = 0; i < datastream.Length; i++)
            //{
            //    if (datastream[i..(i + markerLength)].Distinct().Count() == markerLength)
            //    {
            //        return i + markerLength;
            //    }
            //}

            int uniqueCharactersInWindow = 0;
            var charCountsInWindow = new Dictionary<char, int>();

            foreach (var character in datastream[..markerLength])
            {
                if (charCountsInWindow.ContainsKey(character))
                {
                    charCountsInWindow[character] += 1;
                }
                else
                {
                    charCountsInWindow[character] = 1;
                    uniqueCharactersInWindow += 1;
                }
            }

            for (int windowEnd = markerLength; windowEnd < datastream.Length; windowEnd += 1)
            {
                if (charCountsInWindow.ContainsKey(datastream[windowEnd]))
                {
                    charCountsInWindow[datastream[windowEnd]] += 1;
                    
                    if(charCountsInWindow[datastream[windowEnd]] == 1)
                    {
                        uniqueCharactersInWindow += 1;
                    }
                }
                else
                {
                    charCountsInWindow[datastream[windowEnd]] = 1;
                    uniqueCharactersInWindow += 1;
                }

                if (charCountsInWindow.ContainsKey(datastream[windowEnd - markerLength]))
                {
                    charCountsInWindow[datastream[windowEnd - markerLength]] -= 1;

                    if (charCountsInWindow[datastream[windowEnd - markerLength]] == 0)
                    {
                        uniqueCharactersInWindow -= 1;
                    }
                }

                if (uniqueCharactersInWindow == markerLength)
                {
                    Console.WriteLine(new string(datastream[(windowEnd - markerLength + 1)..(windowEnd + 1)]));
                    return windowEnd + 1;
                }
            }

            throw new Exception("Merry Christmas");
        }
    }
}
