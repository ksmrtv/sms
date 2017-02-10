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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            labelValidation.Text = "";
            textBoxPass.PasswordChar = '*';
        }

        private void buttonRegistry_Click(object sender, EventArgs e)
        {
            buttonRegistry.Enabled = false; ;
            try
            {
                string response = Connection.Channel.RegisterUser(textBoxLogin.Text, textBoxPass.Text);
                this.labelValidation.Text = response;
            }
            catch (EndpointNotFoundException)
            {
                labelValidation.Text = "Ошибка соединения с сервером";
            }
            catch (CommunicationObjectFaultedException)
            {
                labelValidation.Text = "Ошибка соединения с сервером";
            }
            buttonRegistry.Enabled = true; ;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            string response = string.Empty;
            try
            {
                response = Connection.Channel.Enter(textBoxLogin.Text, textBoxPass.Text);
            }
            catch (EndpointNotFoundException)
            {
                labelValidation.Text = "Ошибка соединения с сервером";
                buttonLogin.Enabled = true;
                return;
            }
            catch (CommunicationObjectFaultedException)
            {
                labelValidation.Text = "Ошибка соединения с сервером";
                buttonLogin.Enabled = true;
                return;
            }
            if (!response.Equals("Success"))
                this.labelValidation.Text = response;
            else
            {
                ClientForm clientForm = new ClientForm(this, textBoxLogin.Text);
                clientForm.Show();
                this.Hide();
                textBoxPass.Text = "";
            }
            buttonLogin.Enabled = true;
        }
    }
}
