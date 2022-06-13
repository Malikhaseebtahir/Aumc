using Aumc.Core;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class PostRepository: IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostCategory>> GetPostCategoriesAsync()
        {
            return await _context.PostCategories.ToListAsync();
        }

        public async Task<Post> GetPostAsync(int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public void RemovePost(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}