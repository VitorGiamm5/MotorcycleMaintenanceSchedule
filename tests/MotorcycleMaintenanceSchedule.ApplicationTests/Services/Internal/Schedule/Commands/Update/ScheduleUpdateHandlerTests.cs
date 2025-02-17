using FluentAssertions;
using Moq;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Update;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Services.Schedule.Queries.List;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.ApplicationTests.Services.Internal.Schedule.Commands.Update;

public class ScheduleUpdateHandlerTests
{
    private Mock<IScheduleRepository> _scheduleRepositoryMock;
    private ScheduleUpdateHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _handler = new ScheduleUpdateHandler(_scheduleRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldUpdateSchedule_WhenValidRequest()
    {
        // Arrange
        var command = new ScheduleUpdateCommand
        {
            Id = "123",
            Name = "Updated Name",
            Email = "updated@example.com",
            Phone = 123456789,
            PhoneDDD = 11,
            Observation = "Updated Observation",
            Status = ScheduleStatusEnum.AwaitingForSchedule,
            MotorcycleId = "Motorcycle123"
        };

        var existingEntity = new ScheduleEntity
        {
            Id = command.Id,
            Name = "Original Name",
            Email = "original@example.com",
            Phone = 987654321,
            PhoneDDD = 22,
            Observation = "Original Observation",
            Status = ScheduleStatusEnum.Scheduled,
            MotorcycleId = "Motorcycle456"
        };

        var updatedEntity = new ScheduleEntity
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

        _scheduleRepositoryMock.Setup(repo => repo.GetById(command.Id))
            .ReturnsAsync(existingEntity);

        _scheduleRepositoryMock.Setup(repo => repo.Update(It.IsAny<ScheduleEntity>()))
            .ReturnsAsync(updatedEntity);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.HasError().Should().BeFalse();

        _scheduleRepositoryMock.Verify(repo => repo.GetById(command.Id), Times.Once);
        _scheduleRepositoryMock.Verify(repo => repo.Update(It.Is<ScheduleEntity>(dto =>
            dto.Id == command.Id &&
            dto.Name == command.Name &&
            dto.Email == command.Email &&
            dto.Phone == command.Phone &&
            dto.PhoneDDD == command.PhoneDDD &&
            dto.Observation == command.Observation &&
            dto.Status == command.Status &&
            dto.MotorcycleId == command.MotorcycleId)), Times.Once);
    }

    [Test]
    public async Task Handle_ShouldReturnFalse_WhenUpdateFails()
    {
        // Arrange
        var command = new ScheduleUpdateCommand
        {
            Id = "123",
            Name = "Updated Name",
            Email = "updated@example.com",
            Phone = 123456789,
            PhoneDDD = 11,
            Observation = "Updated Observation",
            Status = ScheduleStatusEnum.AwaitingForSchedule,
            MotorcycleId = "Motorcycle123"
        };

        _scheduleRepositoryMock.Setup(repo => repo.GetById(command.Id))
            .ReturnsAsync((ScheduleEntity)null);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.HasError().Should().BeTrue();

        _scheduleRepositoryMock.Verify(repo => repo.GetById(command.Id), Times.Once);
        _scheduleRepositoryMock.Verify(repo => repo.Update(It.IsAny<ScheduleEntity>()), Times.Never);
    }
}
