using Microsoft.AspNetCore.Mvc;
using MyPortfolioWebApp.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // HACK : ���߿� ����... ���� �����ʿ� ���۸���.
        [HttpPost]
        public async Task<IActionResult> Contact(string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)
                || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "��� �ʵ带 �Է����ּ���.";
                return View();
            } 

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("personar95@gmail.com", "password"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("personar95@gmail"),
                    Subject = subject ?? "[No Subject]",
                    Body = $"���� ��� : {name} ({email})\n\n�޽���:\n{message}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("destination@example.com");

                await smtpClient.SendMailAsync(mailMessage);
                ViewBag.Success = true;
            } 
            catch (Exception ex)
            {
                ViewBag.Error = "���� ���ۿ� �����߽��ϴ�: " + ex.Message;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
