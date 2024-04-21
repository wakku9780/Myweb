using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Myweb.Models;

namespace Myweb.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // Add this field

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager) // Modify the constructor
        {
            _userManager = userManager;
            _signInManager = signInManager; // Initialize _signInManager
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            // Create a new user with the provided details and password
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<SignInResult> LoginAsync(string username, string password)
        {
            // Attempt to sign in the user with the provided username and password
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

            return result;
        }
    }
}
