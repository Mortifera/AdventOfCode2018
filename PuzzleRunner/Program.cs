using System;
using System.Diagnostics;
using Day1;
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

        public TimedPuzzleSolver(IPuzzleSolver puzzleSolver) {
            _puzzleSolver = puzzleSolver;
        }

        public IPuzzleOutput Solve()
        {
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            var solverOutput = _puzzleSolver.Solve();
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

            IPuzzleSolver day1Solver = new Day1Solver("../PuzzleInputs/Day1/input.txt");
            day1Solver = new TimedPuzzleSolver(day1Solver);

            var solverOutput = day1Solver.Solve();
            System.Console.WriteLine(solverOutput.ToHumanString());

            if (!testDay1()) {
                throw new FailedTestException("Failed Day1 tests");
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
    }
}
