using System;
using PuzzleCommon;

namespace Day1
{
    public class Day1Solver : IPuzzleSolver
    {
        private string _inputPath;
        public Day1Solver(string inputPath) {
            _inputPath = inputPath;
        }

        public IPuzzleOutput Solve()
        {
            var inputReader = new InputReader();
            var inputProcessor = new InputProcessor();

            var inputs = inputReader.GetInputFromFile(_inputPath);
            var outputFrequency = inputProcessor.GetFrequencyOutput(inputs);

            return new Day1SolverOutput(outputFrequency);
        }

        private class Day1SolverOutput : IPuzzleOutput
        {
            private readonly long _frequency;

            public Day1SolverOutput(long frequency) {
                _frequency = frequency;
            }

            public object Output => _frequency;

            public string ToHumanString()
            {
                return $"Frequency: {_frequency}";
            }
        }
    }
}
