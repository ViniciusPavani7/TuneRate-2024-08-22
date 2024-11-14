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
using System.Configuration;

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
                SetUserProfileImage(usuarioDoLogin); // Carregar a imagem de perfil
            }
            catch (Exception)
            {
                // Lidar com o erro (não é necessário lançar a exceção aqui)
            }

            // Verificar a página atual para ativar o menu correspondente
            string currentPage = Path.GetFileName(Request.Path);

            if (currentPage == "artistas.aspx")
            {
                navOption3.Attributes.Add("class", "active");
            }
            else if (currentPage == "albuns.aspx")
            {
                navOption2.Attributes.Add("class", "active");
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

        private void SetUserProfileImage(string usuarioDoLogin)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT ProfilePicture FROM [TuneRate].[dbo].[UserProfilePictures] WHERE UserId = " +
                         "(SELECT UserId FROM aspnet_Users WHERE UserName = @usuarioDoLogin)";

            SqlConnection conn = null;
            SqlDataReader dr = null;

            try
            {
                conn = new SqlConnection(conexao);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.Parameters.AddWithValue("@usuarioDoLogin", usuarioDoLogin);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    // Lê os resultados
                    while (dr.Read())
                    {
                        // Verifica se existe uma imagem de perfil para o usuário
                        if (dr["ProfilePicture"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])dr["ProfilePicture"];
                            string base64String = Convert.ToBase64String(imageBytes);
                            imgPerfil.ImageUrl = "data:image/jpeg;base64," + base64String;  // Ajuste o tipo conforme necessário
                        }
                        else
                        {
                            imgPerfil.ImageUrl = "~/imgs/unknown.png";  // Imagem padrão caso não haja imagem
                        }
                    }
                }
                else
                {
                    imgPerfil.ImageUrl = "~/imgs/unknown.png";  // Imagem padrão se não encontrar o UserId na tabela
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                imgPerfil.ImageUrl = "~/imgs/unknown.png";  // Imagem padrão em caso de erro
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

        protected void Pesquisar(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string textoDigitado = SearchBox.Text;

            string query = @"
        SELECT TOP 5 Titulo AS Resultado, 'Musica' AS Origem
        FROM Musicas
        WHERE Titulo LIKE '%' + @TextoDigitado + '%'
        
        UNION

        SELECT TOP 5 Titulo AS Resultado, 'Album' AS Origem
        FROM Albuns
        WHERE Titulo LIKE '%' + @TextoDigitado + '%'
        
        UNION

        SELECT TOP 5 Nome AS Resultado, 'Artista' AS Origem
        FROM Artistas
        WHERE Nome LIKE '%' + @TextoDigitado + '%';
    ";

            DataTable resultados = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TextoDigitado", textoDigitado);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(resultados);
                    }
                }
            }

            if (resultados.Rows.Count > 0)
            {
                rptResultados.DataSource = resultados;
                rptResultados.DataBind();
                pnlResultados.Visible = true;     // Mostra o painel de resultados
                lblNoResults.Visible = false;     // Oculta a mensagem de "não encontrado"
            }
            else
            {
                rptResultados.DataSource = null;
                rptResultados.DataBind();
                pnlResultados.Visible = true;     // Exibe o painel para mostrar a mensagem
                lblNoResults.Visible = true;      // Mostra a mensagem de "não encontrado"
            }
        }

        protected string GerarLink(string origem, string resultado)
        {
            switch (origem)
            {
                case "Musica":
                    return $"detalheMusica.aspx?nome={Server.UrlEncode(resultado)}";
                case "Album":
                    return $"detalheAlbum.aspx?nome={Server.UrlEncode(resultado)}";
                case "Artista":
                    return $"detalheArtista.aspx?nome={Server.UrlEncode(resultado)}";
                default:
                    return "#"; // Link vazio caso a origem não corresponda a nenhum tipo
            }
        }

    }
}