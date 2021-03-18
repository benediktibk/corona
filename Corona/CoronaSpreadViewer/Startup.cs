using Backend.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap;

namespace CoronaSpreadViewer {
    public class Startup {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddRazorPages();
            var container = new Container();
            container.Configure(config => {
                config.AddRegistry(new DependencyInjectionRegistry(_configuration));
                config.Populate(services);
            });
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
