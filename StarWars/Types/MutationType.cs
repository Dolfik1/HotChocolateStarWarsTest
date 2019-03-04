using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;

namespace StarWars.Types
{
    public class MutationType
        : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(t => t.CreateReview(default, default, default))
                .Type<NonNullType<ReviewType>>()
                .Argument("episode", a => a.Type<NonNullType<EpisodeType>>())
                .Argument("review", a => a.Type<NonNullType<ReviewInputType>>());

            descriptor.Field(t => t.CreateToken(default, default))
                .Type<NonNullType<TokenDataType>>()
                .Argument("name", a => a
                    .Type<NonNullType<StringType>>())
                .Argument("email", a => a
                    .Type<NonNullType<StringType>>());

            descriptor.Field(t => t.JwtAuthorizedMutation())
                .Type<NonNullType<BooleanType>>()
                .Directive(new AuthorizeDirective());
        }
    }
}
