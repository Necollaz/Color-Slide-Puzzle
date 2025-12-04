public struct TileTransferStep
{
    public TileStackView Source;
    public TileStackView Target;
    public int ChainStepIndex;

    public TileTransferStep(TileStackView source, TileStackView target, int chainStepIndex)
    {
        Source = source;
        Target = target;
        ChainStepIndex = chainStepIndex;
    }
}