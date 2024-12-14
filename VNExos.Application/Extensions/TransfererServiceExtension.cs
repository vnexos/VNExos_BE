using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace VNExos.Application.Extensions;

public static class TransfererServiceExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(TransfererServiceExtension).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();
    }
}
