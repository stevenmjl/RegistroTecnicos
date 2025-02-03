using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Obtenermos el ConStr para agregarlo al contexto
            var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
            
            // Agregamos el contexto al builder con el ConStr
            builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

            // Inyectamos todos los servicios
            builder.Services.AddScoped<TecnicosService>();
            builder.Services.AddScoped<ClientesService>();
            builder.Services.AddScoped<CiudadesService>();
            builder.Services.AddScoped<TicketsService>();

            builder.Services.AddBlazorBootstrap();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
