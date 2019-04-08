using DatabaseIntegration.Model;
using DatabaseIntegration.Model.Context;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace DatabaseIntegration.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        CursoAspNetCoreContext _context;

        public AuthenticationService(CursoAspNetCoreContext context)
        {
            _context = context;
        }

        public Authentication Authenticate(User user)
        {
            var userResult = _context.Users.SingleOrDefault(u => u.Login.Equals(user.Login));
            bool isValid = userResult != null && userResult.Password.Equals(user.Password);

            if (isValid)
                return new Authentication(Create(user.Login), true);
            else
                return new Authentication(new Token(), false);
        }

        private Token Create(string login)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, login)
                    });

            var handler = new JwtSecurityTokenHandler();
            DateTime now = DateTime.Now;
            DateTime expiryAt = now.Add(TimeSpan.FromSeconds(Token.SecondsToExpiry));

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = Token.Issuer,
                Audience = Token.Audience,
                SigningCredentials = SignInCredentialsHandler.Credentials,
                Subject = identity,
                NotBefore = now,
                Expires = expiryAt
            });

            return new Token(handler.WriteToken(securityToken), now, expiryAt); 
        }
    }
}
