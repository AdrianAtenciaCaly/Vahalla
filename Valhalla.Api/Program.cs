using Valhalla.Application.Interfaces;
using Valhalla.Infrastructure.Repositories;
using Valhalla.Infrastructure.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<
    IVikingRepository,
    VikingRepository>();

builder.Services.AddScoped<
    IVikingClassificationService,
    VikingClassificationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();