
using EcommerceEjemploApi.Data;
using EcommerceEjemploApi.Interfaces;
using EcommerceEjemploApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceEjemploApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //esto es como importar dentro de la app el automapper y que pueda accedes a mapperar los assemblies
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //ADDSCOPE: This is a method provided by IServiceCollection that registers a service with a specific lifetime
            //scope.
            //In this case, AddScoped indicates that the service will be created once per request within the current HTTP
            //request scope.
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



            // **CORS configuration (point number 2):**
            //aqui simplemente queri especificar que se ppudiera llamar de cualquier sitio pero
            //nose como va asi que salto a mirarme un proyecto plantilla
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            //nos conectamos al la base de datsos dels ervidor configurado en el appsetting.json
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowReactApp");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
