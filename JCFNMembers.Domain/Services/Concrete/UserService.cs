using JCFNMembers.Domain.Entities;
using JCFNMembers.Domain.Repositories.Abstract;
using JCFNMembers.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace JCFNMembers.Domain.Services.Concrete {
    public class UserService : IUserService {
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        IEnumerable<ApplicationUser> User {
            get { return userRepository.User; }
        }

        public ApplicationUser GetUserProfileById(string id) {
            return userRepository.GetUserProfileById(id);
        }

        public ApplicationUser updateUser(ApplicationUser user, string password) {
            return userRepository.updateUser(user, password);
        }

        public void UpdateUser(ApplicationUser user) {
            userRepository.UpdateUser(user);
        }


        public void PasswordRecoveryTokenRequest(string email, string token) {
            try {
                //using (SmtpClient client = new SmtpClient("mail.equipper.org")) {
                //using (SmtpClient client = new SmtpClient("smtp.emailsrvr.com")) {
                //using (SmtpClient client = new SmtpClient("smtp.gmail.com")) {
                //using (SmtpClient client = new SmtpClient("smtpout.secureserver.net")) {
                using (SmtpClient client = new SmtpClient("globalreturnees.org")) {

                    client.UseDefaultCredentials = false;
                    //client.Credentials = new NetworkCredential("ecregistration@equipper.org", "registration1997");
                    //client.Credentials = new NetworkCredential("support@marubeni-trans.com", "01Mtssupport");
                    //client.Credentials = new NetworkCredential("RJC2021online@gmail.com", "JesusChrist#1");
                    //client.Credentials = new NetworkCredential("admin@rjcnetwork.org", "0ScN!TMKx$II");
                    //client.Credentials = new NetworkCredential("info@kingdomcoders.net", "all4HisGlory!");
                    client.Credentials = new NetworkCredential("info@globalreturnees.org", "Ochanomizu1010062");

                    MailMessage mailMessage = new MailMessage();
                    //mailMessage.From = new MailAddress("ecregistration@equipper.org");
                    //mailMessage.From = new MailAddress("support@marubeni-trans.com");
                    //mailMessage.From = new MailAddress("RJC2021online@gmail.com");
                    //mailMessage.From = new MailAddress("info@kingdomcoders.net");
                    mailMessage.From = new MailAddress("info@globalreturnees.org");

                    mailMessage.To.Add(email.Trim());
                    mailMessage.Bcc.Add("yasu_ozeki@hotmail.com");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body =
                    "パスワード変更のリクエストをされた方にこのメールを送らせていただいております。下の指示に従ってパスワード変更の手続きを行ってください。" +
                    "もしパスワード変更のリクエストをされていない場合はこのメールは無視していただいて結構です。あなたのアカウントの安全性は保たれています。" + "<br>" +
                    "We received a request to reset the password associated with this email address. If you made this request, please follow the instructions below." +
                    "If you did not request to have your password reset you can safely ignore this email. Rest assured your customer account is safe." + "<br><br>" +

                    "下のリンクをクリックしてパスワード変更手続きをしてください" + "<br>" +
                    "Click the link below to reset your password:" + "<br><br>" +

                     "<a href='https://registration.globalreturnees.org/#/enter-new-password/token" + token.Replace('/', '$').Replace('+', '&') +
                     "email" + email + "'><b>パスワード変更 Reset Password </b></a> " + "<br><br>" +

                    "このメールは自動送信されたものです。このメールに返信しないでください。ヘルプが必要な場合は以下までご連絡ください" + "<br>" +
                    "This is an automatically generated email, please do not reply. If you need a help, please contact below." + "<br><br>" +

                    "GGRC21事務局" + "<br>" +
                    "GRC21 Office " + "<br>" +
                    "Email: info@globalreturnees.org";
                    mailMessage.Subject = "パスワード変更手続き　Reset Password Assistance";
                    client.Send(mailMessage);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


        public void updateLastAccessedDate(ApplicationUser user) {
            userRepository.updateLastAccessedDate(user);
        }

        public void DeleteUserById(string Id) {
            userRepository.DeleteUserById(Id);
        }


        /*********************************************************************************/

        public void SendMail(string email, string passcode) {
            try {
                using (SmtpClient client = new SmtpClient("smtp.emailsrvr.com")) {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("support@marubeni-trans.com", "01Mtssupport");
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("support@marubeni-trans.com");
                    mailMessage.To.Add(email.Trim());
                    // mailMessage.Bcc.Add("ozeki@marubeni-trans.com");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "Please copy & paste the below passcode to the authentication process screen.<br><font color=blue><b>" + passcode + "</b></font>";
                    mailMessage.Subject = "User Registration";
                    client.Send(mailMessage);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void SendErrorMessageToAdmin(ApplicationUser model, string errorMessage) {
            try {
                using (SmtpClient client = new SmtpClient("smtpout.secureserver.net")) {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("info@kingdomcoders.net", "all4HisGlory!");
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("info@kingdomcoders.net");
                    mailMessage.To.Add("info@kingdomcoders.net");
                    mailMessage.Bcc.Add("yasu_ozeki@hotmail.com");
                    mailMessage.Bcc.Add("oozeki@kingdomcoders.net");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = errorMessage + "***********************************" +
                    "Email:" + model.Email + "<br>" +
                    "UserName:" + model.Email + "<br>" +
                    "CreatedDate:" + DateTime.Today + "<br>" +
                    "LastModifiedDate:" + DateTime.Today + "<br>" +
                    "FirstName:" + model.FirstName + "<br>" +
                    "LastName:" + model.LastName + "<br>" +
                    "Prefix:" + model.Prefix + "<br>" +
                    "Shimei:" + model.Shimei + "<br>" +
                    "Myoji:" + model.Myoji + "<br>" +
                    "Yubin_Bango:" + model.Zip + "<br>" +
                    "To_Do_Fu_Ken & To_Do_Fu_Ken_JP :" + model.State + "<br>" +
                    // "To_Do_Fu_Ken_JP:"+ model.State.Split(':')[1] + "<br>" +
                    "Shi_Gun_Ku:" + model.City + "<br>" +
                    "Cho_Son:" + model.Street + "<br>" +
                    "Apartment_Etc:" + model.Street2 + "<br>" +
                    "Country:" + "Japan" + "<br>" +
                    "Yubin_Bango:" + model.Zip + "<br>" +
                    "Shi_Gun_Ku_JP:" + model.City + "<br>" +
                    "Cho_Son_JP:" + model.Street + "<br>" +
                    "Apartment_Etc_JP:" + model.Street2 + "<br>" +
                    "Country:" + "Japan" + "<br>" +
                    "Street:" + model.Street + "<br>" +
                    "Street2:" + model.Street2 + "<br>" +
                    "City:" + model.City + "<br>" +
                    "State:" + model.State + "<br>" +
                    "Zip:" + model.Zip + "<br>" +
                    "Country:" + model.Country + "<br>" +
                    "Gender:" + model.Gender + "<br>" +
                    "DOB:" + model.DOB + "<br>" +
                    "TelNo:" + model.TelNo + "<br>";

                    mailMessage.Subject = "ERROR - GRC21 Registration";
                    client.Send(mailMessage);
                }
            } catch (Exception ex) {
                throw ex;
            }
        }


        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
