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
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carregar a lista de artistas no DropDownList
                CarregarArtista();

                // Carregar a lista de álbuns no DropDownList
                PreencherAlbuns();
            }
        }

        private void CarregarArtista()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT ArtistaId, Nome FROM Artistas";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlArtistas.DataSource = reader;
                    ddlArtistas.DataTextField = "Nome";
                    ddlArtistas.DataValueField = "ArtistaId";
                    ddlArtistas.DataBind();
                }
            }
        }

        private void PreencherAlbuns()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT AlbumId, Titulo FROM Albuns";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlAlbuns.DataSource = reader;
                    ddlAlbuns.DataTextField = "Titulo";
                    ddlAlbuns.DataValueField = "AlbumId";
                    ddlAlbuns.DataBind();
                }
            }
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

        private void CarregarArtistas()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT ArtistaId, Nome FROM Artistas ORDER BY Nome";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlArtistas.DataSource = reader;
                    ddlArtistas.DataTextField = "Nome";
                    ddlArtistas.DataValueField = "ArtistaId";
                    ddlArtistas.DataBind();
                }
            }
        }

        protected void btnAdicionarAlbum_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "INSERT INTO Albuns (Titulo, ArtistaId, DataLancamento, CapaBinaria) VALUES (@Titulo, @ArtistaId, @DataLancamento, @CapaBinaria)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", txtTituloAlbum.Text);
                    cmd.Parameters.AddWithValue("@ArtistaId", ddlArtistas.SelectedValue);

                    // Converter a data para o formato americano
                    if (DateTime.TryParse(txtDataLancamentoAlbum.Text, out DateTime dataLancamento))
                    {
                        cmd.Parameters.AddWithValue("@DataLancamento", dataLancamento);
                    }
                    else
                    {
                        LabelErrorAlbum.Text = "Data de lançamento inválida. Por favor, insira uma data válida.";
                        LabelErrorAlbum.Visible = true; // Torna o Label visível
                        return; // Interrompe a execução
                    }

                    if (FileUploadCapa.HasFile)
                    {
                        byte[] capaBytes;
                        using (BinaryReader reader = new BinaryReader(FileUploadCapa.PostedFile.InputStream))
                        {
                            capaBytes = reader.ReadBytes(FileUploadCapa.PostedFile.ContentLength);
                        }
                        cmd.Parameters.AddWithValue("@CapaBinaria", capaBytes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CapaBinaria", DBNull.Value); 
                    }

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        LabelErrorAlbum.Visible = false; // Limpa a mensagem de erro se a inserção for bem-sucedida
                    }
                    catch (Exception ex)
                    {
                        LabelErrorAlbum.Text = "Ocorreu um erro ao adicionar o álbum: " + ex.Message;
                        LabelErrorAlbum.Visible = true; // Torna o Label visível
                    }
                }
            }
        }

        protected void btnAdicionarMusica_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "INSERT INTO Musicas (Titulo, Feats, DataLancamento, DataAdicionado, Capa, AlbumID) VALUES (@Titulo, @Feats, @DataLancamento, @DataAdicionado, @Capa, @AlbumID)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@Feats", txtFeats.Text);

                    // Tenta converter a data de lançamento
                    if (DateTime.TryParse(txtDataLancamento.Text, out DateTime dataLancamento))
                    {
                        cmd.Parameters.AddWithValue("@DataLancamento", dataLancamento);
                    }
                    else
                    {
                        lblMensagem.Text = "Data de lançamento inválida. Por favor, insira uma data no formato AAAA-MM-DD.";
                        return;
                    }

                    // Data atual para DataAdicionado
                    cmd.Parameters.AddWithValue("@DataAdicionado", DateTime.Now);

                    // Converte a imagem de capa para binário e especifica o tipo correto
                    if (FileUpload1.HasFile)
                    {
                        byte[] capaBytes;
                        using (BinaryReader reader = new BinaryReader(FileUpload1.PostedFile.InputStream))
                        {
                            capaBytes = reader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                        }
                        cmd.Parameters.Add("@Capa", SqlDbType.VarBinary).Value = capaBytes;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Capa", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    // Define AlbumID como NULL se for single, ou usa o valor do álbum selecionado
                    if (ddlAlbuns.SelectedValue == "SINGLE")
                    {
                        cmd.Parameters.AddWithValue("@AlbumID", DBNull.Value);
                    }
                    else
                    {
                        if (int.TryParse(ddlAlbuns.SelectedValue, out int albumId))
                        {
                            cmd.Parameters.AddWithValue("@AlbumID", albumId);
                        }
                        else
                        {
                            lblMensagem.Text = "Seleção de álbum inválida.";
                            return;
                        }
                    }

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMensagem.Text = "Música adicionada com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Ocorreu um erro ao adicionar a música: " + ex.Message;
                    }
                }
            }
        }






        protected void btnDeletarUsuario_Click(object sender, EventArgs e)
        {
            string username = txtUsuarioDelete.Text.Trim(); // Pegue o nome do usuário a ser deletado

            if (Membership.GetUser(username) != null)
            {
                // Deletar o usuário e todas as suas associações
                bool userDeleted = Membership.DeleteUser(username, true); // O 'true' também deleta os dados em 'UsersInRoles'

                if (userDeleted)
                {
                    lblMensagem.Text = "Usuário deletado com sucesso!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMensagem.Text = "Erro ao deletar o usuário!";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMensagem.Text = "Usuário não encontrado!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCriarConta_Click(object sender, EventArgs e)
        {
            string username = txtNovoUsuario.Text;
            string password = txtNovaSenha.Text;
            string email = txtEmail.Text;

            MembershipCreateStatus status;
            Membership.CreateUser(username, password, email, null, null, true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                lblMensagemCriar.Text = "Conta criada com sucesso!";
                lblMensagemCriar.Visible = true;
            }
            else
            {
                lblMensagemCriar.Text = "Erro ao criar conta: " + status.ToString();
                lblMensagemCriar.Visible = true;
            }
        }

        private void CarregarUsuarios()
        {
            string connString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM aspnet_Users", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                ddlUsuarios.DataSource = reader;
                ddlUsuarios.DataTextField = "UserName"; // Nome a ser exibido no DropDown
                ddlUsuarios.DataValueField = "UserID"; // Valor a ser usado na lógica
                ddlUsuarios.DataBind();
            }
        }

        protected void btnPraVirarAdmin_Click(object sender, EventArgs e)
        {
            AlterarRole(ddlUsuarios.SelectedValue, "780E493F-EC45-4860-A2A8-EBFAB1B392B0"); // RoleID do Administrador
        }

        /*
        protected void btnPraVirarMembro_Click(object sender, EventArgs e)
        {
            AlterarRole(ddlUsuarios.SelectedValue, "A69019BB-AD81-4A51-92E9-95C989567770"); // RoleID do Membro
        }
        */

        private void AlterarRole(string userId, string roleId)
        {
            string connString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Verifica se o usuário já existe na tabela
                string verificaUsuarioQuery = "SELECT COUNT(*) FROM aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                using (SqlCommand verificaCmd = new SqlCommand(verificaUsuarioQuery, conn))
                {
                    verificaCmd.Parameters.AddWithValue("@UserId", userId);
                    verificaCmd.Parameters.AddWithValue("@RoleId", roleId);
                    int count = (int)verificaCmd.ExecuteScalar();

                    if (count > 0) // O usuário já existe com essa função
                    {
                        
                    }
                    else // O usuário não existe com essa função, insira um novo
                    {
                        // Insere o novo usuário
                        string insertQuery = "INSERT INTO aspnet_UsersInRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserId", userId);
                            insertCmd.Parameters.AddWithValue("@RoleId", roleId);
                            insertCmd.ExecuteNonQuery();
                        }

                        
                       
                    }
                }
            }
        }
    }
}
