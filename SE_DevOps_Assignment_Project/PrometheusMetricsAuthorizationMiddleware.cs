using Prometheus;

public class PrometheusMetricsAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public PrometheusMetricsAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/metrics"))
        {
            if (context.User.Identity.IsAuthenticated && context.User.IsInRole("Admin"))
            {
                var response = context.Response;
                response.ContentType = "text/plain; version=0.0.4; charset=utf-8";
                await Metrics.DefaultRegistry.CollectAndExportAsTextAsync(response.Body, context.RequestAborted);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Redirect("/Error/401");
                return;
            }
        }
        else
        {
            await _next(context);
        }
    }
}
