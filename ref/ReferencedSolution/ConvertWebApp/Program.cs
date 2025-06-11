namespace ConvertWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // HttpClient ���
            builder.Services.AddHttpClient("MyWebClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8000"); // AI �⺻�ּ�
            });

            // CORS ��� (���� �׽�Ʈ��)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            var app = builder.Build();

            app.UseCors();
            // wwwroot/index.html ���� ���� ������ ������ �� �ֵ��� ����
            app.UseDefaultFiles();  // index.html �ڵ� ó��
            app.UseStaticFiles();   // wwwroot ���� ���
            app.MapControllers();
            app.Run();
        }
    }
}
