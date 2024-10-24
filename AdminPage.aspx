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

    <div id="Musics">
        <h2>Músicas</h2>
        <h3>Adicionar Música</h3>
         <asp:TextBox ID="txtMscTitle" runat="server" placeholder="Titulo" />
        <asp:TextBox ID="txtMscAuthor" runat="server" placeholder="Autor" />
        <asp:TextBox ID="txtMscFeats" runat="server" placeholder="Feats." />
        <asp:TextBox ID="txtMscGenre" runat="server" placeholder="Genero Musical"></asp:TextBox>

        
                <asp:TextBox ID="txtMscDate" runat="server" onkeyup="formatDate(this);" MaxLength="10" />

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
        

        <asp:FileUpload ID="ImageMscUpload" runat="server" />
        <asp:Button ID="btnAddMusic" runat="server" Text="Adicionar Música" OnClick="btnAdicionarMusic_Click" />

        <br />
        <asp:Label ID="lblErrorAdd" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <h3>Deletar Música</h3>
            <label for="txtMscName">Nome ou ID, Nome da musica ou Artista:</label>
            <asp:TextBox ID="txtDelete" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnMscDelete" runat="server" Text="Deletar" OnClick="btnDeletarMusic_Click" />
            <br /><br />
            <asp:Label ID="lblErrorDelete" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
