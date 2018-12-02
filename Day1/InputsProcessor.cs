using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day1
{
    internal interface IInputsProcessor {
        long GetFrequencyFromInputs(Input[] inputs);
    }

    internal class OutputFrequencyProcessor : IInputsProcessor
    {
        public long GetFrequencyFromInputs(Input[] inputs)
        {
            long frequency = 0;

            foreach(var input in inputs) {
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
            }

            return frequency;
        }
    }

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

    internal class InputsProcessorFacade {
        private long _firstFrequencyReachedTwice = default(long);
        private long _outputFrequency = default(long);

        public long FirstFrequencyReachedTwice => _firstFrequencyReachedTwice;
        public long OutputFrequency => _outputFrequency;

        private IInputsProcessor outputFrequencyProcessor;
        private IInputsProcessor firstFrequencyReachedTwiceProcessor;

        public InputsProcessorFacade() {
            outputFrequencyProcessor = new OutputFrequencyProcessor();
            firstFrequencyReachedTwiceProcessor = new FindFrequencyReachedTwiceProcessor();
        }
        public void Process(Input[] inputs) {

            _firstFrequencyReachedTwice = firstFrequencyReachedTwiceProcessor.GetFrequencyFromInputs(inputs);
            _outputFrequency = outputFrequencyProcessor.GetFrequencyFromInputs(inputs);
        }
        
    }
}
