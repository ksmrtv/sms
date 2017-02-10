using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using System.Threading;

namespace Server
{
    public partial class ServerForm : Form
    {
        ServiceHost host;
        public ServerForm()
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";

            try
            {
                host = new ServiceHost(typeof(Service));
                host.Open();
            }
            catch (Exception err)
            {
                label2.Text = "Произошла ошибка:";
                label1.Text = err.Message;
            }

            string connectStr = ConfigurationManager.ConnectionStrings["SmsDbContext"].ConnectionString;
            labelds.Text = connectStr.Substring("Data Source=".Length, connectStr.IndexOf(";") - "Data Source=".Length);
            labelInitCat.Text = connectStr.Substring(connectStr.IndexOf("Initial Catalog=") + "Initial Catalog=".Length, connectStr.IndexOf(";Integrated Security") - (connectStr.IndexOf("Initial Catalog=") + "Initial Catalog=".Length));

            Thread thread = new Thread(CheckDbConnection);
            thread.IsBackground = true;
            thread.Start();
        }

        public void CheckDbConnection()
        {
            try
            {
                label2.Text = "Проверка соединения с базой данных...";
                label1.Text = "Измените data source в конфигурационном файле!";
                using (var db = new DatabaseContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Login.Length > 0);
                    label2.Text = "Соединение с базой данных установлено";
                    label1.Text = "Server is ready";
                }
            }
            catch (Exception err)
            {
                label2.Text = "Произошла ошибка:";
                label1.Text = err.Message;
            }

        }
    }
}
