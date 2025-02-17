using System.Globalization;

namespace PeopleDirectory.API.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Headers["Accept-Language"].ToString();
            var culture = !string.IsNullOrEmpty(cultureQuery) ? cultureQuery.Split(',')[0] : "en";

            var supportedCultures = new[] { "en", "ka" };
            if (!supportedCultures.Contains(culture))
                culture = "en";

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            await _next(context);
        }
    }
}
