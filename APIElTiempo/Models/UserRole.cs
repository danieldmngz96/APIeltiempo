using System.ComponentModel.DataAnnotations;
using static APIElTiempo.Models.Users;

namespace APIElTiempo.Models
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
