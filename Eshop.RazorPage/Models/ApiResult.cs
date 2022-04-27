namespace Eshop.RazorPage.Models;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public MetaData MetaData { get; set; }

    public bool IsReload { get; set; } = false;
    public static ApiResult Error(string message, bool isReload = false)
    {
        return new ApiResult()
        {
            IsSuccess = false,
            IsReload = isReload,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.ServerError,
                Message = message
            }
        };
    }
    public static ApiResult Error()
    {
        return new ApiResult()
        {
            IsSuccess = false,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.ServerError,
                Message = "عملیات ناموفق"
            }
        };
    }
    public static ApiResult Success(string message, bool isReload = false)
    {
        return new ApiResult()
        {
            IsSuccess = false,
            IsReload = isReload,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.ServerError,
                Message = message
            }
        };
    }
    public static ApiResult Success()
    {
        return new ApiResult()
        {
            IsSuccess = false,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.ServerError,
                Message = "عملیات با موفقیت انجام شد"
            }
        };
    }
}
public class ApiResult<TData>
{
    public bool IsSuccess { get; set; }
    public TData Data { get; set; }
    public MetaData MetaData { get; set; }


    public static ApiResult<TData> Success(TData data)
    {
        return new ApiResult<TData>()
        {
            IsSuccess = false,
            Data = data,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.ServerError,
                Message = "عملیات با موفقیت انجام شد"
            }
        };
    }
}
public class MetaData
{
    public string Message { get; set; }
    public AppStatusCode AppStatusCode { get; set; }
}

public enum AppStatusCode
{
    Success = 1,
    NotFound = 2,
    BadRequest = 3,
    LogicError = 4,
    UnAuthorize = 5,
    ServerError
}