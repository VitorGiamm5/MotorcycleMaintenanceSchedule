namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public enum FaultMessageEnum
{
    None,
    InvalidFormat,
    FileSizeInvalid,
    InvalidDate,
    UpdateFail,
    CreateFail,
    MustBeUnic,
    AlreadyExist,
    Unavailable,
    Required,
    NotFound,
    InvalidData,
    DataNotFound,
    CreateItem,
    InvalidOperation,
    NullArgument,
    UnauthorizedAccess,
    InvalidPayload,
    InvalidParameters,
    DatabaseInternal,
    BusinessException
}