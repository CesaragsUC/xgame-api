namespace mvc.Configuration
{
    public static class Ambiente
    {
        public static IWebHostEnvironment ConfigureAppSettings(this IWebHostEnvironment hostEnvironment)
        {

            var configManager = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                Console.WriteLine("IsDevelopment");
            }
            return hostEnvironment;
        }

    }
}
