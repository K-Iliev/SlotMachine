using SlotMachine.Domain;
using SlotMachine.Domain.Symbols;
using System;
using System.Collections.Generic;

namespace SlotMachine.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleSlotMachine slotMachine = BuildMachine();

            Console.WriteLine("Welcome to the Simple Slot Machine");
            string continueCommand;

            Console.WriteLine("Please enter deposit");
            decimal deposit = TryParseInputToDecimal(Console.ReadLine());
            slotMachine.EnterDeposit(new Money(deposit));

            do
            {
                Console.WriteLine($"Your balance is {slotMachine.Balance.Amount}");
                Console.WriteLine("Now stake some money");
                decimal stake = TryParseInputToDecimal(Console.ReadLine());
                slotMachine.StakeMoney(new Money(stake));
                Console.WriteLine($"Your balance is {slotMachine.Balance.Amount}");

                slotMachine.Play();


                if (slotMachine.Balance.Amount == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Your deposit finished. Goodbye");
                    return;
                }

                Console.WriteLine("Do you want to continue? yes/no");
                continueCommand = Console.ReadLine();
            }
            while (continueCommand == "yes");

        }
        static SimpleSlotMachine  BuildMachine()
        {
            var random = new RandomValueGenerator();
            var symbols = new List<ISymbol>()
            {
                new Apple(),
                new Banana(),
                new Pineapple(),
                new Wildcard(),

            };
            var engine = new SimpleEngine(symbols, random);
            var machine = new SimpleSlotMachine(engine);

            return machine;
        }

        private static decimal TryParseInputToDecimal(string input)
        {
            decimal output;
            while (!Decimal.TryParse(input, out output))
            {
                Console.WriteLine("Enter valid amount");
                input = Console.ReadLine();
            }

            return output;
        }
    }
}
