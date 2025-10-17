
using CardActionsService.Api.ExceptionHandlers;
using CardActionsService.Api.Extensions;
using CardActionsService.Application.Interfaces;
using CardActionsService.Application.Services;
using CardActionsService.Domain.Interfaces;
using CardActionsService.Infrastructure.Services;

namespace CardActionsService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddHealthChecks();

            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<IAllowedActionsService, AllowedActionsService>();
            builder.Services.AddScoped<ICardActionRulesService, CardActionRulesService>();
            builder.Services.AddActionRules();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}
