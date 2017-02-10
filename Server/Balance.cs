using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    public class Balance
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public virtual User User { get; set; }

        public double Sum { get; set; }

        //public Balance() { }

        //public Balance(int userId, double sum)
        //{
        //    User_Id = userId;
        //    Sum = sum;
        //    User = null;

        //    using (var db = new DatabaseContext())
        //    {
        //        User = db.Users.FirstOrDefault(x => x.Id == userId);
        //    }
        //}
    }
}
