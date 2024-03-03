namespace Exceptions
{
    [Serializable]
    public class RoomNotFoundException : Exception
    {
        public RoomNotFoundException(string information = "Room is not found!\n") : base(information) { }
        public RoomNotFoundException(Exception inner, string information = "Room is not found!\n") : base(information, inner) { }
    }
}
