using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using teste.aiko.Modelos;
using teste.aiko.Repository;

namespace teste.aiko
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Contexto>();
            services.AddTransient<IEquipamentoEstadoRepo, IEquipamentoEstadoRepo>();
            services.AddTransient<IEquipamentoRepo, IEquipamentoRepo>();
            services.AddTransient<IEquipamentoModeloRepo, IEquipamentoModeloRepo>();
            services.AddTransient<IEquipamentoEstadoHistoricoRepo, IEquipamentoEstadoHistoricoRepo>();
            services.AddTransient<IEquipamentoPosicoesHistoricoRepo, IEquipamentoPosicoesHistoricoRepo>();
            services.AddTransient<IEquipamentoModeloEstadoHoraRepo, IEquipamentoModeloEstadoHoraRepo>();

            services.AddControllersWithViews().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddEntityFrameworkNpgsql().AddDbContext<Contexto>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("AikoConection")));

            services.AddSwaggerGen(c =>
            {
                string fileXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string pathXml = Path.Combine(AppContext.BaseDirectory, fileXml);
                c.IncludeXmlComments(pathXml);
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.TESTE - Equipamentos  V1");
                c.RoutePrefix = string.Empty;
            });


        }
    }
}
