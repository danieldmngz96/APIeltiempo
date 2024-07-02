using System.ComponentModel.DataAnnotations;

namespace APIElTiempo.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
