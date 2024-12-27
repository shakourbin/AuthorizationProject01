using Microsoft.AspNetCore.Authorization;
using static System.Formats.Asn1.AsnWriter;

public class AgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; }

    public AgeRequirement(int minimumAge)
    {
        MinimumAge = minimumAge;
    }
}

public class AgePolicyHandler : AuthorizationHandler<AgeRequirement>
{
    public AgePolicyHandler() { }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
    {

        //UserClaimsService claimService = new UserClaimsService(dbContext);
        //claimService.GetUserClaimsAsync(userId);

        var ageClaim = context.User.FindFirst("Age");
        if (ageClaim != null && int.TryParse(ageClaim.Value, out var age) && age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}