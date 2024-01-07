using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rwwo.min_web_api.rbs.Models;

namespace rwwo.min_web_api.rbs.Services
{
    public static class InfraModule
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>();

            builder.Services.AddScoped<SecurityServices>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("read", policy => policy.RequireRole("user"));
        }
    }
}
