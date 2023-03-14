using System.ComponentModel.DataAnnotations;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

[Authorize]
public class CreatePost : PageModel
{
    public record InputModel([Required] string Title, [Required] string Text);
    
    private PostService _postService;

    public CreatePost(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty] 
    public InputModel Input { get; set; }
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();
        await _postService.CreatePost(Input.Title, Input.Text, User.Identity!.Name);
        return RedirectToPage(nameof(Feed));
    }
}