namespace PuzzleCommon {
    public interface IPuzzleOutput {
        object Output { get; }

        string ToHumanString();
    }
}
