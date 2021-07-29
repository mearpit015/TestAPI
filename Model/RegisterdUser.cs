using System.ComponentModel.DataAnnotations;

namespace TestAPI.Model
{
    public class RegisterdUser
    {
        [Key]
        public  int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; } //genrate and store in DB
    }
}
