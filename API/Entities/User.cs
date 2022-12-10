using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserId  { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
    }
}
