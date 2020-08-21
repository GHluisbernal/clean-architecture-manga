namespace WebApi.Modules.Common
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    ///     CORS Extensions.
    /// </summary>
    public static class CorsExtensions
    {
        const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        ///     Add CORS.
        /// </summary>
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        // Not a permanent solution, but just trying to isolate the problem
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });


            return services;
        }

        /// <summary>
        ///     Use CORS.
        /// </summary>
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(MyAllowSpecificOrigins);

            return app;
        }
    }
}
