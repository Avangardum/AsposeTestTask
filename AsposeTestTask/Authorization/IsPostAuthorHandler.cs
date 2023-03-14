using Avangardum.AsposeTestTask.Data;
using Microsoft.AspNetCore.Authorization;

namespace Avangardum.AsposeTestTask.Authorization;

public class IsPostAuthorHandler : AuthorizationHandler<IsPostAuthorRequirement, Post>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        IsPostAuthorRequirement requirement, Post resource)
    {
        if (context.User.Identity?.Name == resource.AuthorName) context.Succeed(requirement);
        return Task.CompletedTask;
    }
}