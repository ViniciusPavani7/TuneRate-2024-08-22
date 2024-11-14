using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string usuarioDoLogin = Session["UsuarioDoLogin"].ToString();
                SetUserProfileImage(usuarioDoLogin);
                SetUserName(usuarioDoLogin);
                SetUserEmail(usuarioDoLogin);
                CarregarComentarios();
            }

        }

        protected void rptComentarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Encontrar o controle do botão de excluir
                Button btnExcluir = (Button)e.Item.FindControl("btnExcluir");

                // Obtém o UserId do comentário e o UserId da sessão
                string userIdComment = DataBinder.Eval(e.Item.DataItem, "UserIdComment").ToString();
                string userIdSession = Session["UserId"]?.ToString();

                // Encontrar o controle da imagem de perfil
                Image imgPerfil = (Image)e.Item.FindControl("imgPerfil");

                // Obtém o UserId associado ao comentário para uso na imagem de perfil
                string comentarioUserId = DataBinder.Eval(e.Item.DataItem, "UserIdComment").ToString();

                // Chamar o método para carregar a imagem de perfil
                SetUserProfileImageForCommentUser(imgPerfil, comentarioUserId);

                if (!string.IsNullOrEmpty(userIdSession))
                {
                    // Verificar se o usuário logado é administrador
                    bool ehAdministrador = verificarTipoDeUsuario(Session["UsuarioDoLogin"].ToString()); // Função que retorna true se for administrador

                    // Comparando o UserId do comentário com o usuário logado ou se ele é um administrador
                    if (userIdComment == userIdSession || ehAdministrador)
                    {
                        btnExcluir.Visible = true;
                    }
                    else
                    {
                        btnExcluir.Visible = false;
                    }
                }
            }
        }

        private string ObterUserIdPorUsuario(string usuarioDoLogin)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string query = "SELECT UserId FROM aspnet_Users WHERE UserName = @usuarioDoLogin";

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuarioDoLogin", usuarioDoLogin);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString(); // Retorna como string
                    }
                    else
                    {
                        return string.Empty; // Retorna string vazia se não encontrar o usuário
                    }
                }
            }
        }



        private bool verificarTipoDeUsuario(string usuarioDoLogin)
        {
            bool ehAdministrador = false;
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT ROLENAME " +
                         "FROM aspnet_Users " +
                         "INNER JOIN aspnet_UsersInRoles ON aspnet_UsersInRoles.UserId = aspnet_Users.UserId " +
                         "INNER JOIN aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId " +
                         "WHERE UserName = @usuarioDoLogin";

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuarioDoLogin", usuarioDoLogin);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string roleName = dr["ROLENAME"].ToString();
                                if (roleName == "Administrador")
                                {
                                    ehAdministrador = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Tratar exceção se necessário
                }
            }

            return ehAdministrador;
        }

        protected void rptComentarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int commentId = Convert.ToInt32(e.CommandArgument);

                // Chama o método para excluir o comentário com base no ID
                ExcluirComentario(commentId);

                // Recarrega os comentários para atualizar a lista
                CarregarComentarios();
            }
        }

        private bool UsuarioEhAdministrador(string userId)
        {
            string roleIdAdmin = "780E493F-EC45-4860-A2A8-EBFAB1B392B0";
            using (SqlConnection conn = new SqlConnection("TuneRate"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId", conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@RoleId", roleIdAdmin);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void ExcluirComentario(int commentId)
        {
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM COMENTARIOS WHERE CommentID = @CommentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", commentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SetUserProfileImageForCommentUser(Image imgPerfil, string userId)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT ProfilePicture FROM [TuneRate].[dbo].[UserProfilePictures] WHERE UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows && dr.Read())
                    {
                        if (dr["ProfilePicture"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])dr["ProfilePicture"];
                            string base64String = Convert.ToBase64String(imageBytes);
                            imgPerfil.ImageUrl = "data:image/jpeg;base64," + base64String;
                        }
                        else
                        {
                            imgPerfil.ImageUrl = "~/imgs/unknown.png"; // Imagem padrão
                        }
                    }
                    else
                    {
                        imgPerfil.ImageUrl = "~/imgs/unknown.png"; // Imagem padrão caso o UserId não exista
                    }
                }
                catch
                {
                    imgPerfil.ImageUrl = "~/imgs/unknown.png"; // Caso algum erro aconteça, usa a imagem padrão
                }
            }

            imgPerfil.DataBind(); // Garante que o controle é atualizado após a atribuição da imagem
        }



        protected string GenerateStarRating(int rating)
        {
            string stars = "";
            for (int i = 1; i <= 5; i++)
            {
                if (i <= rating)
                {
                    stars += "★"; // Estrela preenchida
                }
                else
                {
                    stars += "☆"; // Estrela vazia
                }
            }
            return stars;
        }

        private void SetUserName(string usuarioDoLogin)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT UserName FROM aspnet_Users WHERE UserId = " +
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
                        // Verifica se há o nome de usuário
                        if (dr["UserName"] != DBNull.Value)
                        {
                            string userName = dr["UserName"].ToString();
                            lblUserName.Text = userName;  // Aqui estamos atribuindo o nome do usuário ao Label
                        }
                        else
                        {
                            lblUserName.Text = "Usuário não encontrado";  // Caso não encontre o UserName
                        }
                    }
                }
                else
                {
                    lblUserName.Text = "Usuário não encontrado";  // Caso não encontre o UserId na tabela
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                lblUserName.Text = "Erro ao recuperar o nome de usuário";  // Mensagem de erro
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void SetUserEmail(string usuarioDoLogin)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT Email FROM aspnet_Membership WHERE UserId = " +
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
                        // Verifica se há o email
                        if (dr["Email"] != DBNull.Value)
                        {
                            string email = dr["Email"].ToString();
                            lblUserEmail.Text = email;  // Aqui estamos atribuindo o email ao Label
                        }
                        else
                        {
                            lblUserEmail.Text = "Email não encontrado";  // Caso não encontre o Email
                        }
                    }
                }
                else
                {
                    lblUserEmail.Text = "Email não encontrado";  // Caso não encontre o UserId na tabela
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções, se necessário
                lblUserEmail.Text = "Erro ao recuperar o email";  // Mensagem de erro
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        protected void btnAlterarFoto_Click(object sender, EventArgs e)
        {
            // Verifica se um arquivo foi selecionado
            if (FileUploadImagem.HasFile)
            {
                try
                {
                    // Obter o arquivo da imagem e convertê-lo para binário
                    byte[] imagemBytes;
                    using (BinaryReader reader = new BinaryReader(FileUploadImagem.PostedFile.InputStream))
                    {
                        imagemBytes = reader.ReadBytes(FileUploadImagem.PostedFile.ContentLength);
                    }

                    // Obter o UserId do usuário logado
                    Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

                    // Conectar ao banco de dados e manipular os dados
                    string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(conexao))
                    {
                        string deleteSQL = "DELETE FROM UserProfilePictures WHERE UserId = @UserId";
                        string insertSQL = "INSERT INTO UserProfilePictures (UserId, ProfilePicture) VALUES (@UserId, @ProfilePicture)";

                        conn.Open();

                        // Excluir a entrada antiga, se existir
                        using (SqlCommand deleteCmd = new SqlCommand(deleteSQL, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@UserId", userId);
                            deleteCmd.ExecuteNonQuery();
                        }

                        // Inserir a nova imagem
                        using (SqlCommand insertCmd = new SqlCommand(insertSQL, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@ProfilePicture", imagemBytes);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

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

        protected void btnDeletarFoto_Click(object sender, EventArgs e)
        {
            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

            // Conectar ao banco de dados e manipular os dados
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                // Comando para excluir a imagem existente, se houver
                string deleteSQL = "DELETE FROM UserProfilePictures WHERE UserId = @UserId";

                try
                {
                    conn.Open();

                    // Excluir a entrada da imagem associada ao UserId
                    using (SqlCommand deleteCmd = new SqlCommand(deleteSQL, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@UserId", userId);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Tratar qualquer erro

                }
            }
        }

        private void CarregarComentarios()
        {
            Guid usuarioDoLogin = (Guid)Membership.GetUser().ProviderUserKey;
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT u.UserName AS Usuario, c.Comentario, c.Data, c.Rating, c.CommentID, c.UserID AS UserIdComment, m.Titulo
            FROM COMENTARIOS c
            INNER JOIN aspnet_Users u ON c.UserID = u.UserId
            INNER JOIN Musicas m ON c.MusicaID = m.MusicaID
            WHERE c.UserID = @UserID
            ORDER BY c.Data DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", usuarioDoLogin);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        rptComentarios.DataSource = reader;
                        rptComentarios.DataBind();
                    }
                }
            }
        }




    }
}