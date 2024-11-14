<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="_2024_08_22_TuneRate.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            padding: 0;
            margin: 0;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div ID="Arts">
        <h2>Artistas</h2>
        <h3>Adicionar Artista</h3>
        <asp:TextBox ID="txtNome" runat="server" placeholder="Nome" />
        <asp:TextBox ID="txtNacionalidade" runat="server" placeholder="Nacionalidade" />
        <asp:TextBox ID="txtGeneroMusical" runat="server" placeholder="Gênero Musical" />

        
                <asp:TextBox ID="txtDataNascimento" runat="server" onkeyup="formatDate(this);" MaxLength="10" />

                <script type="text/javascript">
                    function formatDate(input) {
                        // Remove todos os caracteres que não são números
                        let value = input.value.replace(/[^0-9]/g, '');

                        // Formata a data automaticamente
                        if (value.length > 4) {
                            value = value.slice(0, 4) + '-' + value.slice(4); // Adiciona o separador de ano
                        }
                        if (value.length > 7) {
                            value = value.slice(0, 7) + '-' + value.slice(7); // Adiciona o separador de mês
                        }

                        input.value = value; // Atualiza o valor do input
                    }
                </script>
        

        <asp:FileUpload ID="FileUploadImagem" runat="server" />
        <asp:Button ID="btnAdicionarArtista" runat="server" Text="Adicionar Artista" OnClick="btnAdicionarArtista_Click" />

        <br />
        <asp:Label ID="LabelError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <h3>Deletar Artista</h3>
            <label for="txtNomeArtista">Nome ou ID do Artista:</label>
            <asp:TextBox ID="txtNomeArtista" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnDeletarArtista" runat="server" Text="Deletar" OnClick="btnDeletarArtista_Click" />
            <br /><br />
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
    </div>

    <div ID="Albuns">
        <h3>Adicionar Álbum</h3>
        <asp:TextBox ID="txtTituloAlbum" runat="server" placeholder="Título do Álbum" />
        <asp:DropDownList ID="ddlArtsAlb" runat="server">
            <asp:ListItem Text="Selecione um Artista" Value="0" />
        </asp:DropDownList>
        <asp:TextBox ID="txtDataLancamentoAlbum" runat="server" onkeyup="formatDate(this);" MaxLength="10" placeholder="Data de Lançamento" />
        <asp:FileUpload ID="FileUploadCapa" runat="server" />

        <asp:Button ID="btnAdicionarAlbum" runat="server" Text="Adicionar Álbum" OnClick="btnAdicionarAlbum_Click" />

        <br />
        <asp:Label ID="LabelErrorAlbum" runat="server" ForeColor="Red" Visible="false"></asp:Label>

    </div>

<div id="Musics">
    <h2>Músicas</h2>
    <h3>Adicionar Música</h3>

    <!-- Campo para o título da música -->
    <asp:TextBox ID="txtTitulo" runat="server" placeholder="Título"></asp:TextBox>

<!-- Campo para selecionar o artista principal -->
<asp:DropDownList ID="ddlArtsMsc" runat="server">
    <asp:ListItem Value="">Selecione um Artista</asp:ListItem>
</asp:DropDownList>

<!-- Campo para selecionar feats (participações especiais) -->
<asp:DropDownList ID="ddlFeats" runat="server">
    <asp:ListItem Value="">Selecione Participações (Feats)</asp:ListItem>
</asp:DropDownList>

    <!-- Campo para data de lançamento -->
    <asp:TextBox ID="txtDataLancamento" runat="server" MaxLength="10" placeholder="Data de Lançamento (AAAA-MM-DD)"></asp:TextBox>

    <!-- Upload de arquivo para a capa da música -->
    <asp:FileUpload ID="FileUpload1" runat="server" />

    <!-- Dropdown para selecionar o álbum ou single -->
    <asp:DropDownList ID="ddlAlbuns" runat="server">
        <asp:ListItem Value="SINGLE">SINGLE</asp:ListItem>
    </asp:DropDownList>

    <!-- Botão para adicionar a música -->
    <asp:Button ID="btnAdicionarMusica" runat="server" Text="Adicionar Música" OnClick="btnAdicionarMusica_Click" />

    <!-- Label para exibir mensagens de erro ou sucesso -->
    <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
</div>


                    

    <div>
        <h3>Deletar Conta</h3>
        <asp:Label ID="Label1" runat="server" Text="" />
        <br />
        <label for="txtUsuarioDelete">Digite o Nome de Usuário:</label>
        <asp:TextBox ID="txtUsuarioDelete" runat="server" />
        <br />
        <asp:Button ID="btnDeletarUsuario" runat="server" Text="Deletar Usuário" OnClick="btnDeletarUsuario_Click" />
    </div>

    <div>
        <h3>Criar Conta</h3>
        <asp:TextBox ID="txtNovoUsuario" runat="server" placeholder="Nome de Usuário" />
        <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password" placeholder="Senha" />
        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" />
        <asp:Button ID="btnCriarConta" runat="server" Text="Criar Conta" OnClick="btnCriarConta_Click" />
        <asp:Label ID="lblMensagemCriar" runat="server" ForeColor="Green" Visible="false"></asp:Label>
    </div>

    <div>
        <asp:DropDownList ID="ddlUsuarios" runat="server"></asp:DropDownList>
        <asp:Button ID="btnPraVirarAdmin" runat="server" Text="Tornar Admin" OnClick="btnPraVirarAdmin_Click" />
        <asp:Label ID="Label2" runat="server" Visible="false" ForeColor="Green"></asp:Label>
    </div>

                                      <script type="text/javascript">
                                          function formatDate(input) {
                                              let value = input.value.replace(/[^0-9]/g, '');
                                              if (value.length > 4) {
                                                  value = value.slice(0, 4) + '-' + value.slice(4);
                                              }
                                              if (value.length > 7) {
                                                  value = value.slice(0, 7) + '-' + value.slice(7);
                                              }
                                              input.value = value;
                                          }
                                       </script>

</asp:Content>
