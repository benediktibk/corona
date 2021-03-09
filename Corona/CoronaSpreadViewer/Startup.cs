using Backend.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap;

namespace CoronaSpreadViewer {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            var container = new Container();
            container.Configure(config => {
                config.AddRegistry(new DependencyInjectionRegistry());
                config.Populate(services);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            //NConfigurator.UsingFiles("Config\\Corona.config").SetAsSystemDefault();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
