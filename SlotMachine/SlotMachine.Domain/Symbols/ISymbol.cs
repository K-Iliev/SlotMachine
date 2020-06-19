namespace SlotMachine.Domain.Symbols
{
    /// <summary>
    /// Represents a symbol
    /// </summary>
    public interface ISymbol
    {
        /// <summary>
        /// Represent the multiplication coefficient in a case of win
        /// </summary>
        public decimal Coefficient { get; }

        /// <summary>
        /// Represents the probability to appear
        /// </summary>
        public decimal Probability { get;  }

        /// <summary>
        /// Represents the type of the symbol
        /// </summary>
        public SymbolType Type { get; }
    }
}
