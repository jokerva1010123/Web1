namespace Exceptions
{
    [Serializable]
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(string information = "Student is not found!\n") : base(information) { }
        public StudentNotFoundException(Exception inner, string information = "Student is not found!\n") : base(information, inner) { }
    }
    [Serializable]
    public class StudentPageException : Exception
    {
        public StudentPageException(string information = "Student is not found!\n") : base(information) { }
        public StudentPageException(Exception inner, string information = "Student is not found!\n") : base(information, inner) { }
    }
    [Serializable]
    public class StudentInRoomException : Exception
    {
        public StudentInRoomException(string information = "Student is in this room!\n") : base(information) { }
        public StudentInRoomException(Exception inner, string information = "Student is in this room!\n") : base(information, inner) { }
    }
    [Serializable]
    public class StudentNotLiveException : Exception
    {
        public StudentNotLiveException(string information = "Student does not live in dormitory!\n") : base(information) { }
        public StudentNotLiveException(Exception inner, string information = "Student does not live in dormitory!\n") : base(information, inner) { }
    }
}
