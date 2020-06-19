namespace SlotMachine.Domain.Symbols
{
    ///<inheritdoc/>
    public class Banana : ISymbol
    {
        ///<inheritdoc/>
        public decimal Coefficient  => 0.6m;

        ///<inheritdoc/>
        public decimal Probability  => 0.35m;

        ///<inheritdoc/>
        public SymbolType Type => SymbolType.Banana;

        public override string ToString()
        {
            return "B";
        }

    }
}
