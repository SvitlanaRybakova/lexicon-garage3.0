using lexicon_garage3.Persistance;
using lexicon_garage3.Persistance.Data;

namespace lexicon_garage3.Web.Extention
{
    public static class ApplicationBuilderExtentions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                try
                {
                    await SeedData.Init(context, services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during seeding: {ex.Message}");
                    throw;
                }
            }
            return app;
        }
    }
}
