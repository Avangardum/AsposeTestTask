using Avangardum.AsposeTestTask.Data;

namespace Avangardum.AsposeTestTask.Models;

public class UserService
{
    private ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string GetUserName(string id) => _dbContext.Users.SingleOrDefault(u => u.Id == id)?.UserName;
}