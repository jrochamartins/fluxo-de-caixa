using FluentAssertions;
using FluxoDeCaixa.Domain.Abstractions.Adapters;
using FluxoDeCaixa.Domain.Abstractions.Repositories;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Notifications;
using FluxoDeCaixa.Domain.Services;
using NSubstitute;

namespace FluxoDeCaixa.Domain.Tests
{
    public class EntriesServiceTests
    {
        private readonly Notifier _notifier = new();
        private readonly EntriesService _entriesService;
        private readonly IEntriesRepository _entriesRepositoryMock;
        private readonly IQueuePublisher _queuePublisherMock;

        public EntriesServiceTests()
        {
            _entriesRepositoryMock = Substitute.For<IEntriesRepository>();
            _queuePublisherMock = Substitute.For<IQueuePublisher>();
            _entriesService = new EntriesService(_notifier, _entriesRepositoryMock, _queuePublisherMock);
        }

        [Fact]
        public async Task CreateAsync_WithError_Should_NotContainValidDecription()
        {
            // Arrange
            Entry invalidEntry = new() { Description = "A", Date = DateTime.Now, Value = 5, EntryType = EntryType.Credit };

            // Act
            await _entriesService.CreateAsync(invalidEntry);

            //Assert
            await CheckOperationWithInvalidEntry(invalidEntry, 1);
            _notifier.GetNotifications().FirstOrDefault()?.Message
                .Should().NotBeEmpty();
        }        

        [Fact]
        public async Task CreateAsync_WithError_Should_NotContainValidValue()
        {
            // Arrange
            Entry invalidEntry = new() { Description = "Test", Date = DateTime.Now, Value = -1, EntryType = EntryType.Credit };

            // Act
            await _entriesService.CreateAsync(invalidEntry);

            //Assert
            await CheckOperationWithInvalidEntry(invalidEntry, 1);
        }

        [Fact]
        public async Task CreateAsync_WithError_Should_NotContainValidType()
        {
            // Arrange
            Entry invalidEntry = new() { Description = "Test", Date = DateTime.Now, Value = 5, EntryType = default };

            // Act
            await _entriesService.CreateAsync(invalidEntry);

            //Assert
            await CheckOperationWithInvalidEntry(invalidEntry, 1);
        }

        [Fact]
        public async Task CreateAsync_WithError_Should_ContainNotifications()
        {
            // Arrange
            Entry invalidEntry = new() { Description = string.Empty, Date = DateTime.Now, Value = -1, EntryType = default };

            // Act
            await _entriesService.CreateAsync(invalidEntry);

            //Assert
            await CheckOperationWithInvalidEntry(invalidEntry, 4);
        }

        private async Task CheckOperationWithInvalidEntry(Entry invalidEntry, int expectedNotifications)
        {
            //Assert
            _notifier.HasNotifications().Should().BeTrue();
            _notifier.GetNotifications().Should().HaveCount(expectedNotifications);

            await _entriesRepositoryMock
                .DidNotReceive()
                .CreateAsync(invalidEntry);

            _queuePublisherMock
                .DidNotReceiveWithAnyArgs()
                .Publish(invalidEntry);
        }

        [Fact]
        public async Task CreateAsync_WithSucess_Should_CallRepositoryAndPublisher()
        {
            // Arrange
            Entry newValidEntry = new() { Description = "Test", Date = DateTime.Now, Value = 5, EntryType = EntryType.Credit };

            // Act
            await _entriesService.CreateAsync(newValidEntry);

            //Assert
            _notifier.HasNotifications().Should().BeFalse();

            await _entriesRepositoryMock
                .Received(1)
                .CreateAsync(Arg.Is<Entry>(e => e.Id == newValidEntry.Id
                                                && e.Description == newValidEntry.Description
                                                && e.Date == newValidEntry.Date
                                                && e.Value == newValidEntry.Value
                                                && e.EntryType == newValidEntry.EntryType));

            _queuePublisherMock
                .Received(1)
                .Publish(Arg.Is<object>(e =>
                                            e.GetType() == typeof(Entry)
                                            && ((Entry)e).Id == newValidEntry.Id
                                            && ((Entry)e).Description == newValidEntry.Description
                                            && ((Entry)e).Date == newValidEntry.Date
                                            && ((Entry)e).Value == newValidEntry.Value
                                            && ((Entry)e).EntryType == newValidEntry.EntryType));
        }
    }
}
