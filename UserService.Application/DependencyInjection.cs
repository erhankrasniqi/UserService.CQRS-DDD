using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Features.Users.Commands;

namespace UserService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            return services;
        }
    }
}
