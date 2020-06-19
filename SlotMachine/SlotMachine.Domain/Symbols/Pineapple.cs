namespace SlotMachine.Domain.Symbols
{
    ///<inheritdoc/>
    public class Pineapple : ISymbol
    {
        ///<inheritdoc/>
        public decimal Coefficient => 0.8m;

        ///<inheritdoc/>
        public decimal Probability => 0.15m;

        ///<inheritdoc/>
        public SymbolType Type => SymbolType.Pineapple;

        public override string ToString()
        {
            return "P";
        }
    }
}
