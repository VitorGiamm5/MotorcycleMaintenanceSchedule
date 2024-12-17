using FluentAssertions;
using Moq;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Delete;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.ApplicationTests.Services.Internal.Schedule.Commands.Delete;

public class ScheduleDeleteHandlerTests
{
    private Mock<IScheduleRepository> _scheduleRepositoryMock;
    private ScheduleDeleteHandler _handler;

    [SetUp]
    public void Setup()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _handler = new ScheduleDeleteHandler(_scheduleRepositoryMock.Object);
    }


    [Test]
    public async Task Handle_ShouldDeleteSchedule_WhenScheduleExists()
    {
        // Arrange
        var scheduleId = "existing-id";
        var scheduleEntity = new ScheduleEntity
        {
            Id = scheduleId,
            Name = "oi"
        };
        _scheduleRepositoryMock.Setup(repo => repo.GetById(scheduleId)).ReturnsAsync(scheduleEntity);
        _scheduleRepositoryMock.Setup(repo => repo.Delete(scheduleId)).ReturnsAsync(scheduleEntity);

        var command = new ScheduleDeleteCommand { Id = scheduleId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.HasData().Should().BeFalse();
        _scheduleRepositoryMock.Verify(repo => repo.GetById(scheduleId), Times.Once);
        _scheduleRepositoryMock.Verify(repo => repo.Delete(scheduleId), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnError_WhenScheduleDoesNotExist()
    {
        // Arrange
        var scheduleId = "non-existing-id";
        _scheduleRepositoryMock.Setup(repo => repo.GetById(scheduleId)).ReturnsAsync((ScheduleEntity)null);

        var command = new ScheduleDeleteCommand { Id = scheduleId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.HasData().Should().BeFalse();
        _scheduleRepositoryMock.Verify(repo => repo.GetById(scheduleId), Times.Once);
        _scheduleRepositoryMock.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Never);
    }
}
