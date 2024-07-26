using PtnDemoProject.DAL.Extensions;
using PtnDemoProjectAPI.BLL.Extensions;
using PtnDemoProjectAPI.Presentation.Extensions;

namespace PtnDemoProjectAPI.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.
                AddDbContextConfigurations(builder.Configuration).
                AddMongoDbConfigurations(builder.Configuration).
                AddRepositories().
                SeedDatas().
                AddJwtConfigurations(builder.Configuration).
                AddOtherBLLExtensions().
                AddServices().
                AddFluentValidation().
                AddSwaggerGenConfiguration().
                AddCorsConfiguration();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}