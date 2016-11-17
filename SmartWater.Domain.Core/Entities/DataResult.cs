namespace SmartWater.Domain.Core.Entities
{
    public class DataResult<T>
    {
        public bool Ok { get; private set; }
        public string Message { get; private set; }
        public T Result { get; private set; }


        protected DataResult(bool ok = true, string message = null, T result = default(T))
        {
            Message = message;
            Result  = result;
            Ok      = ok;
        }


        public static DataResult<T> Fail(string message)
        {
            return new DataResult<T>(ok: false, message: message);
        }


        public static DataResult<T> Pass(T result)
        {
            return new DataResult<T>(result: result);
        }
    }
}
