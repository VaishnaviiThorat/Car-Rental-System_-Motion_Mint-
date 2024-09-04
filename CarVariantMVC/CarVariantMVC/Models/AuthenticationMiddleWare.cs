namespace CarVariantMVC.Models
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();

            // List of paths that do not require authentication
            var allowedPaths = new[] {"/", "/account/login", "/account/register", "/home/about", "/home/contact" };


            if (!allowedPaths.Contains(path))
            {
                var username = context.Request.Cookies["Username"];
                var password = context.Request.Cookies["Password"];

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    // Redirect to the login page if the user is not authenticated
                    context.Response.Redirect("/account/login");
                    return;
                }
            }

            await _next(context);
        }
    }


}
