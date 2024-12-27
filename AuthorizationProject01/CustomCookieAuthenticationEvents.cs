using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly ILogger<CustomCookieAuthenticationEvents> _logger;
    private readonly IDataProtectionProvider _dataProtectionProvider;

    public CustomCookieAuthenticationEvents(ILogger<CustomCookieAuthenticationEvents> logger, IDataProtectionProvider dataProtectionProvider)
    {
        _logger = logger;
        _dataProtectionProvider = dataProtectionProvider;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        try
        {
            //Logging cookie value 
            // Retrieve the cookie value
            var cookieData = context.Request.Cookies[context.Options.Cookie.Name];

            if (!string.IsNullOrEmpty(cookieData))
            {
                // Decrypt the cookie
                var protector = _dataProtectionProvider.CreateProtector(
                    "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                    "Cookies", "v2");
                var decryptedCookie = protector.Unprotect(cookieData);

                // Log the decrypted cookie content
                _logger.LogInformation("Decrypted Cookie Content: {CookieContent}", decryptedCookie);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to log decrypted cookie content");
        }

        await base.ValidatePrincipal(context);
    }
}
