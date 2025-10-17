using CardActionsService.Domain.Interfaces;
using System.Reflection;

namespace CardActionsService.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddActionRules(this IServiceCollection services)
        {
            Assembly domainAssembly = typeof(IActionRule).Assembly;
            var ruleTypes = domainAssembly.GetTypes()
                .Where(t => t.IsClass 
                    && !t.IsAbstract 
                    && typeof(IActionRule).IsAssignableFrom(t));

            foreach (var ruleType in ruleTypes)
            {
                services.AddScoped(typeof(IActionRule), ruleType);
            }

            return services;
        }
    }
}
