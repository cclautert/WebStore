
namespace WebStore.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordHSalt { get; set; }

        public User()
        {
            
        }
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        public User(string name, string email, string password, byte[] passwordHash, byte[] passwordHSalt)
        {
            Name = name;
            Email = email;
            Password = password;
            PasswordHash = passwordHash;
            PasswordHSalt = passwordHSalt;
        }

        public void UpdatePassword(byte[] passwordHash, byte[] passwordHSalt)
        {
            PasswordHash = passwordHash;
            PasswordHSalt = passwordHSalt;
        }
    }
}
