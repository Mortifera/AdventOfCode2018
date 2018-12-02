using System;

namespace PuzzleCommon
{
    public class TwoPartPuzzleOutput : IPuzzleOutput
    {
        private readonly IPuzzleOutput part1;
        private readonly IPuzzleOutput part2;

        public TwoPartPuzzleOutput(IPuzzleOutput part1, IPuzzleOutput part2)
        {
            this.part1 = part1;
            this.part2 = part2;
        }

        public object Output => new Tuple<IPuzzleOutput, IPuzzleOutput>(part1, part2);

        public IPuzzleOutput Part1 => part1;

        public IPuzzleOutput Part2 => part2;

        public string ToHumanString()
        {
            return part1.ToHumanString() + '\n'
                    + part2.ToHumanString();
        }
    }

}
