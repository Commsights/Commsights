using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Commsights.Service.Mail;
using Microsoft.AspNetCore.Http.Features;

namespace Commsights.MVC
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
            services.AddControllers(options => options.EnableEndpointRouting = false)
                     .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddDbContext<CommsightsContext>();
            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<IMembershipAccessHistoryRepository, MembershipAccessHistoryRepository>();
            services.AddTransient<IMembershipPermissionRepository, MembershipPermissionRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductPropertyRepository, ProductPropertyRepository>();
            services.AddTransient<IProductSearchRepository, ProductSearchRepository>();
            services.AddTransient<IProductSearchPropertyRepository, ProductSearchPropertyRepository>();
            services.AddTransient<IConfigRepository, ConfigRepository>();
            services.AddTransient<IDashbroadRepository, DashbroadRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<ICodeDataRepository, CodeDataRepository>();
            services.AddTransient<IProductPermissionRepository, ProductPermissionRepository>();
            services.AddTransient<IEmailStorageRepository, EmailStorageRepository>();
            services.AddTransient<IEmailStoragePropertyRepository, EmailStoragePropertyRepository>();
            services.AddTransient<IReportMonthlyRepository, ReportMonthlyRepository>();
            services.AddTransient<IReportMonthlyPropertyRepository, ReportMonthlyPropertyRepository>();
            services.AddTransient<IBaiVietUploadCountRepository, BaiVietUploadCountRepository>();
            services.AddTransient<IBaiVietUploadRepository, BaiVietUploadRepository>();
            services.AddTransient<IMailService, MailService>();
            services.AddControllersWithViews();

            services.AddKendo();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
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
                    name: "default",
                    pattern: "{controller=Membership}/{action=EmployeeInfo}/{id?}");
            });
        }
    }
}
