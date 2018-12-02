using PuzzleCommon;

namespace Day1
{
    public partial class Day1Solver : IPuzzleSolver
    {
        private string _inputPath;
        public Day1Solver(string inputPath) {
            _inputPath = inputPath;
        }

        public IPuzzleOutput Solve()
        {
            var inputReader = new InputReader();
            var inputProcessor = new InputsProcessorFacade();

            var inputs = inputReader.GetInputFromFile(_inputPath);
            
            inputProcessor.Process(inputs);

            return new TwoPartPuzzleOutput(
                new Day1Part1Output(inputProcessor.OutputFrequency),
                new Day1Part2Output(inputProcessor.FirstFrequencyReachedTwice)
            );
        }

        private class Day1Part1Output : IPuzzleOutput {
            private readonly long _frequency;

            public Day1Part1Output(long frequency) {
                _frequency = frequency;
            }

            public object Output => _frequency;

            public string ToHumanString()
            {
                return $"Frequency: {_frequency}";
            }
        }
        private class Day1Part2Output : IPuzzleOutput
        {
            private readonly long _reachedFrequencyTwice;

            public Day1Part2Output(long reachedFrequencyTwice) {
                _reachedFrequencyTwice = reachedFrequencyTwice;
            }

            public object Output => _reachedFrequencyTwice;

            public string ToHumanString()
            {
                return $"First frequency reach twice: {_reachedFrequencyTwice}";
            }
        }
    }
}
