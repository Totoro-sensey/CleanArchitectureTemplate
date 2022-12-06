using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using FluentValidation;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddBehaviors();

            return services;
        }

        private static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            return services;
        }
        
        private static IServiceCollection AddRequestProcessingBehaviors(this IServiceCollection services, Type serviceType)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(i => i.GetInterfaces()
                    .Any(j => j.IsGenericType
                              && j.GetGenericTypeDefinition() == serviceType));
            foreach (var type in types)
                services.AddTransient(typeof(IPipelineBehavior<,>), type);

            return services;
        }
    }
}
