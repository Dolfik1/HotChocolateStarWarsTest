using HotChocolate.Types;
using StarWars.Models;

namespace StarWars.Types
{
    public class TokenDataType
        : ObjectType<TokenData>
    {
        protected override void Configure(IObjectTypeDescriptor<TokenData> desc)
        {
            desc.Field(t => t.RefreshToken).Description("User refresh token.");
            desc.Field(t => t.AccessToken).Description("User access token.");

            desc.Field(t => t.ExpiresIn)
                .Description("Access token expires date.");
        }
    }
}
