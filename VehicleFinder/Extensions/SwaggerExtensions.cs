using Microsoft.OpenApi.Models;

namespace VehicleFinder.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddVehicleFinderSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var enabled = configuration.GetValue<bool>("Swagger:Enabled");

            if (!enabled)
                return services;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VehicleFinder API",
                    Version = "v1",
                    Description = "API para la búsqueda de vehículos disponibles"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: 'Bearer {token}'"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

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

            return services;
        }

        public static void UseInmoSwagger(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment env)
        {
            var enabled = configuration.GetValue<bool>("Swagger:Enabled");

            if (env.IsDevelopment() || enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "swagger";
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleFinder API v1");
                });
            }
        }
    }
}
