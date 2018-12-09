using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PuzzleCommon;

namespace Day4
{
    internal struct Input {
        public DateTime DateTime {get; set;}
        public Type InputType {get; set;}
        public int GuardNum {get; set;}

        public enum Type {
            FALL_ASLEEP,
            WAKES_UP,
            BEGIN_SHIFT
        }
    }

    public class Day4Solver : IPuzzleSolver
    {
        private readonly string inputFile;
        public Day4Solver(string inputFile) {
            this.inputFile = inputFile;
        }

        public IPuzzleOutput Solve()
        {
            var inputLines = File.ReadAllLines(inputFile);

            /*
            [1518-03-11 00:45] wakes up
            [1518-07-13 00:13] falls asleep
            [1518-11-02 23:56] Guard #3463 begins shift
             */
            var inputs = inputLines.Select((line) => {
                var lineParts = line.Replace("[","").Replace("]", "").Split(" ").ToArray();
                
                var dateParts = lineParts[0].Split("-").Select(int.Parse).ToArray();

                var timeParts = lineParts[1].Split(":").Select(int.Parse).ToArray();
                
                int? guardId = lineParts[2].Contains("Guard") ? int.Parse(lineParts[3].TrimStart('#')) : (int?)null;
                
                var input = new Input() {
                    DateTime = new DateTime(dateParts[0], dateParts[1], dateParts[2], timeParts[0], timeParts[1], 0),
                    InputType = lineParts[2].Contains("falls") 
                                    ? Input.Type.FALL_ASLEEP
                                    : lineParts[2].Contains("wakes")
                                        ? Input.Type.WAKES_UP
                                        : Input.Type.BEGIN_SHIFT,
                };
                if (guardId.HasValue) {
                    input.GuardNum = guardId.Value;
                }

                return input;
            });

            inputs = inputs.OrderBy(x => x.DateTime);
            
            var guardToMinutesSlept = new Dictionary<int, List<int>>();

            int currentGuardId = -1;
            List<int> minutesAsleepForGuard = null;
            foreach(var input in inputs) {
                if (input.InputType == Input.Type.BEGIN_SHIFT) {
                    currentGuardId = input.GuardNum;

                    if (minutesAsleepForGuard != null) {
                        if (guardToMinutesSlept.ContainsKey(currentGuardId)) {
                            guardToMinutesSlept[currentGuardId].AddRange(minutesAsleepForGuard);
                        } else {
                            guardToMinutesSlept.Add(currentGuardId, minutesAsleepForGuard);
                        }
                        minutesAsleepForGuard.Clear();
                    }
                }

                if (input.InputType == Input.Type.FALL_ASLEEP) {
                    if (minutesAsleepForGuard == null) {
                        minutesAsleepForGuard = new List<int>();
                    }
                    minutesAsleepForGuard.Add(input.DateTime.Minute);
                }

                if (input.InputType == Input.Type.WAKES_UP) {
                    minutesAsleepForGuard.AddRange(Enumerable.Range(minutesAsleepForGuard.Last() + 1, input.DateTime.Minute-1));
                }
            }

            int guardSleepingMost = guardToMinutesSlept.OrderByDescending(x => x.Value.Count).First().Key;
            int guardsFavouriteSleepingMinute = guardToMinutesSlept[guardSleepingMost]
                                                    .Max(x => guardToMinutesSlept[guardSleepingMost].Count(y => x.Equals(y)));

            return new Day4SolverOutput(guardSleepingMost * guardsFavouriteSleepingMinute);
        }

        private class Day4SolverOutput : IPuzzleOutput
        {
            private readonly int guardMultMinute;

            public Day4SolverOutput(int guardMultMinute)
            {
                this.guardMultMinute = guardMultMinute;
            }

            public object Output => guardMultMinute;

            public string ToHumanString() => $"Guard multiplied by minute: {guardMultMinute}";
        }
    }
}
