using Avangardum.AsposeTestTask.Data;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

public class Feed : PageModel
{
    public record PostViewModel(string Text, string AuthorName);

    private readonly PostService _postService;

    public Feed(PostService postService)
    {
        _postService = postService;
    }

    public List<PostViewModel> Posts { get; set; } = new();

    public void OnGet()
    {
        Posts = _postService.GetAllPostIds()
            .Select(_postService.GetPost)
            .OrderByDescending(p => p.PublicationTime)
            .Select(ConvertPostToViewModel)
            .ToList();
    }

    private PostViewModel ConvertPostToViewModel(Post post) => new(post.Text, post.AuthorName);
}