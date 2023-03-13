﻿using Avangardum.AsposeTestTask.Data;

namespace Avangardum.AsposeTestTask.Models;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetPost(string id) => _dbContext.Posts.SingleOrDefault(p => p.Id == id);

    public List<string> GetAllPostIds() => _dbContext.Posts.Select(p => p.Id).ToList();
}