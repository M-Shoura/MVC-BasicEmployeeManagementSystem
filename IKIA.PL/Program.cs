using IKIA.BLL.Common.Services.Attachments;
using IKIA.BLL.Common.Services.Emails;
using IKIA.BLL.Services.Departments;
using IKIA.BLL.Services.Employees;
using IKIA.DAL.Models.Identity;
using IKIA.DAL.Presistence.Data;
using IKIA.DAL.Presistence.UnitOfWork;
using IKIA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKIA.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();


			builder.Services.AddDbContext<ApplicationDbcontext>(optionsBuilder =>
			{
				optionsBuilder
				.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();
			builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
			builder.Services.AddTransient<IAttachmentService, AttachmentService>();
			
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
			{
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 5;       

				options.User.RequireUniqueEmail = true;

				options.Lockout.MaxFailedAccessAttempts = 3;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

			}).AddEntityFrameworkStores<ApplicationDbcontext>()
			  .AddDefaultTokenProviders();        

			builder.Services.ConfigureApplicationCookie((option) => 
			{
				option.LoginPath = "/Account/SignIn";
				option.AccessDeniedPath = "/Home/Error";
				option.ExpireTimeSpan = TimeSpan.FromDays(1);
			});

			builder.Services.AddTransient<IEmailService , EmailService>();

			var app = builder.Build();
			
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
