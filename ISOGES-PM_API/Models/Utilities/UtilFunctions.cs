using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ISOGES_PM_API.Models.Utilities
{
    public class UtilFunctions
    {
        public string CreatePassword()
        {
            int length = 10;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%&*";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void SendEmail(string destinatario, string asunto, string mensaje)
        {
            string CuentaEmail = ConfigurationManager.AppSettings["CuentaEmail"].ToString();
            string PasswordEmail = ConfigurationManager.AppSettings["PasswordEmail"].ToString();

            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(destinatario));
                msg.From = new MailAddress(CuentaEmail);
                msg.Subject = asunto;
                msg.Body = mensaje;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(CuentaEmail, PasswordEmail);
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(msg);
                Console.WriteLine("Correo enviado al: "+destinatario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }

        public void SendMail(string NombreCorreo, string Asunto, string Cuerpo)
        {
            string CuentaEmail = ConfigurationManager.AppSettings["CuentaEmail"].ToString();
            string PasswordEmail = ConfigurationManager.AppSettings["PasswordEmail"].ToString();

            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(NombreCorreo));
                msg.From = new MailAddress(CuentaEmail);
                msg.Subject = Asunto;
                msg.Body = Cuerpo;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(CuentaEmail, PasswordEmail);
                client.Port = 587;
                client.Host = "smtp.office365.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(msg);
                Console.WriteLine("Correo enviado al: " + NombreCorreo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}