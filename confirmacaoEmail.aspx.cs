using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class confirmacaoEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];

            if (!string.IsNullOrEmpty(email))
            {
                string conexao = WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(conexao))
                {
                    conn.Open();
                    string query = "UPDATE USERS SET STATUS = 'On' WHERE EMAIL = @EMAIL";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EMAIL", email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMensagem.Text = "Sua conta foi confirmada com sucesso!";
                    }
                    else
                    {
                        lblMensagem.Text = "Falha ao confirmar a conta. Verifique o link de confirmação.";
                    }
                }
            }
            else
            {
                lblMensagem.Text = "E-mail inválido.";
            }
        }
}