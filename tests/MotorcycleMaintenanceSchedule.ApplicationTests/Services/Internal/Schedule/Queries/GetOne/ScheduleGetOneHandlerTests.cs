using FluentAssertions;
using Moq;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.GetOne;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.ApplicationTests.Services.Internal.Schedule.Queries.GetOne;

public class ScheduleGetOneHandlerTests
{
    private Mock<IScheduleRepository> _scheduleRepositoryMock;
    private ScheduleGetOneHandler _handler;

    [SetUp]
    public void Setup()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _handler = new ScheduleGetOneHandler(_scheduleRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldGetOneScheduleAndReturnActionResult()
    {
        // Arrange
        var scheduleId = "test-id";
        var scheduleEntity = new ScheduleEntity
        {
            Id = scheduleId,
            Name = "Test Schedule",
            Email = "test@example.com",
            Phone = 123456789,
            PhoneDDD = 12,
            Observation = "Test observation",
            Status = ScheduleStatusEnum.AwaitingForSchedule,
            MotorcycleId = "motorcycle-id"
        };

        _scheduleRepositoryMock.Setup(repo => repo.GetById(scheduleId)).ReturnsAsync(scheduleEntity);

        var query = new ScheduleGetOneQuery { Id = scheduleId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasData().Should().BeTrue();

        var data = result.GetData();

        data.Should().NotBeNull();

        var entity = new ScheduleEntity
        {
            Id = scheduleEntity.Id,
            Name = scheduleEntity.Name,
            Email = scheduleEntity.Email,
            Phone = scheduleEntity.Phone,
            PhoneDDD = scheduleEntity.PhoneDDD,
            Observation = scheduleEntity.Observation,
            Status = scheduleEntity.Status,
            MotorcycleId = scheduleEntity.MotorcycleId,
            StartBusinessHour = scheduleEntity.StartBusinessHour,
            EndBusinessHour = scheduleEntity.EndBusinessHour,
            ScheduleDate = scheduleEntity.ScheduleDate
        };

        var expectedSchedule = new
        {
            data = entity
        };

        expectedSchedule.Should().BeEquivalentTo(expectedSchedule);

    }

    [Test]
    public async Task Handle_ShouldReturnErrorWhenScheduleNotFound()
    {
        // Arrange
        var scheduleId = "non-existent-id";
        _scheduleRepositoryMock.Setup(repo => repo.GetById(scheduleId)).ReturnsAsync((ScheduleEntity)null);

        var query = new ScheduleGetOneQuery { Id = scheduleId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasError().Should().BeTrue();
        result.GetError().Should().NotBeNull();
    }
}
