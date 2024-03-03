using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum Levels
    {
        NONE,
        STUDENT,
        KAMEDAN,
        MANAGER
    }
    public class User
    {
        [Key]
        public int ID { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public Levels Level { get; set; }
    }
}
