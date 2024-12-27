using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
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
    public async void InsertUserClaims(string userId)
    {
        //var exists = _dbContext.Database.ExecuteSqlRaw($"select top 1 * FROM #tempdb_{userId}");

        // if (exists == 0)
        // {
        var trimedUserId = userId.Replace("-", "").Trim();

        string query = $"CREATE TABLE #TempUserClaims_{trimedUserId} (" + Environment.NewLine;
        query += $"  Id INT PRIMARY KEY," + Environment.NewLine;
        query += "UserId NVARCHAR(450)," + Environment.NewLine;
        query += "    ClaimType NVARCHAR(max)," + Environment.NewLine;
        query += "ClaimValue NVARCHAR(max));" + Environment.NewLine;
        //query += "Go " + Environment.NewLine;
        query += $"INSERT INTO #TempUserClaims_{trimedUserId} select * from AspNetUserClaims where UserId = '{userId}'" + Environment.NewLine;

        _dbContext.UserClaims
      .FromSqlRaw(query); // Executes the SQL query
      
        //}
    }

}
