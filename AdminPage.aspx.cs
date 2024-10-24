using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAdicionarArtista_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "INSERT INTO Artistas (Nome, Nacionalidade, GeneroMusical, DataNascimento, FotoBinario) VALUES (@Nome, @Nacionalidade, @GeneroMusical, @DataNascimento, @FotoBinario)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Nacionalidade", txtNacionalidade.Text);
                    cmd.Parameters.AddWithValue("@GeneroMusical", txtGeneroMusical.Text);

                    // Converter a data para o formato americano
                    if (DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
                    {
                        // Verifica se a data está dentro do intervalo permitido
                        if (dataNascimento < new DateTime(1753, 1, 1))
                        {
                            LabelError.Text = "Data de nascimento inválida. Deve estar entre 1/1/1753 e 31/12/9999.";
                            LabelError.Visible = true; // Torna o Label visível
                            return; // Interrompe a execução
                        }
                        cmd.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                    }
                    else
                    {
                        // Trata o erro se a data não for válida
                        LabelError.Text = "Data de nascimento inválida. Por favor, insira uma data válida.";
                        LabelError.Visible = true; // Torna o Label visível
                        return; // Interrompe a execução
                    }

                    // Convertendo a imagem para binário
                    if (FileUploadImagem.HasFile)
                    {
                        byte[] imagemBytes;
                        using (BinaryReader reader = new BinaryReader(FileUploadImagem.PostedFile.InputStream))
                        {
                            imagemBytes = reader.ReadBytes(FileUploadImagem.PostedFile.ContentLength);
                        }
                        cmd.Parameters.AddWithValue("@FotoBinario", imagemBytes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FotoBinario", DBNull.Value); // Se nenhuma imagem for carregada
                    }

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        LabelError.Visible = false; // Limpa a mensagem de erro se a inserção for bem-sucedida
                    }
                    catch (Exception ex)
                    {
                        LabelError.Text = "Ocorreu um erro ao adicionar o artista: " + ex.Message;
                        LabelError.Visible = true; // Torna o Label visível
                    }
                }
            }
        }

        protected void btnDeletarArtista_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string nomeArtista = txtNomeArtista.Text.Trim(); // Obter o valor do TextBox
            int artistaId;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();
                try
                {
                    if (int.TryParse(nomeArtista, out artistaId))
                    {
                        // Apagar pelo ID
                        string sqlDeleteById = "DELETE FROM Artistas WHERE ArtistaId = @ArtistaId";
                        using (SqlCommand cmd = new SqlCommand(sqlDeleteById, conn))
                        {
                            cmd.Parameters.AddWithValue("@ArtistaId", artistaId);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            lblMensagem.Text = rowsAffected > 0 ? "Artista deletado com sucesso!" : "Nenhum artista encontrado com esse ID.";
                        }
                    }
                    else
                    {
                        // Apagar pelo Nome
                        string sqlDeleteByNome = "DELETE FROM Artistas WHERE Nome = @Nome";
                        using (SqlCommand cmd = new SqlCommand(sqlDeleteByNome, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nome", nomeArtista);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            lblMensagem.Text = rowsAffected > 0 ? "Artista deletado com sucesso!" : "Nenhum artista encontrado com esse nome.";
                        }
                    }

                    // Remova o DBCC CHECKIDENT pois não é mais necessário ajustar o IDENTITY
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro: " + ex.Message;
                }
            }
        }

        protected void btnAdicionarMusic_Click(object sender, EventArgs e)
        {
            try
            {
                string titulo = txtMscTitle.Text;
                string autor = txtMscAuthor.Text;
                string feats = txtMscFeats.Text;
                string genero = txtMscGenre.Text;
                string dataLancamento = txtMscDate.Text;
                byte[] capa = null;

                // Upload da imagem (capa)
                if (ImageMscUpload.HasFile)
                {
                    using (BinaryReader br = new BinaryReader(ImageMscUpload.PostedFile.InputStream))
                    {
                        capa = br.ReadBytes((int)ImageMscUpload.PostedFile.InputStream.Length);
                    }
                }

                // Conexão com o banco de dados

                string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(conexao))
                {
                    string query = "INSERT INTO Musicas (Titulo, Autor, Feats, Capa, DataLancamento, Genero) " +
                                   "VALUES (@Titulo, @Autor, @Feats, @Capa, @DataLancamento, @Genero)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Titulo", titulo);
                        cmd.Parameters.AddWithValue("@Autor", autor);
                        cmd.Parameters.AddWithValue("@Feats", feats);
                        cmd.Parameters.AddWithValue("@Capa", capa ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataLancamento", dataLancamento);
                        cmd.Parameters.AddWithValue("@Genero", genero);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblErrorAdd.Text = "Música adicionada com sucesso!";
                        lblErrorAdd.ForeColor = System.Drawing.Color.Green;
                        lblErrorAdd.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorAdd.Text = "Erro ao adicionar música: " + ex.Message;
                lblErrorAdd.Visible = true;
            }
        }

        protected void btnDeletarMusic_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeOuID = txtDelete.Text;

                // Conexão com o banco de dados

                string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(conexao))
                {
                    string query = "DELETE FROM Musicas WHERE Titulo = @Titulo OR Autor = @Autor";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Titulo", nomeOuID);
                        cmd.Parameters.AddWithValue("@Autor", nomeOuID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblErrorDelete.Text = "Música deletada com sucesso!";
                            lblErrorDelete.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblErrorDelete.Text = "Nenhuma música encontrada com esse nome ou autor.";
                        }
                        lblErrorDelete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorDelete.Text = "Erro ao deletar música: " + ex.Message;
                lblErrorDelete.Visible = true;
            }
        }
    }
}
    
