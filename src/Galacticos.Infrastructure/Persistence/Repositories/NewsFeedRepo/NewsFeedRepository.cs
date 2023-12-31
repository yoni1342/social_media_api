using Galacticos.Application.Persistence.Contracts;
using Galacticos.Infrastructure.Data;
using Galacticos.Domain.Entities;

namespace Galacticos.Infrastructure.Persistence.Repositories
{
    public class NewsFeedRepository : INewsFeedRepository
    {
        private readonly ApiDbContext _dbContext;
        private readonly IRelationRepository _relationRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public NewsFeedRepository(ApiDbContext dbContext, IRelationRepository relationRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _relationRepository = relationRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Post>> GetNewsFeedForUser(Guid userId, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;

            var followedUserIds = await _relationRepository.GetAllFollowedIdsByUserId(userId);
            var aggregatedPosts = new List<Post>();

            foreach (var followedUserId in followedUserIds)
            {
                var postsFromFollowedUser = await _postRepository.GetPostsByUserId(followedUserId.Id);
                aggregatedPosts.AddRange(postsFromFollowedUser);
            }

            var paginatedPosts = aggregatedPosts
                .OrderByDescending(post => post.CreatedAt)
                .Skip(itemsToSkip)
                .Take(pageSize);

            var newsFeedData = new List<Post>();

            foreach (var post in paginatedPosts)
            {
                var author = _userRepository.GetUserById(post.UserId);
                newsFeedData.Add(post);
            }

            newsFeedData.Sort((x, y) => DateTime.Compare(y.CreatedAt, x.CreatedAt));
            return newsFeedData;
        }
    }
}