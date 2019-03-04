using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Subscriptions;
using StarWars.Data;
using StarWars.Models;

namespace StarWars
{
    public class Mutation
    {
        private readonly ReviewRepository _repository;

        public Mutation(ReviewRepository repository)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Review> CreateReview(
            Episode episode, Review review,
            [Service]IEventSender eventSender)
        {
            _repository.AddReview(episode, review);
            await eventSender.SendAsync(new OnReviewMessage(episode, review));
            return review;
        }

        public Task<bool> JwtAuthorizedMutation() => Task.FromResult(true);

        public Task<TokenData> CreateToken(string name, string email)
        {
            var accessExpires = DateTimeOffset.Now.AddMinutes(Constants.AccessExpiresMinutes);
            var refreshExpires = DateTimeOffset.Now.AddMinutes(Constants.RefreshExpiresMinutes);

            var id = Guid.NewGuid().ToString("N");
            var result = new TokenData
            {
                AccessToken = AuthUtility.BuildAccessToken(
                    id, email, name, accessExpires),
                RefreshToken = AuthUtility.BuildRefreshToken(
                    id, email, name, refreshExpires),
                ExpiresIn = accessExpires
            };

            return Task.FromResult(result);
        }
    }
}
