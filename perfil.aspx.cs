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
            }
        }

        protected void btnShowMenu_Click(object sender, EventArgs e)
        {
            panelUpload.Visible = true;
        }

        protected void btnAlterarFoto_Click(object sender, EventArgs e)
        {

            // Verificar se o controle FileUpload tem um arquivo selecionado
            if (FileUploadImagem.HasFile)
            {
                // Obter o UserId do usuário logado
                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

                // Obter o arquivo da imagem e convertê-lo para binário
                byte[] imagemBytes;
                using (BinaryReader reader = new BinaryReader(FileUploadImagem.PostedFile.InputStream))
                {
                    imagemBytes = reader.ReadBytes(FileUploadImagem.PostedFile.ContentLength);
                }

                // Conectar ao banco de dados e manipular os dados
                string conexao = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TuneRate"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(conexao))
                {
                    // Comando para excluir imagem existente, se houver
                    string deleteSQL = "DELETE FROM UserProfilePictures WHERE UserId = @UserId";
                    string insertSQL = "INSERT INTO UserProfilePictures (UserId, ProfilePicture) VALUES (@UserId, @ProfilePicture)";

                    try
                    {
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

                        lblMensagem.Text = "Foto de perfil alterada com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        // Tratar qualquer erro
                        lblMensagem.Text = "Ocorreu um erro: " + ex.Message;
                    }
                }
            }
            else
            {
                lblMensagem.Text = "Por favor, selecione uma imagem.";
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
                            lblMensagem.Text = "Foto de perfil excluída com sucesso!";
                        }
                        else
                        {
                            lblMensagem.Text = "Nenhuma foto de perfil encontrada para o usuário.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Tratar qualquer erro
                    lblMensagem.Text = "Ocorreu um erro: " + ex.Message;
                }
            }



        }
    }
}