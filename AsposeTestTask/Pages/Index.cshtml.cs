﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Avangardum.AsposeTestTask.Pages;

public class Index : PageModel
{
    public IActionResult OnGet()
    {
        return RedirectToPage(nameof(Feed));
    }
}