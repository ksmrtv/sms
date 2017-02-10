using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public User() { }

        public User(string login, string passText)
        {
            Login = login;
            PasswordSalt = GenerateSalt();
            Password = Hash(passText, PasswordSalt);
        }

        private static string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[8];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private static string Hash(string value, string salt)
        {
            string saltedvalue = value + salt;
            byte[] saltedvb = Encoding.UTF8.GetBytes(saltedvalue);
            return Convert.ToBase64String(new SHA256Managed().ComputeHash(saltedvb));
        }

        public bool ValidatePassworg(string pass)
        {
            string passwordHash = Hash(pass, PasswordSalt);
            return Password.SequenceEqual(passwordHash);
        }
    }
}
