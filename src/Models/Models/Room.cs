using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum RoomTypes
    {
        None,
        StudentRoom,
        Storage
    };
    public class Room
    {
        [Key]
        public int Id_room { get; set; }
        public int Number { get; set; }
        public RoomTypes Roomtype { get; set; }  
    }
}
