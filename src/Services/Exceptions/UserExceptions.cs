namespace Exceptions
{
    [Serializable]
    public class IncorrectPasswordExcept : Exception
    {
        public IncorrectPasswordExcept(string information = "Wrong password!\n") : base(information) { }
        public IncorrectPasswordExcept(Exception inner, string information = "Wrong password!\n") : base(information, inner) { }
    }
    [Serializable]
    public class LoginNotFoundException : Exception
    {
        public LoginNotFoundException(string information = "Wrong login!\n") : base(information) { }
        public LoginNotFoundException(Exception inner, string information = "Wrong login!\n") : base(information, inner) { }
    }
    [Serializable]
    public class OldPasswordWrongException : Exception
    {
        public OldPasswordWrongException(string information = "Old password is not correct!\n") : base(information) { }
        public OldPasswordWrongException(Exception inner, string information = "Old password is not correct!") : base(information, inner) { }
    }
    [Serializable]
    public class RepeateNewPassWordWrongException : Exception
    {
        public RepeateNewPassWordWrongException(string information = "Confirm password is not correct!\n") : base(information) { }
        public RepeateNewPassWordWrongException(Exception inner, string information = "Confirm password is not correct!\n") : base(information, inner) { }
    }
    [Serializable]
    public class LoginExistsException : Exception
    {
        public LoginExistsException(string information = "Login is exists!\n") : base(information) { }
        public LoginExistsException(Exception inner, string information = "Login is exists!\n") : base(information, inner) { }
    }
    [Serializable]
    public class InputInvalidException : Exception
    {
        public InputInvalidException(string information = "Input is invalid\n") : base(information) { }
        public InputInvalidException(Exception inner, string information = "Input is invalid!\n") : base(information, inner) { }
    }
}
