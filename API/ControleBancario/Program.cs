namespace ControleBancario
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            UpdateDbIfExists(host);

            //É instruido pela própria microsoft que isso é uma má pratica, e que à mais maleficios utilizando desta forma que beneficios!
            //CreateDbIfNotExists(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void UpdateDbIfExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            db.Database.Migrate();
        }

        //private static void CreateDbIfNotExists(IHost host)
        //{
        //    using var scope = host.Services.CreateScope();
        //    var services = scope.ServiceProvider;

        //    try
        //    {
        //        var context = services.GetRequiredService<ApplicationContext>();
        //        context.Database.EnsureCreated();
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        logger.LogError(ex, "An error occurred creating the DB.");
        //    }
        //}
    }
}