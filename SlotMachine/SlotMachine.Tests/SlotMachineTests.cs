using FluentAssertions;
using Moq;
using SlotMachine.Domain;
using SlotMachine.Domain.Symbols;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlotMachine.Tests
{
    public class SlotMachineTests
    {
        [Fact]
        public void One_deposit_increses_the_balance()
        {
            //Arrange
            var sut = MachineFacotry();
            var moneyToDeposit = new Money(200);

            //Act
            sut.EnterDeposit(moneyToDeposit);

            //Assert
            sut.Balance.Amount.Should().Be(moneyToDeposit.Amount);
        }


        [Fact]
        public void Multiple_deposits_build_up_the_balance()
        {
            //Arrange
            var sut = MachineFacotry();
            var depositOne = new Money(200);
            var depositTwo = new Money(100);

            //Act
            sut.EnterDeposit(depositOne);
            sut.EnterDeposit(depositTwo);

            //Assert
            sut.Balance.Amount.Should().Be(depositOne.Amount + depositTwo.Amount);
        }

        [Fact]
        public void Stack_more_than_balance_is_invalid_operation()
        {
            //Arrange
            var sut = MachineFacotry();
            var moneyToDeposit = new Money(200);
            var stake = new Money(300);

            //Act
            sut.EnterDeposit(moneyToDeposit);

            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.StakeMoney(stake));
        }

        [Fact]
        public void Stack_multiple_when_exceeding_balance_is_invalid_operation()
        {
            //Arrange
            var sut = MachineFacotry();
            var moneyToDeposit = new Money(200);
            var stakeOne = new Money(100);
            var stakeTwo = new Money(101);

            //Act
            sut.EnterDeposit(moneyToDeposit);
            sut.StakeMoney(stakeOne);

            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.StakeMoney(stakeTwo));
        }

        [Fact]
        public void User_lost_all_money_when_stake_everything_and_no_winning_combinantion_appear()
        {
            //Arrange
            decimal deposit = 200;
            decimal stake = 200;
            var engine = new Mock<ISimpleEngine>();
            engine.Setup(x => x.SpinTheReel()).Returns(NoWinCombination);
            var slotMachine = new SimpleSlotMachine(engine.Object);

            //Act
            slotMachine.EnterDeposit(new Money(deposit));
            slotMachine.StakeMoney(new Money(stake));
            slotMachine.Play();

            //Assert
            slotMachine.Balance.Amount.Should().Be(deposit - stake);
        }

        [Fact]
        public void User_win_money_when_winning_combinantion_appear()
        {
            //Arrange
            decimal deposit = 200;
            decimal stake = 10;
            var engine = new Mock<ISimpleEngine>();
            engine.Setup(x => x.SpinTheReel()).Returns(WinCombinationApples);
            var slotMachine = new SimpleSlotMachine(engine.Object);

            //Act
            slotMachine.EnterDeposit(new Money(deposit));
            slotMachine.StakeMoney(new Money(stake));
            slotMachine.Play();

            //Assert
            slotMachine.Balance.Amount.Should().BeGreaterThan(deposit - stake);
        }

        [Fact]
        public void User_win_less_than_stake_when_won_with_one_wildcard_only()
        {
            //Arrange
            decimal deposit = 200;
            decimal stake = 10;
            var engine = new Mock<ISimpleEngine>();
            engine.Setup(x => x.SpinTheReel()).Returns(WinCombinationWildCards);
            var slotMachine = new SimpleSlotMachine(engine.Object);

            //Act
            slotMachine.EnterDeposit(new Money(deposit));
            slotMachine.StakeMoney(new Money(stake));
            slotMachine.Play();

            //Assert
            slotMachine.Balance.Amount.Should().BeLessThan(deposit).And.BeGreaterThan(deposit-stake);
        }


        private  IList<IEnumerable<ISymbol>> NoWinCombination()=>
        
             new List<IEnumerable<ISymbol>>()
            {
                new List<ISymbol>(){new Apple(),new Banana(), new Pineapple()},
                new List<ISymbol>(){new Apple(),new Banana(), new Banana()},
                new List<ISymbol>(){new Apple(),new Banana(), new Wildcard()}
            };
        

        private IList<IEnumerable<ISymbol>> WinCombinationWildCards()
        {
            return new List<IEnumerable<ISymbol>>()
            {
                new List<ISymbol>(){new Apple(),new Wildcard(), new Wildcard()},
                new List<ISymbol>(){new Wildcard(),new Banana(), new Apple()},
                new List<ISymbol>(){new Wildcard(),new Pineapple(), new Banana()}
            };
        }

        private IList<IEnumerable<ISymbol>> WinCombinationApples()
        {
            return new List<IEnumerable<ISymbol>>()
            {
                new List<ISymbol>(){new Apple(),new Apple(), new Apple()},
                new List<ISymbol>(){new Wildcard(),new Banana(), new Pineapple()},
                new List<ISymbol>(){new Pineapple(),new Pineapple(), new Banana()}
            };
        }


        private SimpleSlotMachine MachineFacotry()
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
    }
}