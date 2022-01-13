using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace Demo_WebApplication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Private midleware that works in SPA (Single Page Application)
        /// </summary>
        /// <param name="app"></param>
        public static void UseSpaAzureAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    await context.ChallengeAsync();
                }
                else
                {
                    await next();
                }
            });

        }
    }
}
