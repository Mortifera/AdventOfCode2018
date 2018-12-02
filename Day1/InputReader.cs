using System.IO;
using System.Linq;

namespace Day1
{
    internal class InputReader {
        public Input[] GetInputFromFile(string filePath) {
            string[] inputFileLines = File.ReadAllLines(filePath);

            return inputFileLines.Select(this.GetInputFromLine).ToArray();
        }

        private Input GetInputFromLine(string line) {
            var inputType = line[0];
            var num = line.Substring(1);

            return new Input {
                Type = (inputType.Equals('+') ? InputType.ADD : InputType.SUBTRACT),
                Number = long.Parse(num)
            };
        }
    }
}
