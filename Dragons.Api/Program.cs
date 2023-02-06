using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleSystemsManagement;
using Dapper;
using Dragons.Api.Authentication;
using Dragons.Api.DataAccess;
using Dragons.Api.Models;
using Dragons.Api.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container
{
    var services = builder.Services;

    // Add controllers
    services.AddControllers();

    // Get the AWS profile information from configuration providers
    AWSOptions awsOptions = builder.Configuration.GetAWSOptions();

    // Configure AWS service clients to use these credentials
    services.AddDefaultAWSOptions(awsOptions);

    // These AWS service clients will be singleton by default
    services.AddAWSService<IAmazonSimpleSystemsManagement>();

    // Add http context accessor access to classes
    services.AddHttpContextAccessor();

    // Match column names with underscores in them in dapper
    DefaultTypeMap.MatchNamesWithUnderscores = true;

    // Register authentication handler
    services.AddAuthentication("DragonsAuthentication")
        .AddScheme<AuthenticationSchemeOptions, DragonsAuthenticationHandler>("DragonsAuthentication", null);

    // Register data services
    services.AddScoped<IDragonRepository, DragonRepository>();
    services.AddScoped<IUserRepository, UserRepository>();

    // Register automatic fluent validation
    services.AddFluentValidationAutoValidation();

    // Register validators
    services.AddScoped<IValidator<Dragon>, DragonValidator>();
}

var app = builder.Build();

// Configure application
{
    app.UseHttpsRedirection();

    // Enable CORS
    app.UseCors(options =>
    {
        options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    // Enable authentication and authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Enable controllers
    app.MapControllers();
}

app.Run();

public partial class Program { }