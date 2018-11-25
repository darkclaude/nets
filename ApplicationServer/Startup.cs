using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hangfire;
using Hangfire.Mongo;
using RestSharp;
namespace ApplicationServer
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
            services.AddHangfire(config =>
            {
                config.UseMongoStorage("mongodb://nets:nets123@ds117719.mlab.com:17719/heroku_x7jqwmcx", "heroku_x7jqwmcx");
               // config.UseActivator(new AutofacJobActivator(container));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            GlobalConfiguration.Configuration.UseMongoStorage("mongodb://nets:nets123@ds117719.mlab.com:17719/heroku_x7jqwmcx", "heroku_x7jqwmcx");
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate(() => richsurvey(false), "*/1 * * * *");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public void richsurvey(bool enable){
            if (enable)
            {
                var client = new RestClient("http://richsurvey.herokuapp.com");
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest("asp", Method.GET);
                request.AddQueryParameter("time", DateTime.Today.ToLongDateString());



                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                Console.WriteLine(content);
            }

        }
    }
}
