using System.Collections.Generic;
using System.Linq;
using Celerik.NetCore.Services;
using Gap.Insurance.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gap.Insurance.RestApi
{
    public static class SwaggerHandler
    {
        public static void AddSwaggerUI(this IServiceCollection services, IConfiguration config)
        {
            var swagger = config.ReadObject<SwaggerConfig>("Swagger");

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(swagger.ApiVersion, new OpenApiInfo
                {
                    Title = swagger.ApiName,
                    Version = swagger.ApiVersion
                });

                setup.OperationFilter<SwaggerAuthorizationFilter>();

                setup.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "Token only without Bearer prefix",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app, IConfiguration config)
        {
            var swagger = config.ReadObject<SwaggerConfig>("Swagger");

            app.UseSwagger(setupAction =>
            {
                setupAction.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> {
                        new OpenApiServer {
                            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}"
                        }
                    };
                });
            });

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(swagger.ApiEndpoint, swagger.ApiName);
                setupAction.RoutePrefix = string.Empty;
            });
        }
    }

    public class SwaggerAuthorizationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodInfo = context.MethodInfo;
            var declaringAttrs = methodInfo.DeclaringType.GetCustomAttributes(true);
            var methodAttrs = methodInfo.GetCustomAttributes(true);

            var isAuthorized = (declaringAttrs.OfType<AuthorizeAttribute>().Any()
                && !declaringAttrs.OfType<AllowAnonymousAttribute>().Any())
                || (methodAttrs.OfType<AuthorizeAttribute>().Any()
                && !methodAttrs.OfType<AllowAnonymousAttribute>().Any());

            if (!isAuthorized)
                return;

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement { [jwtbearerScheme] = new string []{} }
            };
        }
    }
}
