using Xunit;
using FluentAssertions;
using SlotMachine.Domain;
using System;

namespace SlotMachine.Tests
{
    public class SnackMachineTests
    {
      [Fact]
      public void One_deposit_increses_the_balance()
        {
            //Arrange
            var sut = new SimpleSlotMachine();
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
            var sut = new SimpleSlotMachine();
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
            var sut = new SimpleSlotMachine();
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
            var sut = new SimpleSlotMachine();
            var moneyToDeposit = new Money(200);
            var stakeOne = new Money(100);
            var stakeTwo = new Money(101);

            //Act
            sut.EnterDeposit(moneyToDeposit);
            sut.StakeMoney(stakeOne);

            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.StakeMoney(stakeTwo));
        }
    }
}