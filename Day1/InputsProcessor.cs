using System.Threading.Tasks;
using Day1.InputProcessors;

namespace Day1
{

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
