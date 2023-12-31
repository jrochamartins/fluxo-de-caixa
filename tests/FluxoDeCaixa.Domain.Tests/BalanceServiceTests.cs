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

        private static readonly Entry _newEntry = new() { Description = "Test", Date = DateTime.Now, Value = 5, };
        private readonly Balance _existingBalance = new() { Credits = 50, Debts = 25, Date = DateOnly.FromDateTime(_newEntry.Date) };

        public BalanceServiceTests()
        {
            _balanceRepositoryMock = Substitute.For<IBalanceRepository>();
            _balanceService = new BalanceService(_balanceRepositoryMock);
        }

        [Fact]
        public async Task CalculateAsync_NewBalance_WithCreditEntry_Should_HasCreditValues()
        {
            //Arrange
            _newEntry.EntryType = EntryType.Credit;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns((Balance?)null);

            // Act
            await _balanceService.CalculateAsync(_newEntry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id != _existingBalance.Id
                                                && b.Credits == 5
                                                && b.Debts == 0
                                                && b.Value == 5
                                                && b.Date == _existingBalance.Date));
        }

        [Fact]
        public async Task CalculateAsync_WithCreditEntry_Should_HasCreditValues()
        {
            //Arrange            
            _newEntry.EntryType = EntryType.Credit;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns(_existingBalance);

            // Act
            await _balanceService.CalculateAsync(_newEntry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id == _existingBalance.Id
                                                && b.Credits == 55
                                                && b.Debts == 25
                                                && b.Value == 30
                                                && b.Date == _existingBalance.Date));
        }

        [Fact]
        public async Task CalculateAsync_NewBalance_WithDebtEntry_Should_HasDebtValues()
        {
            //Arrange
            _newEntry.EntryType = EntryType.Debt;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns((Balance?)null);

            // Act
            await _balanceService.CalculateAsync(_newEntry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id != _existingBalance.Id
                                                && b.Credits == 0
                                                && b.Debts == 5
                                                && b.Value == -5
                                                && b.Date == _existingBalance.Date));
        }

        [Fact]
        public async Task CalculateAsync_WithDebtEntry_Should_HasDebtValues()
        {
            //Arrange            
            _newEntry.EntryType = EntryType.Debt;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns(_existingBalance);

            // Act
            await _balanceService.CalculateAsync(_newEntry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id == _existingBalance.Id
                                                && b.Credits == 50
                                                && b.Debts == 30
                                                && b.Value == 20
                                                && b.Date == _existingBalance.Date));
        }
    }
}