using AppCoreSystem.Application.Common.Wrappers;

namespace AppCoreSystem.Application.Common.Bases
{
    public class Result<T> : Result
    {
        public T? Data { get; set; }
        public static new Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
        public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
    }
}
