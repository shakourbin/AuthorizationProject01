using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class UserClaimsService
{
    private readonly ApplicationDbContext _dbContext;

    public UserClaimsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Claim>> GetUserClaimsAsync(string userId)
    {
        return await _dbContext.UserClaims
            .Where(c => c.UserId == userId)
            .Select(c => new Claim(c.ClaimType, c.ClaimValue))
            .ToListAsync();
    }
}
