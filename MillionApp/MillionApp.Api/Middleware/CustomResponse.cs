namespace MillionApp.Api.Middleware;

public class CustomResponse<T> where T : class
{
    public bool Success { get; set; }
    public int ErrorCode { get; }
    public string Message { get; }
    public T? Data { get; }
    private CustomResponse(bool success, int errorCode, string message, T? data)
    {
        Success = success;
        ErrorCode = errorCode;
        Message = message;
        Data = data;
    }

    public static CustomResponse<T> BuildSuccess(T data)
    {
        return new CustomResponse<T>(true, 0, string.Empty, data);
    }

    public static CustomResponse<T> BuildError(int errorCode, string message)
    {
        return new CustomResponse<T>(false, errorCode, message, null);
    }
}
