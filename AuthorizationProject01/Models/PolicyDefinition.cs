public class PolicyDefinition
{
    public int Id { get; set; }
    public string PolicyName { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
    public string Role { get; set; } // Optional role for the policy
}
