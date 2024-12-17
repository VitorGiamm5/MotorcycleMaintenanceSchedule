using Microsoft.OpenApi.Models;
using MotorcycleMaintenanceSchedule.Api.Converters;
using MotorcycleMaintenanceSchedule.Api.Swagger;
using MotorcycleMaintenanceSchedule.Application;
using MotorcycleMaintenanceSchedule.Infrastructure.Database.Services;
using NLog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile("nlog.config");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.DefaultBufferSize = 4096;

    options.JsonSerializerOptions.Converters.Add(new TrimStringJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("s"));
});

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"Maintence schedule motorcycle API - {builder.Environment.EnvironmentName}",
        Version = "v1",
        Contact = new OpenApiContact() { Name = ": Send Email", Email = "vitorgiammella@gmail.com" },
        Description = $"<b>ENVIRONMENT:<b/> {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}"
    });
    c.CustomSchemaIds(type => type.ToString());
    c.OperationFilter<DefaultValuesOperation>();
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

EnvironmentVariablesExtensions.AddEnvironmentVariables(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    int kestrelPort = builder.Configuration.GetValue<int>("Kestrel:Port", 5000);
    options.ListenAnyIP(kestrelPort);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .WithMethods("GET", "POST", "PUT", "DELETE")
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    ExecutePendingMigration.Execute(builder.Services);
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

await app.RunAsync();
