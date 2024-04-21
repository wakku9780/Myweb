using Microsoft.EntityFrameworkCore;
using Myweb.Data;
using Myweb.Models;

namespace Myweb.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            // Retrieve all posts from the database
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            // Retrieve a post by its id from the database
            return await _context.Posts.FindAsync(id);
        }

        public async Task CreatePostAsync(Post post)
        {
            // Add a new post to the database
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            // Update an existing post in the database
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            // Delete a post from the database
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
