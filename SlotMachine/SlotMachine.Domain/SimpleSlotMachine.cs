using SlotMachine.Domain.HelperExtensions;
using System;

namespace SlotMachine.Domain
{
    /// <summary>
    /// Represents a slot machine
    /// </summary>
    public class SimpleSlotMachine
    {
        private const int NumberOfRows = 4;

        private readonly ISimpleEngine _engine;
        public Money Balance { get; private set; }
        public Money Stake { get; private set; }


        public SimpleSlotMachine(ISimpleEngine engine)
        {
            if(engine == null)
            {
                throw new ArgumentNullException("engine cannot be null");
            }

            this._engine = engine;
            this.Balance = new Money(0);
            this.Stake = new Money(0);
        }

        /// <summary>
        /// Deposits money in the slot machine
        /// </summary>
        /// <param name="deposit">The amount to deposit</param>
        public void EnterDeposit(Money deposit)
        {
            if(deposit.Amount == 0)
            {
                throw new InvalidOperationException("Inavlid deposit amount");
            }

            this.Balance += deposit;
        }

        /// <summary>
        /// Play with the slot machine
        /// </summary>
        public void Play()
        {
            if (this.Stake.Amount == 0)
            {
                Console.WriteLine("No Stake No Play");
                return;
            }

          decimal wonAmount =
                 _engine.SpinTheReel()
                .DisplaySymbols()
                .GetWinCombinations()
                .CalculateWonAmount(this.Stake.Amount);

            this.Balance += new Money(wonAmount);
            this.Stake = new Money(0);

            DisplayResults(wonAmount);
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
            this.Balance -= this.Stake;
        }

        /// <summary>
        /// Checks whether the stake exceeds the balance
        /// </summary>
        /// <param name="stake">the amount to stake</param>
        /// <returns></returns>
        private bool StakeAmountIsValid(Money stake)
        => !(stake.Amount > this.Balance.Amount ||
               stake.Amount + this.Stake.Amount > Balance.Amount);

        /// <summary>
        /// Display the results of a spin on the console
        /// </summary>
        /// <param name="wonAmount"> the won amount</param>
        private void DisplayResults(decimal wonAmount)
        {
            Console.WriteLine($"You have won: {wonAmount.ToString("F")}");
            Console.WriteLine($"Current balance is: {this.Balance.Amount}");
        }
    }
}
