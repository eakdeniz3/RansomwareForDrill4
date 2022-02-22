using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Bussiness.EntityFramework.Services.Concrete;
using RFD.DataAccess.EntityFramework;
using RFD.WebUI.Infrastructer.Auths;
using RFD.WebUI.Infrastructer.Extensions;
using RFD.WebUI.Infrastructer.Helpers;
using RFD.WebUI.Infrastructer.Hubs;
using RFD.WebUI.Infrastructer.Middlewares;
using RFD.WebUI.Infrastructer.StateManagement;
using RFD.WebUI.Services;
using System;
using System.Reflection;

namespace RFD.WebUI
{
    public class Startup
    {
        public string serverUrl;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            serverUrl = Configuration.GetValue<string>("ServerUrl");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<IRFDHelper, RFDHelper>();
            
           // services.AddDbContext<RFDContext>(options => options.UseSqlite(Configuration.GetConnectionString("RFDConnStr"), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)), ServiceLifetime.Transient);
            //    services.AddScoped<IComponentService, ComponentManager>();
            //    services.AddScoped<IComponentService, ComponentManager>();
            //services.AddTransient<DbContext>();
            //services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddSingleton<IRFDStarterExtension, RFDStarterExtension>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            //  services.AddSingleton<IInsiderStarterExtension, InsiderStarterExtension>();
            services.AddSignalR(option => {
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
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RFD.Server", Version = "v1" });
            //});

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            //});
            //services.AddAuthentication(
            //    CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();
            //services.AddHttpContextAccessor();
            //services.AddScoped<HttpContextAccessor>();
           // services.AddSingleton<ISummaryHttpHelper, SummaryHttpHelper>();
            services.AddSingleton<IInsiderHttpHelper, InsiderHttpHelper>();
            services.AddSingleton<ISummaryHttpHelper, SummaryHttpHelper>();
            services.AddSingleton<IRFDHelper, RFDHelper>();
           // services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddAuthorizationCore();
            services.AddHttpClient("RFDClient", client =>
            {
                client.BaseAddress = new Uri($"{serverUrl}/api");
                // client.BaseAddress = new Uri("https://localhost:44328/api");
                client.DefaultRequestHeaders.Add("ApiKey", "94298e93-398d-4e08-a610-e042f489cb9c");
            }
            );
            services.AddSingleton<HubConnection>(sp =>
            {
                // var navigationManager = sp.GetService<NavigationManager>();

                return new HubConnectionBuilder().WithAutomaticReconnect().WithUrl($"{serverUrl}/hub", option => { option.Headers.Add("ApiKey", "94298e93-398d-4e08-a610-e042f489cb9c"); }).Build();
            });

            services.AddScoped<RFDStateManagement>();
            services.AddCors(o => o.AddPolicy("RFD",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //app.UseMiddleware<RouteControlMiddleware>();
            //app.UseTracker();
            app.UseStaticFiles();
            app.UseMiddleware<ErrorHandlerMiddleware>();
           // app.UseMiddleware<ApiKeyMiddleware>();

            // app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseRouting();
            app.UseCors("RFD");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
               // endpoints.MapControllers();
                //endpoints.MapHub<RFDHub>("/hub");
              
            });
        }
    }
}
