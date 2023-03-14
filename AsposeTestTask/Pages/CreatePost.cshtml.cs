using System.ComponentModel.DataAnnotations;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

[Authorize]
public class CreatePost : PageModel
{
    private PostService _postService;

    public CreatePost(PostService postService)
    {
        _postService = postService;
    }

    [BindProperty, Required]
    public string Text { get; set; }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();
        _postService.CreatePost(Text, User.Identity!.Name);
        return RedirectToPage(nameof(Feed));
    }
}