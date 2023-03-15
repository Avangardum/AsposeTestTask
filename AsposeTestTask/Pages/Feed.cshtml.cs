using Avangardum.AsposeTestTask.Data;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

public class Feed : PageModel
{
    public record PostViewModel(string Id, string Title, string Text, string AuthorName, bool CanEdit);

    private readonly PostService _postService;
    private readonly IAuthorizationService _authService;

    public Feed(PostService postService, IAuthorizationService authService)
    {
        _postService = postService;
        _authService = authService;
    }

    public List<PostViewModel> PostViewModels { get; set; } = new();

    public async Task OnGet()
    {
        var posts = await _postService.GetAllPosts();
        var getPostViewModelTasks = posts.Select(ConvertPostToViewModel).ToList();
        await Task.WhenAll(getPostViewModelTasks);
        PostViewModels = getPostViewModelTasks.Select(t => t.Result).ToList();
    }

    private async Task<PostViewModel> ConvertPostToViewModel(Post post)
    {
        var canEdit = (await _authService.AuthorizeAsync(User, post, "CanManagePost")).Succeeded;
        return new PostViewModel(post.Id, post.Title, post.Text, post.AuthorName, canEdit);
    }
}