using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Clean.Services
{
    public sealed class InMemoryAuthenticationService : IAuthenticationService
    {
        private static readonly Dictionary<string, string> _users = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "admin", "admin" },
            { "user", "user" }
        };

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return _users.TryGetValue(username, out string storedPassword)
                   && string.Equals(storedPassword, password, StringComparison.Ordinal);
        }

        public ClaimsIdentity CreateIdentity(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username)
            };

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}
