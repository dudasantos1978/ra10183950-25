using Api.Authorization;
using Api.Filters;
using Api.Services.Auth;
using BuildingBlocks.Application.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
            options.Filters.Add<ResultFilter>();
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });
        services.AddFluentValidationClientsideAdapters();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProblemDetails();

        services.AddScoped<IAuthService, AuthService>();

        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypeEnum.Role.Type
                };
            });

        // [Authorize] → autenticado + membership no tenant (ou SuperAdmin). Sem AuthPolicies/Policy names.
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new OrganizationAccessRequirement())
                .Build();
        });
        services.AddScoped<IAuthorizationHandler, OrganizationAccessAuthorizationHandler>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        BuildingBlocks.Application.DependencyInjection.AddApplication(services);
        BuildingBlocks.Infrastructure.DependencyInjection.AddInfrastructure(services, configuration);

        Identity.Application.DependencyInjection.AddApplication(services);
        Identity.Infrastructure.DependencyInjection.AddInfrastructure(services, configuration);

        Tenant.Application.DependencyInjection.AddApplication(services);
        Tenant.Infrastructure.DependencyInjection.AddInfrastructure(services, configuration);

        Tickets.Application.DependencyInjection.AddApplication(services);
        Tickets.Infrastructure.DependencyInjection.AddInfrastructure(services, configuration);

        return services;
    }
}