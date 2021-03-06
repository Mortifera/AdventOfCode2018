﻿using System;
using System.Diagnostics;
using System.IO;
using Day1;
using Day2;
using Day3;
using Day4;
using PuzzleCommon;
using TwoPartPuzzleOutput = PuzzleCommon.TwoPartPuzzleOutput;

namespace PuzzleRunner
{
    class TimedPuzzleSolver : IPuzzleSolver {
        private class TimedPuzzleSolverOutput : IPuzzleOutput
        {
            private readonly IPuzzleOutput _output;
            private readonly long _timeMs;
            public TimedPuzzleSolverOutput(IPuzzleOutput output, long timeMs) {
                _output = output;
                _timeMs = timeMs;
            }
            public object Output => _output.Output;
            public String ToHumanString() => _output.ToHumanString() + '\n' 
                                                        + $"Ran in {_timeMs} ms";
                                                        
        }

        private readonly IPuzzleSolver _puzzleSolver;
        private readonly int runXTimes;

        public TimedPuzzleSolver(IPuzzleSolver puzzleSolver, int runXTimes = 1) {
            _puzzleSolver = puzzleSolver;
            this.runXTimes = runXTimes;
        }

        public IPuzzleOutput Solve()
        {
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            IPuzzleOutput solverOutput = null;
            for (int i = 0; i < runXTimes; ++i) {
                solverOutput = _puzzleSolver.Solve();
            }
            stopwatch.Stop();

            return new TimedPuzzleSolverOutput(
                output: solverOutput,
                timeMs: stopwatch.ElapsedMilliseconds
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IPuzzleSolver solver = new Day4Solver("../PuzzleInputs/Day4/input.txt");
            solver = new TimedPuzzleSolver(solver);
            var solverOutput = solver.Solve();
            System.Console.WriteLine(solverOutput.ToHumanString());

            if (!testDay1()) {
                throw new FailedTestException("Failed Day1 tests");
            }

            if (!testDay2()) {
                throw new FailedTestException("Day2");
            }

            if (!testDay3()) { //
                throw new FailedTestException("Day3");
            }
        }

        class FailedTestException : Exception
        {
            public FailedTestException(string message) : base(message)
            {
            }
        }

        static bool testDay1() {
            Day1Solver day1Solver = new Day1Solver("../PuzzleInputs/Day1/input.txt");
            TwoPartPuzzleOutput twoPartPuzzleOutput = day1Solver.Solve() as TwoPartPuzzleOutput;
            
            if((long)twoPartPuzzleOutput.Part1.Output != 472L) {
                System.Console.WriteLine($"Day1 Part1 solver solution is {twoPartPuzzleOutput.Part1.Output} when it should have been 472");
                return false;
            }

            if((long)twoPartPuzzleOutput.Part2.Output != 66932) {
                System.Console.WriteLine($"Day1 Part2 solver solution is {twoPartPuzzleOutput.Part2.Output} when it should have been 66932");
                return false;
            }
            
            return true;
        }

        static bool testDay2() {
            Day2Solver day2Solver = new Day2Solver("../PuzzleInputs/Day2/input.txt");
            var output = day2Solver.Solve() as TwoPartPuzzleOutput;

            if((long)output.Part1.Output != 5928) {
                System.Console.WriteLine($"Day2 Part1 solver solution is {output.Part1.Output} when it should have been 5928");
                return false;
            }

            if((string)output.Part2.Output != "bqlporuexkwzyabnmgjqctvfs") {
                System.Console.WriteLine($"Day2 Part2 solver solution is {output.Part2.Output} when it should have been 'bqlporuexkwzyabnmgjqctvfs");
                return false;
            }

            return true;
        }

        static bool testDay3() {
            Day3Solver day3Solver = new Day3Solver("../PuzzleInputs/Day3/input.txt");
            TwoPartPuzzleOutput twoPartPuzzleOutput = day3Solver.Solve() as TwoPartPuzzleOutput;
            
            if((int)twoPartPuzzleOutput.Part1.Output != 114946) {
                System.Console.WriteLine($"Day1 Part1 solver solution is {twoPartPuzzleOutput.Part1.Output} when it should have been 114946");
                return false;
            }

            if((int)twoPartPuzzleOutput.Part2.Output != 877) {
                System.Console.WriteLine($"Day1 Part2 solver solution is {twoPartPuzzleOutput.Part2.Output} when it should have been 877");
                return false;
            }
            
            return true;
        }
    }
}
