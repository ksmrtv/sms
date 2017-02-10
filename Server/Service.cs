using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Service : IContract
    {
        public string RegisterUser(string login, string pass)
        {
            using (var db = new DatabaseContext())
            {
                foreach (var u in db.Users)
                {
                    if (u.Login.Equals(login))
                        return "Такой логин уже существует";
                }

                User user = new User(login, pass);
                db.Users.Add(user);
                db.SaveChanges();
                return "Вы зарегистрированы. Нажмите Войти для входа.";
            }

        }
        public string Enter(string login, string pass)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login.Equals(login));

                if (user != null)
                {
                    if (user.ValidatePassworg(pass))
                        return "Success";
                    else
                        return "Неверный пароль";
                }
                else
                    return "Пользователя с таким логином не существует";
            }
        }

        public double GetBalance(string login)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login.Equals(login));
                if (user == null)
                    return 0.00;
                else
                {
                    Balance bal = db.Balances.FirstOrDefault(x => x.User_Id.Equals(user.Id));
                    if (bal != null)
                        return bal.Sum;
                    else
                        return 0.00;
                }
            }
        }

        public bool AddBalance(string login, double sum)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login.Equals(login));
                if (user == null)
                    return false;
                else
                {
                    Balance bal = db.Balances.FirstOrDefault(x => x.User_Id.Equals(user.Id));
                    if (bal == null)
                    {
                        bal = new Balance() { User_Id = user.Id, Sum = sum };
                        db.Balances.Add(bal);
                    }
                    else
                        bal.Sum += sum;
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool SendMessage(string login, string phone, string message)
        {
            using (var db = new DatabaseContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login.Equals(login));
                if (user == null)
                    return false;
                Balance bal = db.Balances.FirstOrDefault(x => x.User_Id == user.Id);
                if (bal == null || bal.Sum < 0.5)
                    return false;

                Message mess = new Message { Date = DateTime.Now, Number = phone, Text = message, User_Id = user.Id };
                db.Messages.Add(mess);
                bal.Sum -= 0.50;
                db.SaveChanges();
                return true;
            }
        }

        public string[] GetLastPhoneNumbers(int countNumbers, string login)
        {
            string[] res = null;
            using (var db = new DatabaseContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Login == login);
                if (user == null)
                    return res;

                var list = db.Messages.Where(x => x.User_Id == user.Id)
                                    .GroupBy(x => x.Number)
                                    .Select(grp => new { nmb = grp.Key, dt = grp.Max(x => x.Date) })
                                    .Select(d => d.nmb)
                                    .Take(countNumbers);

                res = new string[list.Count()];
                int i = 0;
                foreach(string l in list)
                {
                    res[i++] = l;
                }
                return res;
            }
        }
    }
}
