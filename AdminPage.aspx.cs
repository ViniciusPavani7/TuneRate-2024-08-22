using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarArtista();
                CarregarFeats();
                CarregarAlbuns();
                CarregarUsuarios();

                rbArtistas.Checked = true;
                ShowTabContent();

                // Chamada assíncrona para carregar as nacionalidades
                await CarregarNacionalidades();
                await CarregarGenerosMusicais();
            }
        }

        public static readonly string ApiUrl = "https://restcountries.com/v3.1/all";
        private static readonly string ClientId = "d3e71f1f93c44fa7b4759b05e26c22a0";
        private static readonly string ClientSecret = "59c249c0c1ee42dbbc46900c64045f78";

        // URL para autenticação e busca de gêneros
        private static readonly string TokenUrl = "https://accounts.spotify.com/api/token";
        private static readonly string GenresUrl = "https://api.spotify.com/v1/recommendations/available-genre-seeds";

        public class Country
        {
            public Demonyms demonyms { get; set; }
        }

        public class Demonyms
        {
            public Dictionary<string, string> eng { get; set; }
        }

        public async Task<List<string>> ObterNacionalidades()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Faz a requisição para a API
                    var response = await client.GetStringAsync(ApiUrl);

                    // Deserializa o JSON para uma lista de países
                    var paises = JsonConvert.DeserializeObject<List<Country>>(response);

                    // Filtra as nacionalidades e extrai as informações
                    var nacionalidades = paises
                        .Where(p => p.demonyms != null && p.demonyms.eng != null && p.demonyms.eng.ContainsKey("m"))
                        .Select(p => p.demonyms.eng["m"])  // Acessa o valor de "m" (masculino) da nacionalidade
                        .OrderBy(n => n)  // Ordena alfabeticamente
                        .ToList();

                    return nacionalidades;
                }
            }
            catch (Exception ex)
            {
                // Exibe apenas mensagens de erro
                lblMensage.Text = "Erro ao obter nacionalidades: " + ex.Message;
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;

                // Retorna uma lista vazia em caso de erro
                return new List<string>();
            }
        }

        private async Task CarregarNacionalidades()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Fazendo a requisição para a API
                    var response = await client.GetStringAsync("https://restcountries.com/v3.1/all");

                    // Deserializa a resposta para um formato de lista de objetos
                    var paises = JsonConvert.DeserializeObject<List<dynamic>>(response);

                    // Filtra as nacionalidades e as organiza
                    var nacionalidades = paises
                        .Where(p => p["demonyms"] != null && p["demonyms"]["eng"] != null)
                        .Select(p => p["demonyms"]["eng"]["m"].ToString())
                        .OrderBy(n => n)
                        .ToList();

                    // Preenche a DropDownList com as nacionalidades
                    ddlNacionalidades.Items.Clear();
                    foreach (var nacionalidade in nacionalidades)
                    {
                        ddlNacionalidades.Items.Add(new ListItem(nacionalidade, nacionalidade));
                    }
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe a mensagem de erro
                lblMensage.Text = "Erro ao carregar as nacionalidades: " + ex.Message;
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;
            }
        }

        private async Task<string> ObterTokenSpotify()
        {
            using (HttpClient client = new HttpClient())
            {
                // Configura os dados para autenticação
                var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + authHeader);

                var requestContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

                // Faz a requisição para obter o token
                HttpResponseMessage response = await client.PostAsync(TokenUrl, requestContent);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

                return data["access_token"];
            }
        }

        private async Task CarregarGenerosMusicais()
        {
            try
            {
                string token = await ObterTokenSpotify();

                using (HttpClient client = new HttpClient())
                {
                    // Adiciona o token de autenticação no cabeçalho
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // Faz a requisição para obter os gêneros
                    HttpResponseMessage response = await client.GetAsync(GenresUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonResponse);

                    // Preenche a DropDownList com os gêneros
                    ddlGeneros.Items.Clear();
                    foreach (string genero in data["genres"])
                    {
                        ddlGeneros.Items.Add(new ListItem(genero, genero));
                    }

                    // Mensagem de sucesso
                    lblMensage.Text = "Gêneros musicais carregados com sucesso!";
                    lblMensage.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                // Mensagem de erro
                lblMensage.Text = "Erro ao carregar gêneros musicais: " + ex.Message;
                lblMensage.ForeColor = System.Drawing.Color.Red;
            }
        }



        protected void TabChanged(object sender, EventArgs e)
        {
            ShowTabContent();

            // Remover a classe de todos os RadioButtons
            RemoveSelectedClass();

            // Adicionar a classe ao RadioButton selecionado
            if (rbArtistas.Checked)
            {
                rbArtistas.CssClass += " selectedTab";
            }
            else if (rbAlbuns.Checked)
            {
                rbAlbuns.CssClass += " selectedTab";
            }
            else if (rbMusicas.Checked)
            {
                rbMusicas.CssClass += " selectedTab";
            }
            else if (rbUsers.Checked)
            {
                rbUsers.CssClass += " selectedTab";
            }
        }

        private void ShowTabContent()
        {
            // Esconde todas as abas
            pnlArtistas.Visible = false;
            pnlAlbuns.Visible = false;
            pnlMusicas.Visible = false;
            pnlUsers.Visible = false;

            // Verifica qual rádio está selecionado e exibe o painel correspondente
            if (rbArtistas.Checked)
            {
                pnlArtistas.Visible = true;
            }
            else if (rbAlbuns.Checked)
            {
                pnlAlbuns.Visible = true;
            }
            else if (rbMusicas.Checked)
            {
                pnlMusicas.Visible = true;
            }
            else if (rbUsers.Checked)
            {
                pnlUsers.Visible = true;
            }
        }

        private void RemoveSelectedClass()
        {
            // Remover a classe de todos os RadioButtons
            rbArtistas.CssClass = rbArtistas.CssClass.Replace(" selectedTab", "");
            rbAlbuns.CssClass = rbAlbuns.CssClass.Replace(" selectedTab", "");
            rbMusicas.CssClass = rbMusicas.CssClass.Replace(" selectedTab", "");
            rbUsers.CssClass = rbUsers.CssClass.Replace(" selectedTab", "");
        }

        private void CarregarFeats()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT ArtistaId, Nome FROM Artistas";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpar itens existentes
                    ddlFeats.Items.Clear();

                    // Adicionar item padrão
                    ddlFeats.Items.Add(new ListItem("Selecione Participações (Feats)", "0"));

                    // Preencher o DropDownList com os artistas
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nome = reader.GetString(1);
                        ddlFeats.Items.Add(new ListItem(nome, id.ToString()));
                    }
                }
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

                    // Limpar itens existentes
                    ddlArtsAlb.Items.Clear();
                    ddlArtsMsc.Items.Clear();
                    ddlArtistasDel.Items.Clear();

                    // Adicionar item padrão
                    ddlArtistasDel.Items.Add(new ListItem("Selecione um Artista", "0"));
                    ddlArtsAlb.Items.Add(new ListItem("Selecione um Artista", "0"));
                    ddlArtsMsc.Items.Add(new ListItem("Selecione um Artista", "0"));

                    // Preencher os DropDownLists com os artistas
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nome = reader.GetString(1);
                        ddlArtistasDel.Items.Add(new ListItem(nome, id.ToString()));
                        ddlArtsAlb.Items.Add(new ListItem(nome, id.ToString()));
                        ddlArtsMsc.Items.Add(new ListItem(nome, id.ToString()));
                    }
                }
            }
        }
        private void CarregarUsuarios()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT UserId, UserName FROM aspnet_Users";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpar itens existentes
                    ddlUsuariosRole.Items.Clear();
                    ddlUsuariosDel.Items.Clear();

                    // Adicionar item padrão
                    ddlUsuariosRole.Items.Add(new ListItem("Selecione um Usuário", "0"));
                    ddlUsuariosDel.Items.Add(new ListItem("Selecione um Usuário", "0"));

                    // Preencher os DropDownLists com os artistas
                    while (reader.Read())
                    {
                        string id = reader.GetGuid(0).ToString(); // GUID do usuário
                        string nome = reader.GetString(1);       // Nome de usuário

                        ddlUsuariosRole.Items.Add(new ListItem(nome, id)); // Nome como valor
                        ddlUsuariosDel.Items.Add(new ListItem(nome, nome)); // Nome como valor
                    }
                }
            }
        }

        private void CarregarAlbuns()
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "SELECT AlbumId, Titulo FROM Albuns";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    // Inicializar o SqlDataReader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Limpar itens existentes
                        ddlAlbuns.Items.Clear();

                        // Adicionar item padrão
                        ddlAlbuns.Items.Add(new ListItem("Selecione um Álbum", "0"));

                        // Preencher os DropDownLists com os artistas
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nome = reader.GetString(1);       // Obter o título como string

                            // Adicionar ao DropDownList
                            ddlAlbuns.Items.Add(new ListItem(nome, id.ToString()));
                        }
                    }
                }
            }
        }

        protected void btnAdicionarArtista_Click(object sender, EventArgs e)
        {
            // Verifica se os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensage.Text = "Por favor, preencha todos os campos.";
                lblMensage.Visible = true;
                return; // Interrompe a execução
            }

            if (!FileUploadImagem.HasFile)
            {
                lblMensage.Text = "Por favor, insira uma imagem para o artista.";
                lblMensage.Visible = true;
                return; // Interrompe a execução
            }

            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                string sql = "INSERT INTO Artistas (Nome, Nacionalidade, GeneroMusical, DataNascimento, FotoBinario) VALUES (@Nome, @Nacionalidade, @GeneroMusical, @DataNascimento, @FotoBinario)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Nacionalidade", ddlNacionalidades.SelectedValue);
                    cmd.Parameters.AddWithValue("@GeneroMusical", ddlGeneros.SelectedValue);

                    // Converter a data para o formato americano
                    if (DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
                    {
                        // Verifica se a data está dentro do intervalo permitido
                        if (dataNascimento < new DateTime(1753, 1, 1))
                        {
                            lblMensage.Text = "Data de nascimento inválida. Deve estar entre 1/1/1753 e 31/12/9999.";
                            lblMensage.Visible = true; // Torna o Label visível
                            return; // Interrompe a execução
                        }
                        cmd.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                    }
                    else
                    {
                        // Trata o erro se a data não for válida
                        lblMensage.Text = "Data de nascimento inválida. Por favor, insira uma data válida.";
                        lblMensage.Visible = true; // Torna o Label visível
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
                        Response.Redirect(Request.Url.AbsoluteUri);
                        lblMensage.Visible = false;                     // Limpa a mensagem de erro se a inserção for bem-sucedida
                    }
                    catch (Exception ex)
                    {
                        lblMensage.Text = "Ocorreu um erro ao adicionar o artista: " + ex.Message;
                        lblMensage.Visible = true; // Torna o Label visível
                    }
                }
            }
        }

        protected void btnDeletarArtista_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
            string nomeArtista = ddlArtistasDel.SelectedValue;
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

                            if (rowsAffected > 0)
                            {
                                lblMensage.Text = "Artista deletado com sucesso!";
                                lblMensage.ForeColor = System.Drawing.Color.Green;
                                lblMensage.Visible = true;
                            }
                            else
                            {
                                lblMensage.Text = "Nenhum artista encontrado com esse ID.";
                                lblMensage.ForeColor = System.Drawing.Color.Red;
                                lblMensage.Visible = true;
                            }
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

                            if (rowsAffected > 0)
                            {
                                lblMensage.Text = "Artista deletado com sucesso!";
                                lblMensage.ForeColor = System.Drawing.Color.Green;
                                lblMensage.Visible = true;
                            }
                            else
                            {
                                lblMensage.Text = "Nenhum artista encontrado com esse nome.";
                                lblMensage.ForeColor = System.Drawing.Color.Red;
                                lblMensage.Visible = true;
                            }
                        }
                    }
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                catch (Exception ex)
                {
                    lblMensage.Text = "Ocorreu um erro: " + ex.Message;
                    lblMensage.Visible = true;
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
                    cmd.Parameters.AddWithValue("@ArtistaId", ddlArtsAlb.SelectedValue);

                    // Converter a data para o formato americano
                    if (DateTime.TryParse(txtDataLancamentoAlbum.Text, out DateTime dataLancamento))
                    {
                        cmd.Parameters.AddWithValue("@DataLancamento", dataLancamento);
                    }
                    else
                    {
                        lblMensage.Text = "Data de lançamento inválida. Por favor, insira uma data válida.";
                        lblMensage.Visible = true; // Torna o Label visível
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
                        lblMensage.Visible = false; // Limpa a mensagem de erro se a inserção for bem-sucedida
                    }
                    catch (Exception ex)
                    {
                        lblMensage.Text = "Ocorreu um erro ao adicionar o álbum: " + ex.Message;
                        lblMensage.Visible = true; // Torna o Label visível
                    }
                }
            }
        }

        protected void btnAdicionarMusica_Click(object sender, EventArgs e)
        {
            string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                int artistaId;
                if (!int.TryParse(ddlArtsMsc.SelectedValue, out artistaId))
                {
                    lblMensage.Text = "Seleção de artista inválida.";
                    lblMensage.ForeColor = System.Drawing.Color.Red;
                    lblMensage.Visible = true;
                    return;
                }

                int featId;
                if (!int.TryParse(ddlFeats.SelectedValue, out featId))
                {
                    lblMensage.Text = "Seleção de feats inválida.";
                    lblMensage.ForeColor = System.Drawing.Color.Red;
                    lblMensage.Visible = true;
                    return;
                }

                int albumId;
                if (!int.TryParse(ddlAlbuns.SelectedValue, out albumId))
                {
                    lblMensage.Text = "Seleção de álbum inválida.";
                    lblMensage.ForeColor = System.Drawing.Color.Red;
                    lblMensage.Visible = true;
                    return;
                }

                // Inserir na tabela Musicas
                string sql = "INSERT INTO Musicas (Titulo, Feats, DataLancamento, DataAdicionado, Capa, AlbumID, Artista) " +
                             "VALUES (@Titulo, @Feats, @DataLancamento, @DataAdicionado, @Capa, @AlbumID, @Artista)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@Feats", featId);  // Insere o ID do feat selecionado

                    if (DateTime.TryParse(txtDataLancamento.Text, out DateTime dataLancamento))
                    {
                        cmd.Parameters.AddWithValue("@DataLancamento", dataLancamento);
                    }
                    else
                    {
                        lblMensage.Text = "Data de lançamento inválida. Por favor, insira uma data no formato AAAA-MM-DD.";
                        lblMensage.ForeColor = System.Drawing.Color.Red;
                        lblMensage.Visible = true;
                        return;
                    }

                    cmd.Parameters.AddWithValue("@DataAdicionado", DateTime.Now);

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

                    cmd.Parameters.AddWithValue("@AlbumID", albumId);
                    cmd.Parameters.AddWithValue("@Artista", artistaId); // Usar o ArtistaID selecionado

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMensage.Text = "Música adicionada com sucesso!";
                        lblMensage.ForeColor = System.Drawing.Color.Green;
                        lblMensage.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblMensage.Text = "Ocorreu um erro ao adicionar a música: " + ex.Message;
                        lblMensage.ForeColor = System.Drawing.Color.Red;
                        lblMensage.Visible = true;
                    }
                }
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
                lblMensage.Text = "Conta criada com sucesso!";
                lblMensage.ForeColor = System.Drawing.Color.Green;
                lblMensage.Visible = true;
            }
            else
            {
                lblMensage.Text = "Erro ao criar conta: " + status.ToString();
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;
            }
        }

        protected void btnDeletarUsuario_Click(object sender, EventArgs e)
        {
            string username = ddlUsuariosDel.SelectedValue;

            if (Membership.GetUser(username) != null)
            {
                // Deletar o usuário e todas as suas associações
                bool userDeleted = Membership.DeleteUser(username, true); // O 'true' também deleta os dados em 'UsersInRoles'

                if (userDeleted)
                {
                    lblMensage.Text = "Usuário deletado com sucesso!";
                    lblMensage.ForeColor = System.Drawing.Color.Green;
                    lblMensage.Visible = true;
                }
                else
                {
                    lblMensage.Text = "Erro ao deletar o usuário!";
                    lblMensage.ForeColor = System.Drawing.Color.Red;
                    lblMensage.Visible = true;
                }
            }
            else
            {
                lblMensage.Text = "Usuário não encontrado!";
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;
            }
        }

        protected void btnPraVirarAdmin_Click(object sender, EventArgs e)
        {
            string selectedUserId = ddlUsuariosRole.SelectedValue;
            string adminRoleId = "780E493F-EC45-4860-A2A8-EBFAB1B392B0"; // RoleID do Administrador

            if (!Guid.TryParse(selectedUserId, out Guid userGuid)) // Verifica se é um GUID válido
            {
                lblMensage.Text = "Selecione um usuário válido.";
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Verifica se o par UserId + RoleId já existe
                    string verificaUsuarioQuery = "SELECT COUNT(*) FROM aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                    using (SqlCommand verificaCmd = new SqlCommand(verificaUsuarioQuery, conn))
                    {
                        verificaCmd.Parameters.AddWithValue("@UserId", userGuid); // Usa o GUID validado
                        verificaCmd.Parameters.AddWithValue("@RoleId", Guid.Parse(adminRoleId)); // GUID fixo do papel de administrador
                        int count = (int)verificaCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            lblMensage.Text = "O usuário já é administrador.";
                            lblMensage.ForeColor = System.Drawing.Color.Red;
                            lblMensage.Visible = true;
                            return;
                        }
                    }

                    // Insere o novo par UserId + RoleId
                    string insertQuery = "INSERT INTO aspnet_UsersInRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@UserId", userGuid); // Usa o GUID validado
                        insertCmd.Parameters.AddWithValue("@RoleId", Guid.Parse(adminRoleId));
                        insertCmd.ExecuteNonQuery();
                    }

                    lblMensage.Text = "Usuário promovido a administrador com sucesso!";
                    lblMensage.ForeColor = System.Drawing.Color.Green;
                    lblMensage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensage.Text = $"Erro ao promover o usuário: {ex.Message}";
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;

            }
        }

        protected void btnPraRemoverAdmin_Click(object sender, EventArgs e)
        {
            string selectedUserId = ddlUsuariosRole.SelectedValue;
            string adminRoleId = "780E493F-EC45-4860-A2A8-EBFAB1B392B0"; // RoleID do Administrador

            if (!Guid.TryParse(selectedUserId, out Guid userGuid)) // Verifica se é um GUID válido
            {
                lblMensage.Text = "Selecione um usuário válido.";
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Verifica se o usuário realmente é um administrador
                    string verificaUsuarioQuery = "SELECT COUNT(*) FROM aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                    using (SqlCommand verificaCmd = new SqlCommand(verificaUsuarioQuery, conn))
                    {
                        verificaCmd.Parameters.AddWithValue("@UserId", userGuid); // Usa o GUID validado
                        verificaCmd.Parameters.AddWithValue("@RoleId", Guid.Parse(adminRoleId)); // GUID fixo do papel de administrador
                        int count = (int)verificaCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            lblMensage.Text = "O usuário não é administrador.";
                            lblMensage.ForeColor = System.Drawing.Color.Red;
                            lblMensage.Visible = true;
                            return;
                        }
                    }

                    // Realiza o DELETE para remover o usuário do papel de administrador
                    string deleteQuery = "DELETE FROM aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@UserId", userGuid); // Usa o GUID validado
                        deleteCmd.Parameters.AddWithValue("@RoleId", Guid.Parse(adminRoleId)); // GUID fixo do papel de administrador
                        int rowsAffected = deleteCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblMensage.Text = "Usuário removido da função de administrador com sucesso!";
                            lblMensage.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMensage.Text = "Erro: o usuário não foi encontrado para remoção.";
                            lblMensage.ForeColor = System.Drawing.Color.Red;
                        }

                        lblMensage.Visible = true;  // Garante que a mensagem será visível
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensage.Text = $"Erro ao remover o usuário: {ex.Message}";
                lblMensage.ForeColor = System.Drawing.Color.Red;
                lblMensage.Visible = true;  // Garante que a mensagem será visível
            }
        }

    }
}
