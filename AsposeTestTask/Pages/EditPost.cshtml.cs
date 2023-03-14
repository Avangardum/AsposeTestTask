using System.ComponentModel.DataAnnotations;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

[Authorize]
public class EditPost : PageModel
{
    public record InputModel([Required] string Title, [Required] string Text);

    private readonly PostService _postService;
    private readonly IAuthorizationService _authService;

    public EditPost(PostService postService, IAuthorizationService authService)
    {
        _postService = postService;
        _authService = authService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    [BindProperty(SupportsGet = true)] 
    public string Id { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        var post = await _postService.GetPost(Id);
        if (post == null) return NotFound();
        var authResult = await _authService.AuthorizeAsync(User, post, "CanManagePost");
        if (!authResult.Succeeded) return Forbid();

        Input = new InputModel(post.Title, post.Text);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var post = await _postService.GetPost(Id);
        if (post is null) return NotFound();
        var authResult = await _authService.AuthorizeAsync(User, post, "CanManagePost");
        if (!authResult.Succeeded) return Forbid();
        if (!ModelState.IsValid) return Page();

        await _postService.EditPost(Id, Input.Title, Input.Text);
        return RedirectToPage(nameof(Feed));
    }
}