using System;

namespace SlotMachine.Domain
{
    /// <summary>
    /// Represents money
    /// </summary>
    public class Money
    {
        /// <summary>
        /// Represent the amount of money
        /// </summary>
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("The amount should be positive.");
            }

            this.Amount = amount;
        }

        public static Money operator +(Money money1, Money money2)
        {
           return new Money(money1.Amount + money2.Amount);
        }

        public static Money operator -(Money money1, Money money2)
        {
            return new Money(money1.Amount - money2.Amount);
        }

    }
}
