using System.Reflection;

using Chinook.Infrastructure.Contracts;


namespace Chinook.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceImplementations(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<TypeInfo> classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in classTypes)
                {

                    if (type.GetInterface(typeof(IBase).Name) != null)
                    {
                        var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                        foreach (var interfaceType in interfaces.Where(_ => _ != typeof(IBase)))
                        {
                            if (services.Any(_ => _.ServiceType == interfaceType))
                            {
                                services.Remove(services.First(_ => _.ServiceType == interfaceType));
                            }

                            services.AddTransient(interfaceType.AsType(), type.AsType());
                        }
                    }
                }
            }

            return services;
        }
    }
}
