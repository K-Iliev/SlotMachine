using SlotMachine.Domain.Symbols;
using System.Collections.Generic;

namespace SlotMachine.Domain
{
    /// <summary>
    /// Represents the engine of the slot machine
    /// </summary>
    public interface ISimpleEngine
    {
        /// <summary>
        /// Generate four sequence of symbolls
        /// </summary>
        /// <returns>Retuns collection of sequences of symbols</returns>
        IList<IEnumerable<ISymbol>> SpinTheReel();
    }
}