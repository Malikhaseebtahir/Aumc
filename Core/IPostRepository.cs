using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aumc.Core
{
    public interface IPostRepository
    {
        void RemovePost(Post post);
        Task AddPostAsync(Post post);
        Task<Post> GetPostAsync(int postId);
        Task<IEnumerable<PostCategory>> GetPostCategoriesAsync();
    }
}