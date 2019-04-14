using System;
using System.Linq;
using Auth.Context;
using Auth.Context.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    public class AuthTest
    {
        private readonly AuthContext _context;

        public AuthTest()
        {
            var options = new DbContextOptionsBuilder<AuthContext>()
                .UseNpgsql("User ID=sa;Password=sa;Host=localhost;Port=5431;Database=dev;Pooling=true;")
                .Options;
                
            _context = new AuthContext(options);
        }
        
        [Fact]
        public void InserUserTest()
        {
            _context.Users.Add(new ApplicationUser
            {
                Email = "test@mail.ru",
                UserName = Guid.NewGuid().ToString(),
                PasswordHash = Guid.NewGuid().ToString()
            });
            _context.SaveChanges();

            _context.Users.Count().Should().BeGreaterOrEqualTo(1);
        }
    }
}