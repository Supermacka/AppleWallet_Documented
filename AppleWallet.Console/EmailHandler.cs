using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


namespace AppleWallet.Console;

public static class EmailHandler
{
    private const string FromEmail = "supermacka12@gmail.com";
    private const string FromDisplayName = "Colin Ticket Master";
    private const string Password = "Pannkakor";

    private const string ToEmail = "colin.farkas@hotmail.se";
    private const string ToDisplayName = "Colin (receiver)";
    
    public static void Send(byte[] responseData)
    {
        try
        {
            var fromAddress = new MailAddress(FromEmail, FromDisplayName);
            var toAddress = new MailAddress(ToEmail, ToDisplayName);
            const string fromPassword = Password;
            
            const string subject = "ALV Biljett";
            const string body = "Tack för ditt köp!";
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Attachments = { 
                    new Attachment(
                        new MemoryStream(responseData, false), 
                        "ALV-biljett.pkpass", "application/vnd.apple.pkpass") 
                },
            };

            smtp.Send(message);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
    }
}