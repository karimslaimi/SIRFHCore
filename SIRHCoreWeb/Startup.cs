using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using SIRHCoreWeb.Data;
using Microsoft.AspNetCore.Http;
using Fido2NetLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Converters;


//using owin;

namespace SIRHCoreWeb
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                  .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddTokenProvider<Fifo2UserTwoFactorTokenProvider>("FIDO2");
            services.AddRazorPages();


            services.AddControllersWithViews();
            // Add Email Service
            services.AddTransient<IEmailSender, EmailService.EmailSender>(
                implementation => new EmailService.EmailSender(
                    this.Configuration["EmailSender:Host"],
                    this.Configuration.GetValue<int>("EmailSender:Port"),
                    this.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    this.Configuration["EmailSender:UserName"],
                    this.Configuration["EmailSender:Password"],
                    this.Configuration["EmailSender:SenderEmail"]));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Configure<Fido2Configuration>(Configuration.GetSection("fido2"));
            services.Configure<Fido2MdsConfiguration>(Configuration.GetSection("fido2mds"));
            services.AddScoped<Fido2Storage>();
            services.AddDistributedMemoryCache();

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());

            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
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
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
            CreateRoles(services).Wait();
            //  CreateRoles();
            //ap.MapSignalR();
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            //here in this line we are adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Responsable RH");
            var roleCheck2 = await RoleManager.RoleExistsAsync("Collaborateur");
            var roleCheck3 = await RoleManager.RoleExistsAsync("Administrateur");
            if (!roleCheck)
            {
                //here in this line we are creating admin role and seed it to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Responsable RH"));

            }
            if (!roleCheck2)
            {
                //here in this line we are creating admin role and seed it to the database

                roleResult = await RoleManager.CreateAsync(new IdentityRole("Collaborateur"));
            }
            if (!roleCheck3)
            {
                var role = new IdentityRole();
                role.Name = "Administrateur";
                await RoleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "sirh.sinorfi@gmail.com";

               string userPWD = "Sghjawtelesup1!";

                var chkUser = await UserManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = await UserManager.AddToRoleAsync(user, "Administrateur");
                }
                //here in this line we are creating admin role and seed it to the database

              //  roleResult = await RoleManager.CreateAsync(new IdentityRole("Administrateur"));
            }
            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
        }
















        /* private void CreateRoles()
         {
             ApplicationDbContext context = new ApplicationDbContext();
             var roleManager = new RoleManager<IdentityRole> (new RoleStore<IdentityRole>(context));
             if (!roleManager.RoleExistsAsync("Collaborateur"));
             {
                 var role = new Microsoft.AspNetCore.Identity.IdentityRole();
                 role.Name = "Collaborateur";
                 roleManager.CreateAsync(role);

             }
             if (!roleManager.RoleExistsAsync("Admin")) ;
             {
                 var role = new Microsoft.AspNetCore.Identity.IdentityRole();
                 role.Name = "Admin";
                 roleManager.CreateAsync(role);

             }

         }*/
    }
}
