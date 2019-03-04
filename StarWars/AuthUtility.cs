using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace StarWars
{

    public static class AuthUtility
    {
        public static string GetUserId(HttpContext context)
        {
            return context?.User?.Claims
                ?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)
                ?.Value;
        }

        public static string BuildAccessToken(
            string id,
            string email,
            string name,
            DateTimeOffset expires)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, id),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("name", name),
            };

            return BuildToken(claims, expires);
        }

        public static string BuildRefreshToken(
            string id,
            string email,
            string name,
            DateTimeOffset expires)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, id),
                new Claim("rfs", "true"), // refresh token
            };

            return BuildToken(claims, expires);
        }

        private static string BuildToken(
            IEnumerable<Claim> claims,
            DateTimeOffset expires)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.JwtIssuer,
                Constants.JwtIssuer,
                claims,
                expires: expires.DateTime,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
