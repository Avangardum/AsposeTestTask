using Avangardum.AsposeTestTask.Data;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

public class Feed : PageModel
{
    public record PostViewModel(string Text, string AuthorName);

    private readonly PostService _postService;
    private readonly UserService _userService;

    public Feed(PostService postService, UserService userService)
    {
        _postService = postService;
        _userService = userService;
    }

    public List<PostViewModel> Posts { get; set; } = new();

    public void OnGet()
    {
        Posts = ((IEnumerable<string>)_postService.GetAllPostIds())
            .Reverse()
            .Select(_postService.GetPost)
            .Select(ConvertPostToViewModel)
            .ToList();
    }

    private PostViewModel ConvertPostToViewModel(Post post) => new(post.Text, _userService.GetUserName(post.AuthorId));
}