using Microsoft.AspNetCore.Http;

namespace SNS.ECommerce.Infrastructure.Services
{
    public class ServiceLocator
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Register(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static T Resolve<T>()
        {
            return (T)_httpContextAccessor.HttpContext?.RequestServices.GetService(typeof(T));
        }
    }
}
