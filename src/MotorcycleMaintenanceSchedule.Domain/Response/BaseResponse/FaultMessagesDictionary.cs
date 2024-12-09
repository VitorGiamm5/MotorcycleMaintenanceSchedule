namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public static class FaultMessagesDictionary
{
    public static string GetMessage(this FaultMessageEnum value)
    {
        return value switch
        {
            FaultMessageEnum.None => FaultMessagesConst.MESSAGE_ERROR_NONE,
            FaultMessageEnum.InvalidFormat => FaultMessagesConst.MESSAGE_ERROR_INVALID_FORMAT,
            FaultMessageEnum.FileSizeInvalid => FaultMessagesConst.MESSAGE_ERROR_FILE_SIZE_INVALID,
            FaultMessageEnum.InvalidDate => FaultMessagesConst.MESSAGE_ERROR_INVALID_DATE,
            FaultMessageEnum.UpdateFail => FaultMessagesConst.MESSAGE_ERROR_UPDATE_FAIL,
            FaultMessageEnum.CreateFail => FaultMessagesConst.MESSAGE_ERROR_CREATE_FAIL,
            FaultMessageEnum.Required => FaultMessagesConst.MESSAGE_ERROR_REQUIRED,
            FaultMessageEnum.MustBeUnic => FaultMessagesConst.MESSAGE_ERROR_MUST_BE_UNIC,
            FaultMessageEnum.Unavailable => FaultMessagesConst.MESSAGE_ERROR_UNAVAILABLE,
            FaultMessageEnum.AlreadyExist => FaultMessagesConst.MESSAGE_ERROR_ALREADY_EXIST,
            FaultMessageEnum.NotFound => FaultMessagesConst.MESSAGE_ERROR_NOT_FOUND,
            FaultMessageEnum.InvalidData => FaultMessagesConst.MESSAGE_ERROR_INVALID_DATA,
            FaultMessageEnum.DataNotFound => FaultMessagesConst.MESSAGE_ERROR_DATA_NOT_FOUND,
            FaultMessageEnum.CreateItem => FaultMessagesConst.MESSAGE_ERROR_CREATE_ITEM,
            FaultMessageEnum.InvalidOperation => FaultMessagesConst.MESSAGE_ERROR_INVALID_OPERATION,
            FaultMessageEnum.NullArgument => FaultMessagesConst.MESSAGE_ERROR_NULL_ARGUMENT,
            FaultMessageEnum.UnauthorizedAccess => FaultMessagesConst.MESSAGE_ERROR_UNAUTHORIZED_ACCESS,
            FaultMessageEnum.InvalidPayload => FaultMessagesConst.MESSAGE_ERROR_INVALID_PAYLOAD,
            FaultMessageEnum.InvalidParameters => FaultMessagesConst.MESSAGE_ERROR_INVALID_PARAMETERS,
            FaultMessageEnum.DatabaseInternal => FaultMessagesConst.MESSAGE_ERROR_DATABASE_INTERNAL,
            FaultMessageEnum.BusinessException => FaultMessagesConst.MESSAGE_ERROR_BUSINESS_EXCEPTION,
            _ => FaultMessagesConst.MESSAGE_ERROR_INTERNAL_ERROR
        };
    }
}
