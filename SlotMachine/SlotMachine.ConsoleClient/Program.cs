using SlotMachine.Domain;
using System;

namespace SlotMachine.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var sm = new SimpleSlotMachine();
            sm.EnterDeposit(new Money(50));
            Console.WriteLine(sm.Balance.Amount);
        }
    }
}
