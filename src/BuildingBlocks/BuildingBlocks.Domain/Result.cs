namespace BuildingBlocks.Domain
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error.Type != ErrorType.None)
                throw new Exception();

            if (!isSuccess && error.Type == ErrorType.None)
                throw new Exception();

            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }

        public Error? Error { get; }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }
        public static Result<T> Success<T>(T result = default)
        {
            return new Result<T>(result, true, Error.None);

        }
        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }
        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(default, false, error);
        }
    }

    public class Result<T> : Result
    {
        public Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        private readonly T _value;
        public T? Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }


    }
}
