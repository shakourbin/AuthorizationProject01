using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Security.AccessControl;

public class PolicyManager
{
    private readonly IServiceCollection _services;

    public PolicyManager(IServiceCollection services)
    {
        _services = services;
    }

    public void RegisterPolicies()
    {
        var handlerTypes = this.GetHandlers();

        foreach (var handlerType in handlerTypes)
        {
            _services.AddSingleton(typeof(IAuthorizationHandler), handlerType);
        }
    }

    public List<Type> GetHandlers()
    {
        var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
           .Where(t => typeof(IAuthorizationHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
           .ToList();

        return handlerTypes;
    }

    public List<Type> GetRequierments()
    {
        var requierments= Assembly.GetExecutingAssembly().GetTypes()
           .Where(t => typeof(IAuthorizationRequirement).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
           .ToList();

        return requierments;
    }
}
