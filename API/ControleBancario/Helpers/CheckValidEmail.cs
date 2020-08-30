namespace ControleBancario.Helpers
{
    using System;

    public class CheckValidEmail
    {        
        public bool IsValidEmail(string email)
        {           
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}