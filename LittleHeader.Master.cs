using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace _2024_08_22_TuneRate
{
    public partial class LittleHeader : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string usuarioDoLogin = Session["UsuarioDoLogin"].ToString();
                verificarTipoDeUsuario(usuarioDoLogin);
            }
            catch (Exception)
            {

                //throw;
            }


            // Verificar a página atual para ativar o menu correspondente
            string currentPage = Path.GetFileName(Request.Path);

            if (currentPage == "resenhas.aspx")
            {
                navOption1.Attributes.Add("class", "active");
            }
            else if (currentPage == "albuns.aspx")
            {
                navOption2.Attributes.Add("class", "active");
            }
            else if (currentPage == "artistas.aspx")
            {
                navOption3.Attributes.Add("class", "active");
            }

            // Verificar se o usuário está logado e se ele pertence ao role "Administrador"
            if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Administrador"))
            {
                // Se o usuário for administrador, o item de administração será visível
                ButtonAdmin.Visible = true;
            }
            else
            {
                // Caso contrário, o item de administração ficará invisível
                ButtonAdmin.Visible = false;
            }
        }


        public void verificarTipoDeUsuario(string usuarioDoLogin)
        {

            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT ROLENAME, * " +
                        "FROM aspnet_Users " +
                        "INNER JOIN aspnet_UsersInRoles ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId " +
                        "INNER JOIN aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId " +
                        "WHERE UserName = '" + usuarioDoLogin + "'";


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
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    // Lê os resultados
                    while (dr.Read())
                    {
                        string roleName = dr["ROLENAME"].ToString();

                        if (roleName != "Administrador")
                        {
                            //ocultar o botão aqui . Exemplo: btnX.Visible = false;
                            ButtonAdmin.Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        protected void ButtonPerfil_Click(object sender, EventArgs e)
        {
            // Redirecionar para a página de perfil
            Response.Redirect("Perfil.aspx");
        }

        protected void ButtonConfiguracoes_Click(object sender, EventArgs e)
        {
            // Redirecionar para a página de configurações
            Response.Redirect("Configuracoes.aspx");
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            // Função de logout
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void ButtonAdmin_Click(object sender, EventArgs e)
        {
            // Redirecionar para a página de administração
            Response.Redirect("AdminPage.aspx");
        }
    }
}