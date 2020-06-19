using SlotMachine.Domain.Symbols;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine.Domain
{
    /// <summary>
    /// Represents the engine that generates the symbols randomly
    /// </summary>
    public class SimpleEngine : ISimpleEngine
    {
        private const int NumberOfColumns = 3;
        private const int NumberOfRows = 4;

        private readonly IEnumerable<ISymbol> _symbols;
        private IRandomValueGenerator _randomGeneraotor;

        public SimpleEngine(IEnumerable<ISymbol> symbols, IRandomValueGenerator randomGeneraotor)
        {
            this._symbols = symbols;
            this._randomGeneraotor = randomGeneraotor;
        }

        /// <summary>
        /// Generates sequences of symbols
        /// </summary>
        /// <returns>Retuns a list of seuqnces</returns>
        public IList<IEnumerable<ISymbol>> SpinTheReel()
        {
            var gameOutput = new List<IEnumerable<ISymbol>>();

            for (int i = 0; i < NumberOfRows; i++)
            {
                var symbols = SymbolsGenerator();

                gameOutput.Add(symbols.ToList());
            }

            return gameOutput;
        }

        /// <summary>
        /// Generate random symbols 
        /// </summary>
        /// <returns>Sequnece of symbols</returns>
        private  IEnumerable<ISymbol> SymbolsGenerator()
        {
            var symbolsSequence = new List<ISymbol>();
            for (int i = 0; i < NumberOfColumns; i++)
            {
                decimal random = this._randomGeneraotor.GetRandomdecimal();

                foreach (var item in this._symbols)
                {
                    if(random < item.Probability)
                    {
                        symbolsSequence.Add(item);
                        break;
                    }
                    random -= item.Probability;
                }
            }

            return symbolsSequence;
        }
    }
}
