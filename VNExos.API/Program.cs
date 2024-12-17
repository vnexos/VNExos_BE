using VNExos.API.Extensions;
using VNExos.Application.Extensions;
using VNExos.Infrastructure.Extensions;

namespace VNExos.API;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add extensions
        builder.AddPresentation();
        builder.AddSwagger();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
};
