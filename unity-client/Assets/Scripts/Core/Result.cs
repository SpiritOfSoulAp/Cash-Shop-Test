namespace Core
{
    public class Result<T>
    {
        public bool IsOk { get; }
        public T Value { get; }
        public ApiError Error { get; }

        private Result(T value)
        {
            IsOk = true;
            Value = value;
        }

        private Result(ApiError error)
        {
            IsOk = false;
            Error = error;
        }

        public static Result<T> Ok(T value) => new Result<T>(value);
        public static Result<T> Fail(ApiError error) => new Result<T>(error);
    }
}
