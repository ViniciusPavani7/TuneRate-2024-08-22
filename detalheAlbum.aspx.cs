using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class detalheAlbum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Captura o nome do álbum da URL
                string nomeAlbum = Request.QueryString["nome"];

                if (!string.IsNullOrEmpty(nomeAlbum))
                {
                    int albumId = CarregarDetalhesAlbum(nomeAlbum);
                    if (albumId > 0)
                    {
                        CarregarMusicasDoAlbum(albumId); // Carrega as músicas no GridView
                    }
                    else
                    {
                        Response.Write("Álbum não encontrado.");
                    }
                }
                else
                {
                    Response.Write("Parâmetro de álbum inválido.");
                }
            }
        }

        protected void gvMusicas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarMusica")
            {
                string nomeMusica = e.CommandArgument.ToString();
                string url = "detalheMusica.aspx?nome=" + Server.UrlEncode(nomeMusica);
                Response.Redirect(url);
            }
        }

        private int CarregarDetalhesAlbum(string titulo)
        {
            int albumId = 0;
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT al.AlbumID, al.Titulo, al.DataLancamento, al.CapaBinaria, ar.Nome
            FROM Albuns al
            JOIN Artistas ar ON al.ArtistaId = ar.ArtistaId
            WHERE al.Titulo = @Titulo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", titulo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Define o ID do álbum para carregar músicas
                            albumId = reader.GetInt32(reader.GetOrdinal("AlbumID"));

                            // Exibe detalhes do álbum
                            lblNomeAlb.Text = "Álbum: " + reader["Titulo"].ToString();

                            DateTime dataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            AnoLancamento.Text = "Data de Lançamento: " + dataLancamento.ToString("dd/MM/yyyy");

                            string nomeArtista = reader["Nome"].ToString();
                            lblAutor.Text = $"Autor: <a href='detalheArtista.aspx?nome={Server.UrlEncode(nomeArtista)}'>{nomeArtista}</a>";

                            byte[] capaBytes = reader["CapaBinaria"] as byte[];
                            if (capaBytes != null)
                            {
                                string capaBase64 = Convert.ToBase64String(capaBytes);
                                fotoAlb.ImageUrl = $"data:image/png;base64,{capaBase64}";
                            }

                            string tituloWiki = reader["Titulo"].ToString().Replace(" ", "_");
                            string wikiUrl = $"https://pt.wikipedia.org/wiki/{tituloWiki}";
                            Literal1.Text = $"<a href='{wikiUrl}' target='_blank'>Ver na Wikipedia</a>";
                        }
                        else
                        {
                            Response.Write("Detalhes do álbum não encontrados.");
                        }
                    }
                }
            }
            return albumId;
        }

        private void CarregarMusicasDoAlbum(int albumId)
        {
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
        SELECT m.Titulo, 
               ar.Nome AS Artista,
               ISNULL(ar_feat.Nome, '') AS Feat
        FROM Musicas m
        JOIN Albuns a ON m.AlbumID = a.AlbumID 
        JOIN Artistas ar ON a.ArtistaId = ar.ArtistaId 
        LEFT JOIN Artistas ar_feat ON m.Feats = ar_feat.ArtistaId
        WHERE m.AlbumID = @AlbumID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AlbumID", albumId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var musicas = new List<dynamic>();
                        while (reader.Read())
                        {
                            string titulo = reader["Titulo"].ToString();
                            string artista = reader["Artista"].ToString();
                            string feat = reader["Feat"].ToString();

                            // Combina Artista e Feat se Feat não for vazio
                            string artistaOuFeats = string.IsNullOrEmpty(feat) ? artista : $"{artista}, feat. {feat}";
                            musicas.Add(new { Titulo = titulo, ArtistaOuFeats = artistaOuFeats });
                        }

                        gvMusicas.DataSource = musicas;
                        gvMusicas.DataBind();
                    }
                }
            }
        }







    }
}