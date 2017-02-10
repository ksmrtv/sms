using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class BalanceForm : Form
    {
        string login;
        public BalanceForm(string login)
        {
            InitializeComponent();
            this.login = login;
            labelLogin.Text = login+",";
            double bal = Connection.Channel.GetBalance(login);
            labelMoney.Text = String.Format("{0:0.00}", bal);
            labelValidation.Text = "";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            double sum;
            try
            {
                sum = Double.Parse(textBox1.Text);
            }
            catch(Exception)
            {
                labelValidation.Text = "Сумма указана неверно";
                return;
            }
            bool res = Connection.Channel.AddBalance(login, Math.Round(sum, 2));
            if (!res)
                labelValidation.Text = "Произошла ошибка";
            else
                this.DialogResult = DialogResult.OK;

        }
    }
}
