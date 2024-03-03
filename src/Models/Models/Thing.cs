using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Thing
    {
        [Key]
        public int Id_thing { get; set; }
        public string Code { get; set; }
        public string? Type { get; set; }
        public int Id_room { get; set; }
        public int Id_student { get; set; }
    }
}
