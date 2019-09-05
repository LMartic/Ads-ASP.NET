using Ads.API.Extensions;
using Ads.Application.Commands;
using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;
using Ads.DataAccess.EfDataAccess;
using Ads.Infrastructure.Auth;
using Ads.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ads.API
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
            services.AddCors(options =>
            {

                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });


            services.AddDbContext<AdsContext>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<ICreateAdCommand, EfCreateAdCommand>();
            services.AddTransient<IGetCategoriesCommand, EfGetCategoriesCommand>();
            services.AddTransient<IGetAdsCommand, EfGetAdsCommands>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IDeleteAdCommand, EfDeleteAdCommand>();
            services.AddTransient<IEditAdCommand, EfEditAdCommand>();
            services.AddTransient<ICreateRemoveFollowerCommand, EfCreateRemoveFollowerCommand>();
            services.AddTransient<IGetFollowersCommand, EfGetFollowersCommand>();
            services.AddTransient<ICreateOfferCommand, EfCreateOfferCommand>();
            services.AddTransient<IDeleteOfferCommand, EfDeleteOfferCommand>();
            services.AddTransient<IEditOfferCommand, EfEditOfferCommand>();
            services.AddTransient<IGetAdOffersCommand, EfGetAdOffersCommand>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<IEditCommentCommand, EfEditCommentCommand>();
            services.AddTransient<IGetCommentCommand, EfGetCommentCommand>();
            services.AddTransient<IGetAdCommentsCommand, EfGetAdCommentsCommand>();

            var section = Configuration.GetSection("Email");

            var sender =
                new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);

            services.AddSingleton<IEmailSender>(sender);



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Ads API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",

                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityRequirement(security);

            });
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 4;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<AdsContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };

                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                ///app.UseHsts();
            }







            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<AdsContext>().Database.Migrate();
            }
        }
    }
}
