using FluentAssertions;
using Moq;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.ApplicationTests.Services.Internal.Schedule.Queries.List;

public class ScheduleGetOneHandlerTests
{
    private Mock<IScheduleListRepository> _scheduleRepositoryMock;
    private ScheduleListHandler _handler;

    [SetUp]
    public void Setup()
    {
        _scheduleRepositoryMock = new Mock<IScheduleListRepository>();
        _handler = new ScheduleListHandler(_scheduleRepositoryMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnPaginatedResult_WhenCalledWithValidParams()
    {
        // Arrange
        var scheduleEntities = new List<ScheduleEntity>();
        for (int i = 1; i <= 10; i++)
        {
            scheduleEntities.Add(new ScheduleEntity
            {
                Id = i.ToString(),
                Name = $"Name{i}",
                Email = $"email{i}@example.com",
                Phone = 123456789,
                PhoneDDD = 11,
                Observation = $"Observation{i}",
                Status = ScheduleStatusEnum.AwaitingForSchedule,
                MotorcycleId = $"MotorcycleId{i}"
            });
        }
        var paginatedResult = new ActionResult();
        paginatedResult.SetData(scheduleEntities.Skip(5).Take(5).ToList());
        paginatedResult.SetPaginationMetadata(new PaginationMetadata(2, 5, 10));

        _scheduleRepositoryMock.Setup(repo => repo.List(It.IsAny<ScheduleListParams>()))
            .ReturnsAsync(paginatedResult);

        var query = new ScheduleListParamsQuery(new ScheduleListParams
        {
            PageNumber = 2,
            PageSize = 5
        });

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasData().Should().BeTrue();

        var expectedSchedule = new
        {
            data = result.GetData()
        };

        expectedSchedule.Should().BeEquivalentTo(expectedSchedule);
    }
}