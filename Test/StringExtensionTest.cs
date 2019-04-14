using Auth.Context;
using FluentAssertions;
using Xunit;

namespace Test
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("RoleNameIndex", "role_name_index")]
        [InlineData("AspNet", "asp_net")]
        [InlineData("users", "users")]
        [InlineData("authRoles", "auth_roles")]
        [InlineData("auth_Roles", "auth_roles")]
        public void ToSnakeCase(string value, string expected)
        {
            value.ToSnakeCase().Should().Be(expected);
        }
    }
}