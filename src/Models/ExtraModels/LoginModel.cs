using System.ComponentModel.DataAnnotations;

namespace ExtraModels
{
    public class LoginModel
    {
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
