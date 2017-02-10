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

namespace Client
{
    public partial class ClientForm : Form
    {
        Form parentForm;
        string login;
        public ClientForm(Form form, string login)
        {
            InitializeComponent();
            this.login = login;
            labelLogin.Text = login;
            parentForm = form;
            double bal = Connection.Channel.GetBalance(login);
            labelBal.Text = String.Format("{0:0.00}", bal);
            labelValidation.Text = "";
            labelFinalValid.Text = "";

            string[] lastPhones =Connection.Channel.GetLastPhoneNumbers(10, login);
            for (int i = 0; i < lastPhones.Length; i++)
                textBoxPhone.AutoCompleteCustomSource.Add(lastPhones[i]);
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BalanceForm balanceForm = new BalanceForm(labelLogin.Text);
            balanceForm.ShowDialog();
            if(balanceForm.DialogResult == DialogResult.OK)
            {
                double bal = Connection.Channel.GetBalance(login);
                labelBal.Text = String.Format("{0:0.00}", bal);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        private void textBoxPhone_Leave(object sender, EventArgs e)
        {
            if (textBoxPhone.Text.Length != 10)
                labelValidation.Text = "Номер должен содержать 10 цифр";
            else if (!textBoxPhone.Text.All(c => char.IsDigit(c)))
                labelValidation.Text = "Номер должен содержать только цифры";
            else
                labelValidation.Text = "";
        }

        private void textBoxMess_TextChanged(object sender, EventArgs e)
        {
            labelCount.Text = String.Format("Осталось {0} символов", 120 - textBoxMess.Text.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPhone.Text.Length != 10)
            {
                labelValidation.Text = "Номер должен содержать 10 цифр";
                return;
            }
            else if (!textBoxPhone.Text.All(c => char.IsDigit(c)))
            {
                labelValidation.Text = "Номер должен содержать только цифры";
                return;
            }
            else if (textBoxMess.Text.Length < 1)
            {
                labelFinalValid.Text = "Введите сообщение";
                return;
            }
            else
                labelFinalValid.Text = "";


            if (Double.Parse(labelBal.Text) < 0.50)
            {
                labelFinalValid.Text = "Недостаточно средств на балансе. Пополните Ваш счет";
                return;
            }
            bool res = Connection.Channel.SendMessage(login, textBoxPhone.Text, textBoxMess.Text);
            if (!res)
            {
                MessageBox.Show("Произошла ошибка. Повторите позже", "Error");
            }
            else
            {
                MessageBox.Show("Сообщение успешно отправлено! С Вашего баланса списано 50 коп.", "Success");
                double bal = Connection.Channel.GetBalance(login);
                labelBal.Text = String.Format("{0:0.00}", bal);
                textBoxMess.Text = "";
                textBoxPhone.AutoCompleteCustomSource.Add(textBoxPhone.Text);
            }
        }
    }
}
