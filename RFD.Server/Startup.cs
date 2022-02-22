using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Bussiness.EntityFramework.Services.Concrete;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.DTO;
using RFD.Server.Infrastructer.Extensions;
using RFD.Server.Infrastructer.Hubs;
using RFD.Server.Infrastructer.Middleware;
using System.Reflection;

namespace RFD.Server
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
            services.AddDbContext<RFDContext>(options => options.UseSqlite(Configuration.GetConnectionString("RFDConnStr"), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)), ServiceLifetime.Transient);
            //    services.AddScoped<IComponentService, ComponentManager>();
            //    services.AddScoped<IComponentService, ComponentManager>();
            services.AddTransient<DbContext>();
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddSingleton<IRFDStarterExtension, RFDStarterExtension>();
            services.AddSignalR(option =>
            {
                option.MaximumReceiveMessageSize = 20 * 1024 * 1024;
            });
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddMvcCore(options =>
            {

                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                //   options.OutputFormatters.Add(new jsonoutputformatter());
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
                .AddXmlSerializerFormatters();
            //.AddXmlDataContractSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RFD.Server", Version = "v1" });
            });
            services.AddCors(o => o.AddPolicy("RFD",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination");
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RFD.Server v1"));
            }

 app.UseTracker();
            app.UseStaticFiles();
           
            app.UseMiddleware<ErrorHandlerMiddleware>();
            //app.UseMiddleware<ApiKeyMiddleware>();

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("RFD");
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                endpoints.MapHub<RFDHub>("/hub");
            });
        }
    }
}
