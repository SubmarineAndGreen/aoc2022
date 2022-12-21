namespace aoc2022
{
    public class Day07 : AdventOfCodeDay
    {
        private Directory? _directoryTreeRoot, _currentDirectory;
        private readonly List<Directory> _allDirectories = new();

        public override void Run(string input)
        {
            _directoryTreeRoot = new Directory(null);
            _currentDirectory = _directoryTreeRoot;
            _allDirectories.Add(_directoryTreeRoot);

            foreach (var line in input.Split("\r\n"))
            {
                switch (line.Split(" "))
                {
                    case ["$", "cd", var directory]:
                        ChangeDirectory(directory);
                        break;
                    case ["$", "ls"] or ["dir", _]:
                        break;
                    case [var fileSize, _]:
                        _currentDirectory.IncrementSize(int.Parse(fileSize));
                        break;
                    default:
                        throw new Exception("Merry Christmas!");
                }
            }

            var directoriesUnder100kTotalSize = _allDirectories.Where(x => x.Size <= 100_000).Sum(x => x.Size);
            Console.WriteLine($"day07, part01, {directoriesUnder100kTotalSize}");
            var currentFreeSpace = 70_000_000 - _directoryTreeRoot.Size;
            var smallestDirectoryToDeleteSize = _allDirectories.Where(x => currentFreeSpace + x.Size >= 30_000_000).Min(x => x.Size);
            Console.WriteLine($"day07, part02, {smallestDirectoryToDeleteSize}");
        }

        private void ChangeDirectory(string directory)
        {
            switch (directory) {
                case "/":
                    _currentDirectory = _directoryTreeRoot;
                    break;
                case "..":
                    _currentDirectory = _currentDirectory?.Parent;
                    break;
                default:
                    var newDirectory = new Directory(_currentDirectory);
                    _allDirectories.Add(newDirectory);
                    _currentDirectory = newDirectory;
                    break;
            }
        }

        private class Directory
        {
            public Directory? Parent;
            public int Size { get; private set; } = 0;

            public Directory(Directory? parent)
            {
                Parent = parent;
            }

            public void IncrementSize(int fileSize)
            {
                Size += fileSize;
                Parent?.IncrementSize(fileSize);
            }
        }
    }
}
