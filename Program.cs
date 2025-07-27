
using MadiffTechnicalAssignment.Rules;
using MadiffTechnicalAssignment.Services;

namespace MadiffTechnicalAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<ICardActionRule, PrepaidCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, DebitCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, CreditCardActionRule>();

            builder.Services.AddSingleton<ICardActionRule, OrderedCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, InactiveCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, ActiveCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, RestrictedCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, BlockedCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, ExpiredCardActionRule>();
            builder.Services.AddSingleton<ICardActionRule, ClosedCardActionRule>();

            builder.Services.AddSingleton<ICardActionsRulesEngine, CardActionsRulesEngine>();

            builder.Services.AddScoped<ICardService, CardService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
