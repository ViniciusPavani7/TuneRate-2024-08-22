using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate.Detalhes
{
    public partial class detalheArtista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Captura o nome do artista da URL
                string nomeArtista = Request.QueryString["nome"];

                if (!string.IsNullOrEmpty(nomeArtista))
                {
                    CarregarDetalhesArtista(nomeArtista);
                }
                else
                {
                    Response.Write("Artista não encontrado.");
                }
            }
        }

        private void CarregarDetalhesArtista(string nome)
        {
            // Exemplo de consulta ao banco para obter detalhes do artista
            string connectionString = "Server=localhost;Database=TuneRate;Integrated Security=True;"; // Substitua pela sua string de conexão
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Nome, Nacionalidade, FotoBinario FROM Artistas WHERE Nome = @Nome";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblNomeArt.Text = reader["Nome"].ToString();
                            lblNation.Text = reader["Nacionalidade"].ToString();

                            byte[] fotoBytes = reader["FotoBinario"] as byte[];
                            if (fotoBytes != null)
                            {
                                string fotoBase64 = Convert.ToBase64String(fotoBytes);
                                fotoArt.ImageUrl = $"data:image/png;base64,{fotoBase64}";
                            }

                            // Criar o link dinâmico para a Wikipedia usando o nome do artista
                            string nomeWiki = reader["Nome"].ToString().Replace(" ", "_"); // Substituir espaços por underline
                            string wikiUrl = $"https://pt.wikipedia.org/wiki/{nomeWiki}";
                            wikiLink.Text = $"<a href='{wikiUrl}' target='_blank'>Ver na Wikipedia</a>";
                        }
                        else
                        {
                            Response.Write("Detalhes do artista não encontrados.");
                        }
                    }
                }
            }
        }
    }

}
