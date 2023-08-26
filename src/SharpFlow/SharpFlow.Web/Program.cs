
using VorNet.SharpFlow.Engine.Data;
using VorNet.SharpFlow.Engine.Execution;
using VorNet.SharpFlow.Engine.Executor;
using VorNet.SharpFlow.Engine.Serilaizer;

namespace SharpFlow.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IBufferedLogger, BufferedLogger>();
            builder.Services.AddSingleton<IGraphExecutor, GraphExecutor>();
            builder.Services.AddSingleton<IGraphSerializer, GraphSerializer>();
            builder.Services.AddSingleton<IMetadataGenerator, MetadataGenerator>();
            builder.Services.AddSingleton<IGraphDataAccess, SqlLiteGraphDataAccess>();
            builder.Services.AddSingleton((ctx) => new Microsoft.Data.Sqlite.SqliteConnection("Data Source=sharpflow_graphs.db"));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}