using System.Diagnostics;
using Avangardum.AsposeTestTask.Data;
using Microsoft.EntityFrameworkCore;

namespace Avangardum.AsposeTestTask.Models;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post> GetPost(string id) => await _dbContext.Posts.FindAsync(id);

    public async Task<List<Post>> GetAllPosts() => await _dbContext.Posts.ToListAsync();

    public async Task CreatePost(string title, string text, string authorName)
    {
        var post = new Post { Id = Guid.NewGuid().ToString(), Title = title, Text = text, AuthorName = authorName, 
            PublicationTime = DateTime.UtcNow };
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditPost(string id, string title, string text)
    {
        var post = await GetPost(id);
        Debug.Assert(post is not null);
        post.Title = title;
        post.Text = text;
        await _dbContext.SaveChangesAsync();
    }
}