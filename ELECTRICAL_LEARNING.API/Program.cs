
using ElectricalLearning.Api.Middleware;
using ElectricalLearning.Repositories;
using ElectricalLearning.Repositories.Abstraction;
using ElectricalLearning.Services.Implementations;
using ElectricalLearning.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ElectricalLearning.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            //JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var config = builder.Configuration.GetSection("JwtSettings");
                    var key = Encoding.UTF8.GetBytes(config["Secret"]);

                    options.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Issuer"],
                ValidAudience = config["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                });


            builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IJwtService, JwtService>();


            builder.Services.AddTransient<GlobalExceptionHandling>();

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandling>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();///////////

            app.UseAuthorization();


            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("loi");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
