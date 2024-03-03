namespace Exceptions
{
    [Serializable]
    public class ThingNotFoundException : Exception
    {
        public ThingNotFoundException(string information = "Thing is not found!\n") : base(information) { }
        public ThingNotFoundException(Exception inner, string information = "Thing is not found!\n") : base(information, inner) { }
    }
    [Serializable]
    public class ThingCodeExistsException : Exception
    {
        public ThingCodeExistsException(string information = "Thing's code is exists!\n") : base(information) { }
        public ThingCodeExistsException(Exception inner, string information = "Thing's code is exists!\n") : base(information, inner) { }
    }
    
    [Serializable]
    public class ThingInRoomException : Exception
    {
        public ThingInRoomException(string information = "Thing is in this room!\n") : base(information) { }
        public ThingInRoomException(Exception inner, string information = "Thing is in this room!\n") : base(information, inner) { }
    }
    [Serializable]
    public class ThingNotFreeException : Exception
    {
        public ThingNotFreeException(string information = "Thing is not free to give to student!\n") : base(information) { }
        public ThingNotFreeException(Exception inner, string information = "Thing is not free to give to student!\n") : base(information, inner) { }
    }
    [Serializable]
    public class ThingFreeException : Exception
    {
        public ThingFreeException(string information = "Student does not keep this thing!\n") : base(information) { }
        public ThingFreeException(Exception inner, string information = "Student does not keep this thing!\n") : base(information, inner) { }
    }
}
