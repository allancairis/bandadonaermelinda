using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using BandaDonaErmelinda.Class;
using Microsoft.AspNetCore.Mvc;
using BandaDonaErmelinda.Models; 

namespace BandaDonaErmelinda.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AppDbContext dbContext)
    {
        _logger = logger;
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Galeria()
    {
        return View();
    }
    
    public IActionResult Contato()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public ActionResult SendEmail(string txtNome, string txtEmail, string txtMensagem, string txtWebsite)
    {
        try
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
            var fromAddress = new MailAddress(emailSettings.SmtpUsername, emailSettings.FromName);

            var toAddress = new MailAddress(emailSettings.ToEmail, txtNome);
            string fromPassword = emailSettings.SmtpPassword;
            const string subject = "Envio do site Dona Ermelinda";
            string body = txtMensagem;

            var smtp = new SmtpClient
            {
                Host = "mail.cairis.com.br",
                Port = 8889,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
                   {
                       Subject = subject,
                       Body = body
                   })
            {
                smtp.Send(message);
            }

            return Json(new { success = true, message = "Mensagem enviada!" });
        }
        catch (Exception e)
        {
            return Json(new { success = true, message = "Erro de envio: " + e.Message });
        }
    }
    
    [HttpPost]
    public IActionResult Subscribe(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            var subscriber = new Newsletter { Email = email };
            _dbContext.Newsletter.Add(subscriber);
            _dbContext.SaveChanges();

            return Json(new { success = true, message = "Inscrição bem-sucedida!" });
        }
        return Json(new { success = false, message = "O e-mail não pode estar vazio." });
    }
}