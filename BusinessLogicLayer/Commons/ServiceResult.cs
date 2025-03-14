namespace BusinessLogicLayer.Commons
{
    public class ServiceResult
    {
        public bool IsSuccess { get; protected set; }
        public Error Error { get; protected set; }
        public virtual object? Data { get { return _data; } protected set { _data = value; } }
        protected object? _data { get; set; }

        public bool IsFailed => !IsSuccess;

        protected ServiceResult(bool success, Error error, object? data = null)
        {
            if (success && error != Error.NoError || !success && error == Error.NoError)
            {
                throw new ArgumentException("Invalid argument");
            }
            IsSuccess = success;
            Error = error;
            Data = Data;
        }

        public static ServiceResult Success(object? Data = null) => new(true, Error.NoError, Data);

        public static ServiceResult Failed(Error error) => new(false, error);
    }

    public sealed class ServiceResult<T> : ServiceResult
    {
        new public T? Data
        {
            get
            {
                return (T?)_data;
            }
            private set
            {
                _data = value;
            }
        }

        private ServiceResult(bool success, Error error, T? Data = default)
            : base(success, error, Data)
        {
            if (success && error != Error.NoError || !success && error == Error.NoError)
            {
                throw new ArgumentException("Invalid result argument");
            }

            IsSuccess = success;
            Error = error;
            this.Data = Data;
        }

        public static ServiceResult<T> Success(T? Data = default) => new(true, Error.NoError, Data);

        new public static ServiceResult<T> Failed(Error error) => new(false, error);
    }
}
