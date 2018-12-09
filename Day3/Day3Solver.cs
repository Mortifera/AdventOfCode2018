using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PuzzleCommon;

namespace Day3
{

    public class Two2D<V> {
        private readonly int width;
        private V[] data;

        public Two2D(int width, int height) {
            data = new V[width * height];
            this.width = width;
        }

        public V GetValue(int x, int y) {
            return data[y*width + x];
        }

        public void SetValue(int x, int y, V value) {
            data[y*width + x] = value;
        }
    }
    
    internal class SquareVal {
        private List<int> claims;
        public int NumClaims => claims.Count();
        public int[] Claims => claims.ToArray();

        public SquareVal(int initialClaimId) {
            claims = new List<int>();
            claims.Add(initialClaimId);
        }

        public void AddClaim(int claimId) {
            claims.Add(claimId);
        }
    }

    public class Day3Solver : IPuzzleSolver
    {
        private readonly string inputFile;
        public Day3Solver(string inputFile) {
            this.inputFile = inputFile;
        }

        public IPuzzleOutput Solve()
        {
            var inputLines = File.ReadAllLines(inputFile);
            var locToInt = new Dictionary<(int, int), SquareVal>();
            var claimIdToSize = new Dictionary<int, int>();

            foreach(var line in inputLines) {
                var lineSplit = line.Split(" ");

                var marginStr = lineSplit[2].TrimEnd(':').Split(",");
                var sizingStr = lineSplit[3].Split("x");

                var margins = marginStr.Select(int.Parse).ToArray();
                var sizing = sizingStr.Select(int.Parse).ToArray();

                var claimId = int.Parse(lineSplit[0].TrimStart('#'));

                claimIdToSize.Add(claimId, sizing[0]*sizing[1]);

                for(int y = margins[1]; y < margins[1] + sizing[1]; ++y) {
                    for(int x = margins[0]; x < margins[0] + sizing[0]; ++x) {
                        (int, int) loc = (x, y);
                        if(locToInt.ContainsKey(loc)) {
                            locToInt[loc].AddClaim(claimId);
                        } else {
                            locToInt.Add(loc, new SquareVal(claimId));
                        }
                    }
                }
            }

            var overlaps = locToInt.Count((a) => a.Value.NumClaims > 1);
            
            var nonOverlappingSquares = locToInt.Where(x => x.Value.NumClaims == 1);
            var remainingClaims = nonOverlappingSquares.SelectMany(x => x.Value.Claims).Distinct();
            var claimToActualSize = new Dictionary<int, int>();

            foreach(var square in nonOverlappingSquares) {
                var claimId = square.Value.Claims[0];
                if(!claimToActualSize.ContainsKey(claimId)) {
                    claimToActualSize.Add(claimId, 1);
                } else {
                    claimToActualSize[claimId] = claimToActualSize[claimId] + 1;
                }
            }

            var claimIdWithoutOverlap = claimIdToSize.Intersect(claimToActualSize).First().Key;

            return new TwoPartPuzzleOutput(new Day3Part1SolverOutput(overlaps), new Day3Part2SolverOutput(claimIdWithoutOverlap));
        }

        private class Day3Part2SolverOutput : IPuzzleOutput {
            private readonly int claimIdWIthoutOverlap;

            public Day3Part2SolverOutput(int claimIdWithoutOverlap) {
                this.claimIdWIthoutOverlap = claimIdWithoutOverlap;
            }

            public object Output => claimIdWIthoutOverlap;

            public string ToHumanString() => $"ClaimId without overlap {claimIdWIthoutOverlap}";
        }

        private class Day3Part1SolverOutput : IPuzzleOutput
        {
            private readonly int numOverlaps;

            public Day3Part1SolverOutput(int numOverlaps) {
                this.numOverlaps = numOverlaps;
            }

            public object Output => numOverlaps;

            public string ToHumanString() => $"Number of overlaps {numOverlaps}";
        }
    }
}
