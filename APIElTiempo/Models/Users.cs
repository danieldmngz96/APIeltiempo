using System.ComponentModel.DataAnnotations;

namespace APIElTiempo.Models
{
    public class Users
    {


        public class User
        {
            [Key]
            public int UserId { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Email { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public ICollection<UserRole> UserRoles { get; set; }

        }

    }
}
