using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context; // DB����

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            // ���� HTML�� DB �����ͷ� ����ó��
            // DB���� �����͸� �ҷ��� �� About, Skill ��ü�� ������ ��Ƽ� ��� �Ѱ���
            var skillCount = _context.Skill.Count();
            var skill = await _context.Skill.ToListAsync();
            // FirstAsync�� �����Ͱ� ������ ���ܹ߻�. FirstOrDefaultAsync �����Ͱ� ������ �ΰ�
            var about = await _context.About.FirstOrDefaultAsync(); 

            ViewBag.SkillCount = skillCount; // ex. 7�� �Ѿ
            ViewBag.ColNum = (skillCount / 2) + (skillCount % 2); // 3(7/2) + 1(7%2)

            var model = new AboutModel();
            model.About = about;
            model.Skill = skill;

            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (ModelState.IsValid) // Model�� �� �װ� ���� ����� ������
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.mail.nate.com") // Gmail�� ����ϸ� 
                    {
                        Port = 465, // ���� SMPT ������Ʈ �����ʿ�
                        Credentials = new NetworkCredential("personar95@gmail.com", "��й�ȣ"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(model.Email),  // �����ϱ⿡ �ۼ��� �����ּ�
                        Subject = model.Subject ?? "[�������]",
                        Body = $"���� ��� : {model.Name} ({model.Email})\n\n�޽��� : {model.Message}",
                        IsBodyHtml = false,  // ���� ������ HTML�±׸� ��뿩��
                    };

                    mailMessage.To.Add("personar95@naver.com");  // ���� �����ּ�

                    await smtpClient.SendMailAsync(mailMessage); // �� ������ ���ϰ�ü�� ����!
                    ViewBag.Success = true;
                }
                catch (Exception ex)
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"�������� ����! {ex.Message}";
                }                
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
