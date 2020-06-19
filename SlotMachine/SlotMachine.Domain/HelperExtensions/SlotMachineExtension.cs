using SlotMachine.Domain.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine.Domain.HelperExtensions
{
    public static class SlotMachineExtension
    {
        /// <summary>
        /// Filters winning combinantion
        /// </summary>
        /// <param name="symbols">all generated symbols</param>
        /// <returns>Collection of sequneces of symbols</returns>
        public static IList<IEnumerable<ISymbol>> GetWinCombinations(this IList<IEnumerable<ISymbol>> symbols)
        {
            return symbols.Where(x => x.All(x => x.Type == SymbolType.Apple) ||
             x.All(x => x.Type == SymbolType.Banana) ||
             x.All(x => x.Type == SymbolType.Pineapple) ||
             x.Where(x => x.Type == SymbolType.Wildcard).Count() > 1 ||
            (x.Where(x => x.Type == SymbolType.Apple).Count() == 2 && x.Any(x => x.Type == SymbolType.Wildcard)) ||
            (x.Where(x => x.Type == SymbolType.Banana).Count() == 2 && x.Any(x => x.Type == SymbolType.Wildcard)) ||
            (x.Where(x => x.Type == SymbolType.Pineapple).Count() == 2 && x.Any(x => x.Type == SymbolType.Wildcard)))
            .ToList();
        }

        /// <summary>
        /// Displays generated symbols on the console
        /// </summary>
        /// <param name="symbolsCollections">Collection of sequneces of symbols</param>
        /// <returns>Collection of sequneces of symbols</returns>
        public static IList<IEnumerable<ISymbol>> DisplaySymbols(this IList<IEnumerable<ISymbol>> symbolsCollections)
        {
            foreach (var sequence in symbolsCollections)
            {
                foreach (var symbol in sequence)
                {
                    Console.Write(symbol.ToString());
                }

                Console.WriteLine();
            }

            return symbolsCollections;
        }

        /// <summary>
        /// Calulate won amount
        /// </summary>
        /// <param name="winningSymbols">Collection of sequeneces of winning symbols</param>
        /// <param name="stake"></param>
        /// <returns>won amount</returns>
        public static decimal CalculateWonAmount(this IList<IEnumerable<ISymbol>> winningSymbols, decimal stake)
        {
            if (!winningSymbols.Any())
            {
                return 0;
            }

            decimal coefficient = winningSymbols.Sum(x => x.Select(x => x.Coefficient).Sum());

            return coefficient * stake;
        }
    }
}

