using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Address;
using MotorcycleMaintenanceSchedule.Domain.Entities.Contacts;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Maintence.Queries.List;

public class MaintenanceListHandler : IRequestHandler<MaintenanceListParamsQuery, ActionResult>
{
    public async Task<ActionResult> Handle(MaintenanceListParamsQuery request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetData(new List<ScheduleEntity>
        {
            new ()
            {
                Id = "1",
                Name = "a",
                Observation = "test",
                SchedulingDate = DateTime.UtcNow,
                Contacts =  
                [
                    new ContactEntity()
                    {
                        Id = "1",
                        Email = "a@a.com",
                        Name = "Name",
                        Phone = 11111111,
                        PhoneDDD = 11,
                        AdressId = "1",
                        Adress = new AddressEntity()
                        {
                            Id = "1",
                            CityId = "1",
                            Number = "1",
                            Street = "street",
                            ZipCode = "1",
                            Neighborhood = "a"
                        },
                    }
                ],
                Address = new AddressEntity()
                {
                    Id = "2",
                    CityId = "1",
                    Number = "1",
                    Street = "street",
                    ZipCode = "2",
                    Neighborhood = "a"
                }
            },
            new ()
            {
                Id = "1",
                Name = "a",
                Observation = "test",
                SchedulingDate = DateTime.UtcNow,
                Contacts =
                [
                    new ContactEntity()
                    {
                        Id = "1",
                        Email = "a@a.com",
                        Name = "Name",
                        Phone = 11111111,
                        PhoneDDD = 11,
                        AdressId = "1",
                        Adress = new AddressEntity()
                        {
                            Id = "1",
                            CityId = "1",
                            Number = "1",
                            Street = "street",
                            ZipCode = "1",
                            Neighborhood = "a"
                        },
                    }
                ],
                Address = new AddressEntity()
                {
                    Id = "2",
                    CityId = "1",
                    Number = "1",
                    Street = "street",
                    ZipCode = "2",
                    Neighborhood = "a"
                }
            }
        });

        apiReponse.SetPaginationMetadata(currentPage: 0, pageSize: 10, totalRecords: 2);

        return apiReponse;
    }
}
