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
    private UserService _userService;

    public CreatePost(PostService postService, UserService userService)
    {
        _postService = postService;
        _userService = userService;
    }

    [BindProperty, Required]
    public string Text { get; set; }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();
        _postService.CreatePost(Text, _userService.GetUserId(User.Identity.Name));
        return RedirectToPage(nameof(Feed));
    }
}