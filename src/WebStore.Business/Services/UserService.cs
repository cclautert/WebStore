using System.Security.Cryptography;
using System.Text;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Models.Validations;

namespace WebStore.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userepository, 
                           INotifier notificador) : base(notificador)
        {
            _userRepository = userepository;
        }

        public async Task CreateAsync(User user)
        {
            if (!RunValidation(new UserValidation(), user)) return;

            if (user.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                byte[] passwordSalt = hmac.Key;

                user.UpdatePassword(passwordHash, passwordSalt);
            }

            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            if (!RunValidation(new UserValidation(), user)) return;

            if (user.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                byte[] passwordSalt = hmac.Key;

                user.UpdatePassword(passwordHash, passwordSalt);
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> AuthenticationAsync(string email, string password)
        {
            if (password != null)
            {
                User user = await _userRepository.FindByEmailAsync(email);

                if (user.PasswordHSalt != null)
                {
                    using var hmac = new HMACSHA512(user.PasswordHSalt);
                    var computedHas = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHas.Length; i++)
                    {
                        if (computedHas[i] != user.PasswordHash[i]) return false;
                    }
                    return true;
                }
            }

            return false;
        }

        public async Task RemoveAsync(Guid id)
        {
            await _userRepository.RemoveAsync(id);
        }

        public string GenerateToken(string id, string email)
        {
            return _userRepository.GenerateToken(id, email);
        }
    }
}
