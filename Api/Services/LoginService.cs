using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Auth.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public interface ILoginService
    {
        Task SignUpAsync(SignUpRequest request);
        Task SignInAsync(SignInRequest request);
        Task SignOutAsync();
    }

    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task SignUpAsync(SignUpRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _signinManager.SignInAsync(user, false);
            }
            else
            {
                throw new ArgumentException(string.Join(" -> ", result.Errors.Select(x => $"{x.Code}: {x.Description}")));
            }
        }

        public async Task SignInAsync(SignInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) 
                       ?? throw new ArgumentException("Invalid email or password.");
            
            var result = await _signinManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            
            if (!result.Succeeded)
            {
                throw new ArgumentException(result.ToString());
            }
        }
        
        public async Task SignOutAsync()
        {
            await _signinManager.SignOutAsync();
        }
    }
}