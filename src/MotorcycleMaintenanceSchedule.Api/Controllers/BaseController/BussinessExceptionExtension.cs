
namespace MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;

[Serializable]
public sealed class BussinessExceptionExtension : Exception
{

    public BussinessExceptionExtension(object DataException)
    {
        this.DataException = DataException;
    }

    public object DataException { get; }

}