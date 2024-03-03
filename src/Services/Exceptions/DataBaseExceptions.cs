namespace Exceptions
{
    [Serializable]
    public class DataBaseError : Exception
    {
        public DataBaseError(string information = "Connect to database failed!\n") : base(information) { }
        public DataBaseError(Exception inner, string information = "Connect to database failed!\n") : base(information, inner) { }
    }
}
