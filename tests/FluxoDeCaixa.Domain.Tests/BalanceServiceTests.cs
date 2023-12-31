using FluentAssertions;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services;
using NSubstitute;

namespace FluxoDeCaixa.Domain.Tests
{
    public class BalanceServiceTests
    {
        private readonly BalanceService _balanceService;
        private readonly IBalanceRepository _balanceRepositoryMock;

        private static readonly Entry creditEntry = new() { EntryType = EntryType.Credit, Description = "Test", Date = DateTime.Now, Value = 5, };
        private static readonly Entry debtEntry = new() { EntryType = EntryType.Debt, Description = "Test", Date = DateTime.Now, Value = 5, };

        public BalanceServiceTests()
        {
            _balanceRepositoryMock = Substitute.For<IBalanceRepository>();
            _balanceService = new BalanceService(_balanceRepositoryMock);
        }

        [Fact]
        public async Task CalculateAsync_NewBalance_WithCreditEntry_Should_HasCreditValues()
        {
            //Arrange
            _balanceRepositoryMock
                .GetByDateAsync(DateOnly.FromDateTime(creditEntry.Date))
                .Returns((Balance?)null);

            // Act
            var balance = await _balanceService.CalculateAsync(creditEntry);

            // Assert
            balance.Value.Should().Be(5);
            balance.Credits.Should().Be(5);
            balance.Debts.Should().Be(0);
            balance.Date.Should().Be(DateOnly.FromDateTime(creditEntry.Date));
        }

        [Fact]
        public async Task CalculateAsync_WithCreditEntry_Should_HasCreditValues()
        {
            //Arrange            
            _balanceRepositoryMock
                .GetByDateAsync(DateOnly.FromDateTime(creditEntry.Date))
                .Returns(new Balance() { Id = Guid.NewGuid(), Credits = 20, Debts = 5, Date = DateOnly.FromDateTime(creditEntry.Date) });

            // Act
            var balance = await _balanceService.CalculateAsync(creditEntry);

            // Assert
            balance.Value.Should().Be(20);
            balance.Credits.Should().Be(25);
            balance.Debts.Should().Be(5);
            balance.Date.Should().Be(DateOnly.FromDateTime(creditEntry.Date));
        }

        [Fact]
        public async Task CalculateAsync_NewBalance_WithDebtEntry_Should_HasDebtValues()
        {
            //Arrange           
            _balanceRepositoryMock
                .GetByDateAsync(DateOnly.FromDateTime(debtEntry.Date))
                .Returns((Balance?)null);

            // Act
            var balance = await _balanceService.CalculateAsync(debtEntry);

            // Assert
            balance.Value.Should().Be(-5);
            balance.Credits.Should().Be(0);
            balance.Debts.Should().Be(5);
            balance.Date.Should().Be(DateOnly.FromDateTime(debtEntry.Date));
        }

        [Fact]
        public async Task CalculateAsync_WithDebtEntry_Should_HasDebtValues()
        {
            //Arrange            
            _balanceRepositoryMock
                .GetByDateAsync(DateOnly.FromDateTime(debtEntry.Date))
                .Returns(new Balance() { Id = Guid.NewGuid(), Credits = 20, Debts = 5, Date = DateOnly.FromDateTime(debtEntry.Date) });

            // Act
            var balance = await _balanceService.CalculateAsync(debtEntry);

            // Assert
            balance.Date.Should().Be(DateOnly.FromDateTime(debtEntry.Date));
            balance.Credits.Should().Be(20);
            balance.Debts.Should().Be(10);
            balance.Value.Should().Be(10);
        }

        [Fact]
        public async Task CalculateAsync_Sucess_Should_CallSaveAsyncRepository()
        {
            //Arrange
            var exitingBalance = new Balance() { Id = Guid.NewGuid(), Credits = 20, Debts = 5, Date = DateOnly.FromDateTime(debtEntry.Date) };
            _balanceRepositoryMock
                .GetByDateAsync(DateOnly.FromDateTime(debtEntry.Date))
                .Returns(exitingBalance);

            // Act
            var balance = await _balanceService.CalculateAsync(debtEntry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id == exitingBalance.Id && b.Id != debtEntry.Id));
        }
    }
}