using System.Diagnostics;
using BandaDonaErmelinda.Class;
using Microsoft.AspNetCore.Mvc;
using BandaDonaErmelinda.Models; 

namespace BandaDonaErmelinda.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
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
        var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
        var fromAddress = new System.Net.Mail.MailAddress(emailSettings.SmtpUsername, emailSettings.FromName);
        var toAddress = new System.Net.Mail.MailAddress("postmaster@cairis.com.br", txtNome);
        string fromPassword = emailSettings.SmtpPassword;
        const string subject = "Envio do site Dona Ermelinda";
        string body = txtMensagem;
        
        var smtp = new System.Net.Mail.SmtpClient
        {
            Host = "mail.cairis.com.br",
            Port = 8889,
            EnableSsl = false,
            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
               {
                   Subject = subject,
                   Body = body
               })
        {
            smtp.Send(message);
        }
        return Json(new { mensagem = "Dados recebidos com sucesso!" });
    }
}