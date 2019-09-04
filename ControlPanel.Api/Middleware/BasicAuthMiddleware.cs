using ControlPanel.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Api.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        IOptions<AppSettings> _appSettings;

        public BasicAuthMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the encoded username and password
                var encodedUsernamePasswordCombination = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                // Decode from Base64 to string
                var decodedUsernamePasswordCombination = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePasswordCombination));
                // Split username and password
                var username = decodedUsernamePasswordCombination.Split(':', 2)[0];
                var password = decodedUsernamePasswordCombination.Split(':', 2)[1];
                // Check if login is correct
                if (username == _appSettings.Value.BasicAuthUsername && password == _appSettings.Value.BasicAuthPassword)
                {
                    await _next.Invoke(context);
                    return;
                }
            }
            // Return authentication type (causes browser to show login dialog)
            context.Response.Headers["WWW-Authenticate"] = "Basic";

            // Return unauthorized
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
