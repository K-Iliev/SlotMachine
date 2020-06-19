namespace SlotMachine.Domain.Symbols
{
    ///<inheritdoc/>
    public class Wildcard : ISymbol
    {
        ///<inheritdoc/>
        public decimal Coefficient => 0;

        ///<inheritdoc/>
        public decimal Probability => 0.05m;

        ///<inheritdoc/>
        public SymbolType Type => SymbolType.Wildcard;

        public override string ToString()
        {
            return "*";
        }
    }
}
