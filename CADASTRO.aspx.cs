using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class CADASTRO : System.Web.UI.Page
    {
       /* protected void Page_Load(object sender, EventArgs e)
        {

        }*/


        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            // Captura os dados inseridos pelo usuário
            string nome = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("FullName")).Text;
            string usuario = CreateUserWizard1.UserName;
            string senha = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Password")).Text;
            string email = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email")).Text;

            string conexao = WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();
                string query = "INSERT INTO USERS (USUARIO, NOME, SENHA, EMAIL, CARGO, STATUS) VALUES (@USUARIO, @NOME, @SENHA, @EMAIL, 'Usuário', 'On')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@NOME", nome);
                cmd.Parameters.AddWithValue("@SENHA", senha);
                cmd.Parameters.AddWithValue("@EMAIL", email);

                cmd.ExecuteNonQuery();
            }

            // Envia e-mail de confirmação
            // EnviarEmailConfirmacao(email);
        }

        /*  private void EnviarEmailConfirmacao(string email)
          {
              string emailBody = "Obrigado por se registrar. Clique no link abaixo para confirmar sua conta: \n";
              emailBody += "https://www.seusite.com/Confirmacao.aspx?email=" + email;

              MailMessage mail = new MailMessage("tunerate.senai@gmail.com", email);
              mail.Subject = "Confirmação de Registro";
              mail.Body = emailBody;

              SmtpClient client = new SmtpClient("smtp.gmail.com");
              client.Port = 587; // Porta SMTP
              client.Credentials = new NetworkCredential("tunerate.senai@gmail.com", "senaiTuneRate");
              client.EnableSsl = true;

              try
              {
                  client.Send(mail);
              }
              catch (Exception ex)
              {
                  // Tratar erros de envio de e-mail
                  Response.Write("Erro ao enviar e-mail: " + ex.Message);
              }
          }*/


        protected void CreateUserWizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            // Obter referências aos controles
            TextBox userNameTextBox = (TextBox)CreateUserWizard1.FindControl("UserName");
            TextBox passwordTextBox = (TextBox)CreateUserWizard1.FindControl("Password");
            TextBox emailTextBox = (TextBox)CreateUserWizard1.FindControl("Email");

            // Validar se os campos estão preenchidos
            if (string.IsNullOrEmpty(userNameTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text))
            {
                // Cancelar a navegação se os campos obrigatórios não estão preenchidos
                e.Cancel = true;
                Response.Write("Por favor, preencha todos os campos obrigatórios.");
                return;
            }

            // Tentar criar o usuário
            MembershipCreateStatus status;
            Membership.CreateUser(userNameTextBox.Text, passwordTextBox.Text, emailTextBox.Text, null, null, true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                // Enviar e-mail de confirmação se a criação foi bem-sucedida
                // EnviarEmailConfirmacao(emailTextBox.Text);
            }
            else
            {
                // Cancelar a navegação se a criação do usuário falhar
                e.Cancel = true;
                Response.Write("Erro ao criar usuário: " + status.ToString());
            }
        }
    }
}