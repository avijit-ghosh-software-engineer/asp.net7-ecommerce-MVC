using Microsoft.AspNetCore.Builder;

namespace BulkyStore_Extensions
{
    public static class BaseExtension
    {
        public static void ServiceInjects(this WebApplicationBuilder builder)
        {
            builder.DataBaseInject();
            builder.StripePaymentInject();
            builder.CookieInject();
            builder.AuthenticationInject();
            builder.SessionInject();
        }
    }
}
