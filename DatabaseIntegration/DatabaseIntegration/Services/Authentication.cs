using System;

namespace DatabaseIntegration.Services
{
    public class Authentication
    {
        public Token Created { get; }
        public bool IsValid { get; }

        internal Authentication(Token token, bool isValid)
        {
            Created = token;
            IsValid = isValid;
        }
    }
}
