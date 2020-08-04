using Http.Consumer;
using JLocalizer.Web.ApiResource;
using JLocalizer.Web.Db;
using JLocalizer.Web.TestLoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JLocalizer.Web
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
            services.AddDbContext<EFLocalizationContext>(opt => opt.UseInMemoryDatabase("lang", databaseRoot: new Microsoft.EntityFrameworkCore.Storage.InMemoryDatabaseRoot()));

            //JLocalizer configuration 
            services.AddJLocalizer(x =>
            {
                x.DefaultCulture("en-US");
                x.AddLocalizedResource(typeof(TestLocResource));
                x.AddLocalizedResource(new ApiExternalLocalization(new HttpConsumer()));
                x.AddLocalizerFactory<EFLocalizationContext, Db.DbLocalizationFactory>();
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Testing for database localization 
            app.Use(async (httpContext, next) =>
            {
                var context = httpContext.RequestServices.GetService<EFLocalizationContext>();
                context.Add(new Localization
                {
                    Lang = "en-US",
                    Data = new System.Collections.Generic.List<Data>
                    {
                        new Data{ Key = "Hello", Name ="Db en-Us" }
                    }
                });

                context.Add(new Localization
                {
                    Lang = "en-IN",
                    Data = new System.Collections.Generic.List<Data>
                    {
                        new Data{ Key = "Hello", Name ="Db en-IN" }
                    }
                });
                context.SaveChanges();
                await next();
            });

            app.UseMvc();
        }
    }
}
