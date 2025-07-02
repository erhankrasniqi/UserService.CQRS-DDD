
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using System.Reflection;
using UserService.Infrastructure.Contracts;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Messaging;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WitsDbContext>(options => options.UseNpgsql(connectionString));


            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotifier, NoOpNotifier>();

            AutoRegisterRepositories(services);

            return services;
        }

      

        private static void AutoRegisterRepositories(IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            IEnumerable<Type> repositoryInterfaces = types.Where(t => t.IsInterface && t.Name.ToLower().Trim().EndsWith("repository"));

            if (repositoryInterfaces.Any())
            {
                foreach (Type repositoryInterface in repositoryInterfaces)
                {
                    Type implementationType = types.FirstOrDefault(t =>
                        t.IsClass
                        && !t.IsAbstract
                        && !t.IsInterface
                        && repositoryInterface.IsAssignableFrom(t));

                    if (implementationType != null)
                    {
                        services.AddScoped(repositoryInterface, implementationType);
                    }
                }
            }
        }
    }
}