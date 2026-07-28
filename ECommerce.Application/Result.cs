namespace ECommerce.Application;

public class Result<TValue, TStatus>
{
    public TValue Value { get; }
    public string Error { get;}
    public bool IsSuccess { get;}
    public TStatus Status { get; }
    
    private  Result(TValue value, string error, bool isSuccess, TStatus status)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
        Status = status;
    }
    
    public static Result<TValue, TStatus> Success(TValue value, TStatus status) => new Result<TValue, TStatus>(value, null, true, status);
    public static Result<TValue, TStatus> Failure(string error, TStatus status) => new Result<TValue, TStatus>(default, error, false, status);
}