namespace GitHubTokenGenerator.Generators
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Security.Claims;
    using GitHubTokenGenerator.CommandLineOptions;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Cryptography;

    public static class AppTokenGenerator
    {
        public static string GenerateToken(AppTokenOptions options)
        {
            RsaSecurityKey securityKey = CreateRsaSecurityKeyAsync(options.PrivateKeyPath);
            JwtSecurityToken jwtToken = GenerateToken(options.AppId, securityKey);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        
        private static RsaSecurityKey CreateRsaSecurityKeyAsync(string pathToPrivateKey)
        {
            string privateKey = File.ReadAllText(pathToPrivateKey);
            byte[] privateKeyBytes = Convert.FromBase64String(CleanupPrivateKey(privateKey));
            // creating the RSA key 
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ImportRSAPrivateKey(new ReadOnlySpan<byte>(privateKeyBytes), out _);
            return new RsaSecurityKey(provider);
        }
        
        private static string CleanupPrivateKey(string keyText)
        {
            // keeping only key itself
            return keyText
                .Replace("\r\n", "")
                .Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                .Replace("-----END RSA PRIVATE KEY-----", "").Trim();
        }
        
        private static JwtSecurityToken GenerateToken(string clientId, SecurityKey rsaSecurityKey)
        {
            DateTime now = DateTime.UtcNow.AddSeconds(-60);
            long issuedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
            Claim[] claims = {
                new(JwtRegisteredClaimNames.Iss, clientId),
                new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), "http://www.w3.org/2001/XMLSchema#integer")
            };

            return new JwtSecurityToken
            (
                issuer: clientId,
                audience: null,
                claims: claims,
                notBefore:now,
                expires: now.AddMinutes(10),
                signingCredentials: new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
            );
        }

    }
}