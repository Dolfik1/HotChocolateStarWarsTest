using System;

namespace StarWars.Models
{
    public class TokenData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpiresIn { get; set; }
    }
}
