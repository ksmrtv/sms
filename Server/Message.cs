using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set;}
        public virtual User User { get; set; }

        public string Number { set; get; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
