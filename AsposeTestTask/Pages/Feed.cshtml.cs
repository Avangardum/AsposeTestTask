using Avangardum.AsposeTestTask.Data;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

public class Feed : PageModel
{
    public record PostViewModel(string Id, string Title, string Text, string AuthorName, bool CanEdit);

    private readonly PostService _postService;

    public Feed(PostService postService)
    {
        _postService = postService;
    }

    public List<PostViewModel> PostsViewModels { get; set; } = new();

    public async Task OnGet()
    {
        var ids = await _postService.GetAllPostIds();
        var getPostTasks = ids.Select(_postService.GetPost).ToList();
        await Task.WhenAll(getPostTasks);
        var posts = getPostTasks
            .Select(t => t.Result)
            .OrderByDescending(p => p.PublicationTime);
        PostsViewModels = posts.Select(ConvertPostToViewModel).ToList();
    }

    private PostViewModel ConvertPostToViewModel(Post post)
    {
        var canEdit = post.AuthorName == User.Identity?.Name;
        return new PostViewModel(post.Id, post.Title, post.Text, post.AuthorName, canEdit);
    }
}