namespace EComm.API
{
    public static class Startup
    {
        public static void RegisterServices(WebApplication app)
        {
            app.UseAuthorization();
        }
    }
}
