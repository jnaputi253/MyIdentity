using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyIdentity.Models;

namespace MyIdentity.Infrastructure
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        private readonly List<IdentityError> _identityErrors;

        public PasswordValidator()
        {
            _identityErrors = new List<IdentityError>();
        }

        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                AddError("UsernameInPassword", "Username can't be in password");
            }

            if (password.Contains("1234567890"))
            {
                AddError("PasswordContainsSequence", "Numbers not allowed");
            }

            return Task.FromResult(_identityErrors.Any()
                ? IdentityResult.Failed(_identityErrors.ToArray())
                : IdentityResult.Success);
        }

        private void AddError(string code, string description)
        {
            _identityErrors.Add(new IdentityError
            {
                Code = code,
                Description = description
            });
        }
    }
}
