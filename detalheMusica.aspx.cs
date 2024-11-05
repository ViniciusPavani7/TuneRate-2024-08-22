using System;
using System.Collections.Generic;
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
                // Obtém o título da música da URL
                string titulo = Request.QueryString["nome"];

                if (!string.IsNullOrEmpty(titulo))
                {
                    // Define o texto do Label com o título da música
                    lblNomeMusica.Text = Server.UrlDecode(titulo);

                    // Opcional: você também pode carregar outros detalhes da música
                    CarregarDetalhesMusica(titulo);
                }
                else
                {
                    lblNomeMusica.Text = "Título da música não encontrado.";
                }
            }
        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string comentario = txtComentario.Text.Trim();
            string nomeMusica = Request.QueryString["nome"]; // Obtém o nome da música da URL

            // Verifica se o comentário e o nome da música não são nulos ou vazios
            if (!string.IsNullOrEmpty(comentario) && !string.IsNullOrEmpty(nomeMusica))
            {
                // Obtém o UserID do usuário autenticado
                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

                // Obtém o MusicaID com base no nome da música da URL
                int musicaId = ObterMusicaIDPorNome(nomeMusica);

                // Verifica se o MusicaID foi encontrado no banco
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
                            cmd.Parameters.AddWithValue("@Rating", Convert.ToInt32(rblRating.SelectedValue)); // Captura o rating
                            cmd.ExecuteNonQuery();
                        }
                    }
                    txtComentario.Text = ""; // Limpa o campo de comentário após o envio
                    CarregarComentarios(); // Recarrega a lista de comentários
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
            // Se nomeMusica for nulo ou vazio, retorna -1
            if (string.IsNullOrEmpty(nomeMusica))
            {
                return -1;
            }

            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";
            int musicaId = -1; // Valor padrão caso a música não seja encontrada

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MusicaID FROM MUSICAS WHERE Titulo = @Titulo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Adiciona o parâmetro @Nome
                    cmd.Parameters.AddWithValue("@Titulo", nomeMusica);

                    // Executa a consulta e converte o resultado
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

            if (musicaId != -1) // Verifica se a música foi encontrada
            {
                string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT u.UserName AS Usuario, c.Comentario, c.Data, c.Rating 
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
                            // Atualiza o label com o link do álbum
                            int albumId = (int)reader["AlbumID"];
                            string albumTitulo = reader["AlbumTitulo"].ToString();
                            lblNomeAlb.Text = $"Álbum: <a href='detalheAlbum.aspx?nome={Server.UrlEncode(albumTitulo)}'>{albumTitulo}</a>";

                            // Continua carregando as outras informações
                            lblAutor.Text = $"Autor: {reader["Artista"]}";
                            DateTime dataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            AnoLancamento.Text = "Data de Lançamento: " + dataLancamento.ToString("dd/MM/yyyy");

                            // Para a capa da música
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
