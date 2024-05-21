using Microsoft.AspNetCore.Identity;
using UsersAPI.Domain.Entities;
using UsersAPI.Infraestructure.Data.Context;

namespace UsersAPI.Aplication.Stores
{
    public class UserStore : IUserStore<User> , IUserPasswordStore<User>
    {
        private readonly MyMicroservicesContext _context;
        public UserStore(MyMicroservicesContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var userData = _context.Users.Find(user.Id);

            _context.Remove(userData);
            _context.SaveChanges();

            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userData = _context.Users.ToList().Find(x => x.Id == userId);

            return userData;
        }

        public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var userData = _context.Users.ToList().Find(x => x.NormalizedUserName == normalizedUserName);

            return userData;
        }

        public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.NormalizedUserName);
        }

        public Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.UserName);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var userData = _context.Users.Find(user.Id);

            userData.UserName = user.UserName;
            userData.NormalizedUserName = user.NormalizedUserName;
            userData.PasswordHash = user.PasswordHash;

            _context.Users.Update(userData);
            _context.SaveChanges();

            return IdentityResult.Success;
        }
    }
}
