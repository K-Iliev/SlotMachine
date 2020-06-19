namespace SlotMachine.Domain.Symbols
{
    public class Apple : ISymbol
    {
        public decimal Coefficient => 0.4m;
        
        public decimal Probability => 0.45m;

        public SymbolType Type => SymbolType.Apple;

        public override string ToString()
        {
            return "A";
        }

    }
}
