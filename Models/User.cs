using System.ComponentModel.DataAnnotations;

namespace FileSharing.Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string Username, string Password) 
        {
            this.Username = Username;
            this.Password = Password;

        }
    }
}