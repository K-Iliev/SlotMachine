using System;

namespace SlotMachine.Domain
{
    /// <summary>
    /// Represents a slot machine
    /// </summary>
    public class SimpleSlotMachine
    {
        public Money Balance { get; private set; }
        public Money Stake { get; private set; }


        public SimpleSlotMachine()
        {
            this.Balance = new Money(0);
            this.Stake = new Money(0);
        }

        /// <summary>
        /// Deposits money in the slot machine
        /// </summary>
        /// <param name="deposit">The amount to deposit</param>
        public void EnterDeposit(Money deposit)
        {
            this.Balance += deposit;
        }

        /// <summary>
        /// Stake money
        /// </summary>
        /// <param name="stake">The amount to stake</param>
        public void StakeMoney(Money stake)
        {
            if(!StakeAmountIsValid(stake))
            {
                throw new InvalidOperationException("The stake cannot be larger than the balance.");
            }

            this.Stake += stake;
        }

        /// <summary>
        /// Checks whether the stake exceeds the balance
        /// </summary>
        /// <param name="stake">the amount to stake</param>
        /// <returns></returns>
        private bool StakeAmountIsValid(Money stake)
        => !(stake.Amount > this.Balance.Amount ||
               stake.Amount + this.Stake.Amount > Balance.Amount);
    }
}
