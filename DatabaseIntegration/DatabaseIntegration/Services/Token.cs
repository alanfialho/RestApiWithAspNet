using System;

namespace DatabaseIntegration.Services
{
    public class Token
    {
        public const string Audience = "AlanAudience";
        public const string Issuer = "AlanIssuer";
        public const int SecondsToExpiry = 1200;

        public string Hash { get; }
        public DateTime CreatedAt { get; }
        public DateTime ExpiryAt { get; }

        public Token(string hash, DateTime createdAt, DateTime expiryAt)
        {
            Hash = hash;
            CreatedAt = createdAt;
            ExpiryAt = expiryAt;
        }

        public Token(){}
    }
}
