using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class detalheMusica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                // Verifica se o usuário está autenticado
                if (Session["UsuarioDoLogin"] != null)
                {
                    string usuarioDoLogin = Session["UsuarioDoLogin"].ToString();
                    string userIdSession = ObterUserIdPorUsuario(usuarioDoLogin);

                    Session["UserId"] = userIdSession; // Armazena como string

                    if (!string.IsNullOrEmpty(userIdSession))
                    {
                        verificarTipoDeUsuario(usuarioDoLogin);
                    }
                }


                else
                {
                }

                // Carregar o título da música a partir da query string, se disponível
                string titulo = Request.QueryString["nome"];
                if (!string.IsNullOrEmpty(titulo))
                {
                    lblNomeMusica.Text = Server.UrlDecode(titulo); // Exibe o título da música
                    CarregarDetalhesMusica(titulo);  // Carrega os detalhes da música
                }
                else
                {
                    lblNomeMusica.Text = "Título da música não encontrado."; // Caso o título não seja fornecido
                }

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
                // Obtém o ID do comentário a partir do CommandArgument
                int commentId = Convert.ToInt32(e.CommandArgument);

                // Chama o método para excluir o comentário com base no ID
                ExcluirComentario(commentId);

                // Recarrega os comentários após a exclusão
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
            string connectionString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

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

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string comentario = txtComentario.Text.Trim();
            string nomeMusica = Request.QueryString["nome"];

            if (!string.IsNullOrEmpty(comentario) && !string.IsNullOrEmpty(nomeMusica))
            {
                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
                int musicaId = ObterMusicaIDPorNome(nomeMusica);
                int rating = string.IsNullOrEmpty(rblRating.SelectedValue) ? 0 : Convert.ToInt32(rblRating.SelectedValue);

                if (musicaId != -1)
                {
                    string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO COMENTARIOS (UserID, Comentario, MusicaID, Rating) VALUES (@UserID, @Comentario, @MusicaID, @Rating)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@Comentario", comentario);
                            cmd.Parameters.AddWithValue("@MusicaID", musicaId);
                            cmd.Parameters.AddWithValue("@Rating", rating);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    txtComentario.Text = "";
                    CarregarComentarios();
                }
                else
                {
                    lblMensagemErro.Text = "Erro: Música não encontrada.";
                    lblMensagemErro.Visible = true;
                }
            }
            else
            {
                lblMensagemErro.Text = "Por favor, preencha o comentário e selecione uma música.";
                lblMensagemErro.Visible = true;
            }
        }

        private int ObterMusicaIDPorNome(string nomeMusica)
        {
            if (string.IsNullOrEmpty(nomeMusica))
            {
                return -1;
            }

            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";
            int musicaId = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MusicaID FROM MUSICAS WHERE Titulo = @Titulo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", nomeMusica);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        musicaId = Convert.ToInt32(result);
                    }
                }
            }

            return musicaId;
        }

        private void CarregarComentarios()
        {
            string nomeMusica = Request.QueryString["nome"];
            int musicaId = ObterMusicaIDPorNome(nomeMusica);

            if (musicaId != -1)
            {
                string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
            SELECT u.UserName AS Usuario, c.Comentario, c.Data, c.Rating, c.CommentID, c.UserID AS UserIdComment
            FROM COMENTARIOS c
            INNER JOIN aspnet_Users u ON c.UserID = u.UserId
            WHERE c.MusicaID = @MusicaID
            ORDER BY c.Data DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MusicaID", musicaId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            rptComentarios.DataSource = reader;
                            rptComentarios.DataBind();
                        }
                    }
                }
            }
            else
            {
                lblMensagemErro.Text = "Erro: Música não encontrada.";
                lblMensagemErro.Visible = true;
            }
        }


        private void CarregarDetalhesMusica(string titulo)
        {
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
    SELECT m.Capa, a.AlbumID, a.Titulo AS AlbumTitulo, ar.Nome AS Artista, m.DataLancamento
    FROM MUSICAS m
    JOIN Albuns a ON m.AlbumID = a.AlbumID
    JOIN Artistas ar ON m.Artista = ar.ArtistaID
    WHERE m.Titulo = @Titulo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", titulo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int albumId = (int)reader["AlbumID"];
                            string albumTitulo = reader["AlbumTitulo"].ToString();
                            string nomeArtista = reader["Artista"].ToString();

                            lblNomeAlb.Text = $"<b>Álbum:</b> <a href='detalheAlbum.aspx?nome={Server.UrlEncode(albumTitulo)}'>{albumTitulo}</a>";
                            lblAutor.Text = $"<b>Autor:</b> <a href='detalheArtista.aspx?nome={Server.UrlEncode(nomeArtista)}'>{nomeArtista}</a>";
                            DateTime dataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            AnoLancamento.Text = "<b>Data de Lançamento:</b> " + dataLancamento.ToString("dd/MM/yyyy");

                            byte[] capaBytes = reader["Capa"] as byte[];
                            if (capaBytes != null)
                            {
                                string capaBase64 = Convert.ToBase64String(capaBytes);
                                fotoMsc.ImageUrl = $"data:image/png;base64,{capaBase64}";
                            }
                        }
                        else
                        {
                            Response.Write("Detalhes da música não encontrados.");
                        }
                    }
                }
            }
        }







    }
}
