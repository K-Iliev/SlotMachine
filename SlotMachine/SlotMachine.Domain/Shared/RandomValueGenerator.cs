using System;

namespace SlotMachine.Domain
{
    ///<inheritdoc/>
    public class RandomValueGenerator : IRandomValueGenerator
    {
        private Random random;
        public RandomValueGenerator()
        {
            this.random = new Random();
        }

        ///<inheritdoc/>
        public decimal GetRandomdecimal()
        {
            return Convert.ToDecimal(this.random.NextDouble());
        }
        
    }
}
