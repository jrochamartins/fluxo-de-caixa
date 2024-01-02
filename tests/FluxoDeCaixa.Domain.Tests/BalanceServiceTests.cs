using FluentAssertions;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Notifications;
using FluxoDeCaixa.Domain.Services;
using NSubstitute;

namespace FluxoDeCaixa.Domain.Tests
{
    public class BalanceServiceTests
    {
        private readonly Notifier _notifier = new();
        private readonly BalanceService _balanceService;
        private readonly IBalanceRepository _balanceRepositoryMock;

        private readonly Entry _entry;
        private readonly Balance _existingBalance;

        public BalanceServiceTests()
        {
            _entry = new Entry { Description = "Test", Date = DateTime.Now, Value = 5, };
            _existingBalance = new Balance { Credits = 50, Debts = 25, Date = DateOnly.FromDateTime(_entry.Date) };

            _balanceRepositoryMock = Substitute.For<IBalanceRepository>();
            _balanceService = new BalanceService(_notifier, _balanceRepositoryMock);
        }

        [Fact]
        public async Task CalculateAsync_WithValidCreditEntryAndNewBalance_Should_SuccessWithCallRepository()
        {
            //Arrange
            _entry.EntryType = EntryType.Credit;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns((Balance?)null);

            // Act
            await _balanceService.CalculateAsync(_entry);

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
        public async Task CalculateAsync_WithValidCreditEntryAndExistingBalance_Should_SuccessWithCallRepository()
        {
            //Arrange            
            _entry.EntryType = EntryType.Credit;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns(_existingBalance);

            // Act
            await _balanceService.CalculateAsync(_entry);

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
        public async Task CalculateAsync_WithValidDebtEntryAndNewBalance_Should_SuccessWithCallRepository()
        {
            //Arrange
            _entry.EntryType = EntryType.Debt;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns((Balance?)null);

            // Act
            await _balanceService.CalculateAsync(_entry);

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
        public async Task CalculateAsync_WithValidDebtEntryAndExistingBalance_Should_SuccessWithCallRepository()
        {
            //Arrange            
            _entry.EntryType = EntryType.Debt;
            _balanceRepositoryMock
                .GetByDateAsync(_existingBalance.Date)
                .Returns(_existingBalance);

            // Act
            await _balanceService.CalculateAsync(_entry);

            // Assert
            await _balanceRepositoryMock
                .Received(1)
                .SaveAsync(Arg.Is<Balance>(b => b.Id == _existingBalance.Id
                                                && b.Credits == 50
                                                && b.Debts == 30
                                                && b.Value == 20
                                                && b.Date == _existingBalance.Date));
        }

        [Fact]
        public async Task CalculateAsync_WithInvalidEntry_Should_HasNotificationErrors()
        {
            //Arrange            
            _entry.EntryType = default;
            _entry.Description = string.Empty;
          
            // Act
            await _balanceService.CalculateAsync(_entry);

            // Assert
            _notifier.HasNotifications().Should().BeTrue();

        }
    }
}