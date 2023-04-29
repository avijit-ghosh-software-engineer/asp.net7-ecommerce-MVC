using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BulkyStore_Utility.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AuthenticationInject(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication().AddFacebook(option => {
                option.AppId = "193813826680436";
                option.AppSecret = "8fc42ae3f4f2a4986143461d4e2da919";
            });
            builder.Services.AddAuthentication().AddMicrosoftAccount(option =>
            {
                option.ClientId = "ec4d380d-d631-465d-b473-1e26ee706331";
                option.ClientSecret = "qMW8Q~LlEEZST~SDxDgcEVx_45LJQF2cQ_rEKcSQ";
            });
        }
    }
}
