using FluentAssertions;
using Moq;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.NotificationSchedule;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.ApplicationTests.Services.Internal.Schedule.Commands.Create;

public class ScheduleCreateHandlerTests
{
    private Mock<IScheduleRepository> _scheduleRepositoryMock;
    private Mock<INotificationSchedulePublisher> _notificationMock;
    private ScheduleCreateHandler _handler;

    [SetUp]
    public void Setup()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _notificationMock = new Mock<INotificationSchedulePublisher>();
        _handler = new ScheduleCreateHandler(_scheduleRepositoryMock.Object, _notificationMock.Object);
    }

    [Test]
    public async Task Handle_ShouldCreateScheduleAndReturnActionResult()
    {
        // Arrange
        var command = new ScheduleCreateCommand
        {
            Id = "123",
            Name = "Test Name",
            Email = "test@example.com",
            Phone = 123456789,
            PhoneDDD = 12,
            Observation = "Test Observation",
            Status = ScheduleStatusEnum.AwaitingForSchedule,
            MotorcycleId = "TestMotorcycleId",
            StartBusinessHour = DateTime.Now,
            EndBusinessHour = DateTime.Now.AddHours(1),
            ScheduleDate = DateTime.Now.AddDays(1)
        };

        var entity = new ScheduleEntity
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            Phone = command.Phone,
            PhoneDDD = command.PhoneDDD,
            Observation = command.Observation,
            Status = command.Status,
            MotorcycleId = command.MotorcycleId,
            StartBusinessHour = command.StartBusinessHour,
            EndBusinessHour = command.EndBusinessHour,
            ScheduleDate = command.ScheduleDate
        };

        var expectedSchedule = new
        {
            data = entity
        };

        _scheduleRepositoryMock.Setup(repo => repo.Create(It.IsAny<ScheduleEntity>()))
                .ReturnsAsync((ScheduleEntity schedule) => schedule);

        _notificationMock.Setup(n => n.PublishMotorcycle(It.IsAny<NotificationScheduleEntity>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasData().Should().BeTrue();

        expectedSchedule.Should().BeEquivalentTo(expectedSchedule);
        _notificationMock.Verify(n => n.PublishMotorcycle(It.IsAny<NotificationScheduleEntity>()), Times.Once);
    }
}
