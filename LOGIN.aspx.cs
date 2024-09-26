using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class LOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string usuario = Login1.UserName.ToString();
            string senha = Login1.Password.ToString();


            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            string SQL = "SELECT * FROM USERS WHERE CARGO  = 'ADMIN' AND (USUARIO = @V1 OR EMAIL = @V1) AND SENHA = @V2 "; 

            SqlDataReader dr = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(conexao);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.Parameters.AddWithValue("@V1", usuario);
                cmd.Parameters.AddWithValue("@V2", senha);

                dr = cmd.ExecuteReader();

                if (dr.HasRows) // Se o DataReader contiver linhas (login bem-sucedido)
                {
                    if (dr.Read())
                    {
                        // Login com sucesso
                        e.Authenticated = true;

                        // Redireciona diretamente para MainPage.aspx
                        Response.Redirect("MainPage.aspx", false);
                    }
                }
                else
                {
                    // Login sem sucesso
                    e.Authenticated = false;
                    Login1.FailureText = "Usuário ou senha inválidos. Por favor, tente novamente!";
                }
            }
            catch (Exception ex)
            {
                // Login sem sucesso devido a erro na consulta
                e.Authenticated = false;
                Login1.FailureText = "Falha ao consultar os dados no banco de dados: " + ex.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}