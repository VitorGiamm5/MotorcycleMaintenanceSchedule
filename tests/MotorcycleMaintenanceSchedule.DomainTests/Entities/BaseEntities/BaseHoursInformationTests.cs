using FluentAssertions;
using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using NUnit.Framework;

namespace MotorcycleMaintenanceSchedule.DomainTests.Entities.BaseEntities;

public static class BaseHoursInformationTests
{
    [Test]
    public static void IsSchedulingDateValid_ShouldReturnTrue_WhenWithinBusinessHours()
    {
        // Arrange
        var hoursInfo = new BaseHoursInformation
        {
            StartBusinessHour = DateTimeOffset.Parse("2024-12-04T08:00:00Z"),
            EndBusinessHour = DateTimeOffset.Parse("2024-12-04T18:00:00Z"),
            SchedulingDate = DateTimeOffset.Parse("2024-12-04T15:30:00Z")
        };

        // Act
        var result = hoursInfo.IsSchedulingDateValid();

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public static void IsSchedulingDateValid_ShouldReturnFalse_WhenBeforeBusinessHours()
    {
        // Arrange
        var hoursInfo = new BaseHoursInformation
        {
            StartBusinessHour = DateTimeOffset.Parse("2024-12-04T08:00:00Z"),
            EndBusinessHour = DateTimeOffset.Parse("2024-12-04T18:00:00Z"),
            SchedulingDate = DateTimeOffset.Parse("2024-12-04T07:59:00Z")
        };

        // Act
        var result = hoursInfo.IsSchedulingDateValid();

        // Assert
        result.Should().BeFalse();
    }
}