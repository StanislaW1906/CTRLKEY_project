using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendPasswordResetEmail(string email, string resetLink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("CTRLKEY", _config["EmailSettings:SmtpUsername"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Восстановление пароля";
        message.Body = new TextPart("html") 
        { 
            Text = $"Для сброса пароля перейдите <a href='{resetLink}'>по ссылке</a>." 
        };

        using var client = new SmtpClient();
        
        // Подключение с шифрованием (StartTLS)
        await client.ConnectAsync(
            _config["EmailSettings:SmtpServer"], 
            _config.GetValue<int>("EmailSettings:Port"), 
            SecureSocketOptions.StartTls);  

        // Аутентификация
        await client.AuthenticateAsync(
            _config["EmailSettings:SmtpUsername"], 
            _config["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}