using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BulkyStore_Utility.Extensions
{
    public static class StripePaymentExtension
    {
        public static void StripePaymentInject(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        }
    }
}
