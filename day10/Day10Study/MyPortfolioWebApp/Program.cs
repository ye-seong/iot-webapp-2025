using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Runtime;

namespace MyPortfolioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // DB���� �ʱ�ȭ ����
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
                builder.Configuration.GetConnectionString("SmartHomeConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SmartHomeConnection"))
            ));

            // ASP.NET Core Identity ����
            // ������ IdentityUser -> CustomUser �� ����
            builder.Services.AddIdentity<CustomUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // �н����� ��å
            // ���� ��. �ִ� 6�ڸ� �̻�, Ư������ 1�� ����, �����ҹ��� ����
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // �̷� ��ȣ ����ȭ�� �ǵ��� ������ ��
                options.Password.RequiredLength = 4; // �ּұ��� 4�ڸ�
                options.Password.RequireNonAlphanumeric = false; // Ư������ ������
                options.Password.RequireUppercase = false; // �빮�� ������
                options.Password.RequireLowercase = false; // �ҹ��� ������
                options.Password.RequireDigit = false; // �����ʼ� ����
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();  // ASP.NET Core Identity ����
            app.UseAuthorization();   // ����

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
