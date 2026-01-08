namespace MovieRental.Error_Handling
{
    public class GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            }
        }
    }
}
