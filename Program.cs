
using thoeun_coffee.Data;
using thoeun_coffee.Services;

namespace thoeun_coffee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectoinString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectoinString, ServerVersion.AutoDetect(connectoinString));
            });

            builder.Services.AddControllers();
            // Register JwtService for Dependency Injection
            builder.Services.AddScoped<JwtService>();  // Or AddSingleton<JwtService>() if it should be a singleton

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
