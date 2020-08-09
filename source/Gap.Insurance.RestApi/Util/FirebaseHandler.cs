using Celerik.NetCore.Services;
using Gap.Insurance.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gap.Insurance.RestApi
{
    public static class FirebaseHandler
    {
        public static void AddFirebaseAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var firebase = config.ReadObject<FirebaseConfig>("Firebase");
            var authority = string.Format(firebase.AuthorityUrl, firebase.ProjectId);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authority,
                        ValidateAudience = true,
                        ValidAudience = firebase.ProjectId,
                        ValidateLifetime = true
                    };
                });
        }
    }
}
