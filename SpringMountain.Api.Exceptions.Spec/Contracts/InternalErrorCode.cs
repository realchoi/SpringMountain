namespace SpringMountain.Api.Exceptions.Contracts;

/// <summary>
/// 内部错误码枚举
/// </summary>
public enum InternalErrorCode
{
    BadRequest = 400000,
    InvalidParameter = 400100,
    MissingParameter = 400101,
    ConstraintViolation = 400200,
    DuplicateRequest = 400300,
    AlreadyExisted = 400301,
    Unauthenticated = 401000,
    WrongPassword = 401001,
    WrongUserPass = 401002,
    Forbidden = 403000,
    NotFound = 404000,
    TenantNotFound = 404100,
    InternalServerError = 500000,
    InvalidData = 500001,
    ExternalUnavailable = 500100,
    RpcFailed = 500200,
    DatabaseUnavailable = 500300,
    DatabaseTimeout = 500301,
    MessageQueueError = 500400,
    CacheUnavailable = 500500,
    ServiceUnavailable = 503000,
    UnderMaintenance = 503001
}