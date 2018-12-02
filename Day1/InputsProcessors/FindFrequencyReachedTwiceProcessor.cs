using System.Collections.Generic;

namespace Day1.InputProcessors
{
    internal class FindFrequencyReachedTwiceProcessor : IInputsProcessor
    {

        long IInputsProcessor.GetFrequencyFromInputs(Input[] inputs)
        {
            long index = 0;
            long frequency = 0;
            HashSet<long> frequenciesSeen = new HashSet<long>();
            frequenciesSeen.Add(0);

            while(true) {
                var input = inputs[index];
                long multiplier = 0;

                switch(input.Type){
                    case InputType.ADD:
                        multiplier = 1;
                        break;
                    case InputType.SUBTRACT:
                        multiplier = -1;
                        break;
                }

                frequency += multiplier * input.Number;

                if (frequenciesSeen.Contains(frequency)) {
                    return frequency;
                }

                frequenciesSeen.Add(frequency);
                
                ++index;
                if (index >= inputs.Length) {
                    index = 0;
                }
            }
        }
    }
}
