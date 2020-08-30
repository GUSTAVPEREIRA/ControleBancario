namespace ControleBancario.Model
{
    using System;
    using System.Text;
    using ControleBancario.Helpers;
    using System.Security.Cryptography;

    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; private set; }

        public User(string username, string password)
        {
            this.UserName = username;
            this.SetPassword(password);
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            this.DeletedAt = new Nullable<DateTime>();
        }

        public User(string username, string password, string fName = "", string lName = "", string email = "")
        {
            this.UserName = username;
            this.FName = fName;
            this.LName = lName;
            this.SetEmail(email);
            this.SetPassword(password);
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            this.DeletedAt = new Nullable<DateTime>();
        }

        public void SetPassword(string password)
        {            
            if (!string.IsNullOrEmpty(password))
            {
                this.Password = password;
                StringBuilder keyPassword = new StringBuilder();
                MD5 md5 = MD5.Create();
                byte[] input = Encoding.ASCII.GetBytes("//" + this.Password);
                byte[] hash = md5.ComputeHash(input);

                for (int i = 0; i < hash.Length; i++)
                {
                    keyPassword.Append(hash[i].ToString("X2"));
                }

                this.Password = keyPassword.ToString();
            }
            else
            {
                this.Password = "";
            }
        }

        public void SetEmail(string email)
        {
            try
            {
                //Case email is not valid, throw exception!
                if (!new CheckValidEmail().IsValidEmail(email))
                {
                    throw new Exception("Email, não é válido!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            this.Email = email;
        }

        public void SetLogicDeleted()
        {
            this.DeletedAt = DateTime.UtcNow;
        }

        public void UnsetLogicDeleted()
        {
            this.DeletedAt = new Nullable<DateTime>();
        }
    }
}