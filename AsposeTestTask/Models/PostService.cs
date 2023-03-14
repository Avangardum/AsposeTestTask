using System.Diagnostics;
using Avangardum.AsposeTestTask.Data;

namespace Avangardum.AsposeTestTask.Models;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetPost(string id) => _dbContext.Posts.Find(id);

    public List<string> GetAllPostIds() => _dbContext.Posts.Select(p => p.Id).ToList();
    
    public void CreatePost(string title, string text, string authorName)
    {
        var post = new Post { Id = Guid.NewGuid().ToString(), Title = title, Text = text, AuthorName = authorName, PublicationTime = DateTime.UtcNow};
        _dbContext.Posts.Add(post);
        _dbContext.SaveChanges();
    }

    public async Task EditPost(string id, string title, string text)
    {
        var post = GetPost(id);
        Debug.Assert(post is not null);
        post.Title = title;
        post.Text = text;
        await _dbContext.SaveChangesAsync();
    }
}