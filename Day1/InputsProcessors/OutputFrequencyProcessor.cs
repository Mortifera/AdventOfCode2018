namespace Day1.InputProcessors
{
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
}
