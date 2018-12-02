using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PuzzleCommon;

namespace Day2
{
    public class Day2Solver : IPuzzleSolver
    {
        private readonly string _inputFilePath;

        public Day2Solver(string inputFilePath) {
            this._inputFilePath = inputFilePath;
        }

        public IPuzzleOutput Solve()
        {
            string[] inputLines = File.ReadAllLines(_inputFilePath);
            var part1Solution = SolvePart1(inputLines);
            var part2Solution = SolvePart2(inputLines);

            return new TwoPartPuzzleOutput(part1Solution, part2Solution);
        }

        private IPuzzleOutput SolvePart1(string[] inputLines) {
            var nTwosnThrees = inputLines.Select((line) => {
                 LineHasTwoAndThreeLettersTheSame(line, out bool foundTwo, out bool foundThree);
                 return (foundTwo, foundThree);
            });
            int nTwos = nTwosnThrees.Count(pair => pair.Item1);
            int nThrees = nTwosnThrees.Count(pair => pair.Item2);

            return new Day2Part1SolverOutput(nTwos * nThrees);
        }

        private IPuzzleOutput SolvePart2(string[] inputLines) {
            var sortedInputLines = inputLines.Clone() as string[];
            Array.Sort(sortedInputLines);

            for(int y = 0; y < sortedInputLines.Length; ++y) {

                for(int x = 0; x < sortedInputLines.Length; ++x) {
                    long diff = (x == y ? 0 : DiffBy(sortedInputLines[x], sortedInputLines[y]));

                    if (diff == 1) {
                        return new Day2Part2SolverOutput(CommonCharacters(sortedInputLines[x], sortedInputLines[y]));
                    }
                }
            }

            return null;
        }

        private string CommonCharacters(string a, string b) {
            string same = "";

            int iA = 0;
            int iB = 0;

            while(iA < a.Length && iB < b.Length) {
                if (a[iA] == b[iB]) {
                    same += a[iA];

                    ++iA;
                    ++iB;
                } else if(iB + 1 < b.Length && a[iA] == b[iB + 1]){
                    ++iB;
                } else if(iA + 1 < a.Length && a[iA+1] == b[iB]) {
                    ++iA;
                } else {
                    ++iA;
                    ++iB;
                }
            }

            return same;
        }

        private long DiffBy(string strA, string strB) {
            int diff = 0;
            
            int maxLength = Math.Min(strA.Length, strB.Length);
            for(int i = 0; i < maxLength; ++i) {
                if (strA[i] != strB[i]) {
                    ++diff;
                }
            }

            return diff;
        }

        private (bool, bool) LineHasTwoAndThreeLettersTheSame(string line, out bool foundTwo, out bool foundThree) {
            var mapFromCharactorToAppearances = new Dictionary<char, long>();
            foundTwo = false;
            foundThree = false;
            foreach(char c in line) {
                if(!mapFromCharactorToAppearances.ContainsKey(c)) {
                    mapFromCharactorToAppearances.Add(c, 1);
                } else {
                    mapFromCharactorToAppearances[c] = mapFromCharactorToAppearances[c] + 1;
                }
            }

            if (mapFromCharactorToAppearances.ContainsValue(2))
            {
                foundTwo = true;
            }

            if (mapFromCharactorToAppearances.ContainsValue(3))
            {
                foundThree = true;
            }

            return (foundTwo, foundThree);
        }

        private class Day2Part1SolverOutput : IPuzzleOutput
        {
            private readonly long checksum;

            public object Output => checksum;

            public string ToHumanString() => $"Checksum: {checksum}";

            public Day2Part1SolverOutput(long checksum) {
                this.checksum = checksum;
            }
        }
    }

    internal class Day2Part2SolverOutput : IPuzzleOutput
    {
        private string commonCharacters;

        public Day2Part2SolverOutput(string commonCharacters)
        {
            this.commonCharacters = commonCharacters;
        }

        public object Output => commonCharacters;

        public string ToHumanString() => $"Common characters are '{commonCharacters}'";
    }
}
