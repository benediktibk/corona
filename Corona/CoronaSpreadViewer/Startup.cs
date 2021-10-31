using Backend;
using NLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoronaSpreadViewer {
    public class Startup {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        
        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            try {
            var settings = new Settings(_configuration);
            DependencyInjectionRegistry.ConfigureServices(services, settings);
            services.AddMvc();
            services.AddControllers();
            services.AddRazorPages();
            } catch(System.Exception e) {
                _logger.Error(e, "unable to execute startup");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
