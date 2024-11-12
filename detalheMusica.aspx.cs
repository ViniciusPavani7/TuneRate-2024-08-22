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
    public partial class detalheMusica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UsuarioDoLogin"] != null)
                {
                    string usuarioDoLogin = Session["UsuarioDoLogin"].ToString();
                    verificarTipoDeUsuario(usuarioDoLogin);
                }
                else
                {
                    // Lógica para usuário não autenticado, se necessário
                }

                string titulo = Request.QueryString["nome"];
                if (!string.IsNullOrEmpty(titulo))
                {
                    lblNomeMusica.Text = Server.UrlDecode(titulo);
                    CarregarDetalhesMusica(titulo);
                }
                else
                {
                    lblNomeMusica.Text = "Título da música não encontrado.";
                }

                CarregarComentarios();
            }
        }

        protected void rptComentarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnExcluir = (Button)e.Item.FindControl("btnExcluir");

                string comentarioUserId = DataBinder.Eval(e.Item.DataItem, "UserID").ToString();
                string usuarioLogado = Session["UsuarioDoLogin"]?.ToString();

                if (!string.IsNullOrEmpty(usuarioLogado))
                {
                    bool ehAdministrador = verificarTipoDeUsuario(usuarioLogado);
                    if (comentarioUserId == usuarioLogado || ehAdministrador)
                    {
                        btnExcluir.Visible = true;
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

        private void SetUserProfileImage(Image imgPerfil, string usuarioDoLogin)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string SQL = "SELECT ProfilePicture FROM [TuneRate].[dbo].[UserProfilePictures] WHERE UserId = " +
                         "(SELECT UserId FROM aspnet_Users WHERE UserName = @usuarioDoLogin)";

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, conn);
                    cmd.Parameters.AddWithValue("@usuarioDoLogin", usuarioDoLogin);
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
                            imgPerfil.ImageUrl = "~/imgs/unknown.png";
                        }
                    }
                    else
                    {
                        imgPerfil.ImageUrl = "~/imgs/unknown.png";
                    }
                }
                catch
                {
                    imgPerfil.ImageUrl = "~/imgs/unknown.png";
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string comentario = txtComentario.Text.Trim();
            string nomeMusica = Request.QueryString["nome"];

            if (!string.IsNullOrEmpty(comentario) && !string.IsNullOrEmpty(nomeMusica))
            {
                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
                int musicaId = ObterMusicaIDPorNome(nomeMusica);

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
                            cmd.Parameters.AddWithValue("@Rating", Convert.ToInt32(rblRating.SelectedValue));
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
            SELECT u.UserName AS Usuario, c.Comentario, c.Data, c.Rating, c.CommentID, c.UserID 
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
                            lblNomeAlb.Text = $"Álbum: <a href='detalheAlbum.aspx?nome={Server.UrlEncode(albumTitulo)}'>{albumTitulo}</a>";
                            lblAutor.Text = $"Autor: {reader["Artista"]}";
                            DateTime dataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            AnoLancamento.Text = "Data de Lançamento: " + dataLancamento.ToString("dd/MM/yyyy");

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
