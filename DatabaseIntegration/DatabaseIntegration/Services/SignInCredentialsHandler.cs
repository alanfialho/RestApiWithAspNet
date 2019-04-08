using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace DatabaseIntegration.Services
{
    public static class SignInCredentialsHandler
    {
        public static SigningCredentials Credentials { get; }

        static SignInCredentialsHandler()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                var key = new RsaSecurityKey(provider.ExportParameters(true));
                Credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            }
        }
    }
}
