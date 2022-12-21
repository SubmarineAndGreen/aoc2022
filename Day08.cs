using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace aoc2022
{
    internal class Day08 : AdventOfCodeDay
    {
        public override void Run(string input)
        {
            var trees = input.Split("\r\n").Select(x => x.Select(x1 => new Tree(int.Parse(x1.ToString()))).ToArray()).ToArray();
            var trees2 = trees.Select(x => x.Reverse());
            var trees3 = trees.Transpose();
            var trees4 = trees.Transpose().Select(x => x.Reverse());

            foreach (var treeMartix in new[] { trees, trees2, trees3, trees4 })
            {
                foreach (var treeRow in treeMartix)
                {
                    int lastHeight = -1;

                    foreach(var tree in treeRow)
                    {
                        if (tree.Height < lastHeight)
                        {
                            break;
                        } 

                        tree.Visible |= !(tree.Height == lastHeight);
                        lastHeight = tree.Height;
                    }
                }
            }

            var visibleTreeCount = trees.SelectMany(x => x).Where(x => x.Visible).Count();
            Console.WriteLine($"day08, part1 {visibleTreeCount}");
        }

        private class Tree
        {
            public int Height;
            public bool Visible = false;

            public Tree(int height)
            {
                Height = height;
            }
        }
    }

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            return collection.SelectMany(x => x.Select((item, index) => new { Item = item, Index = index }))
                             .GroupBy(x => x.Index, x => x.Item)
                             .Select(x => x.AsEnumerable());
        }
    }
}
