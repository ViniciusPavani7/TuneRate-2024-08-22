<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="detalheArtista.aspx.cs" Inherits="_2024_08_22_TuneRate.Detalhes.detalheArtista" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

html, body {
    height: 100%; /* Para garantir que o corpo da página ocupe 100% da altura da tela */
    margin: 0; /* Remove margens padrão do corpo */
    
}

    .divLeft {
            height: 100vh; /* 100% da altura da tela */
            width: 30%; /* 30% da largura da tela */
            float: left; /* Para que a div fique à esquerda */
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
            border-right: 3px solid #333; /* Borda direita para dividir */
            padding: 20px; /* Adiciona um pouco de espaçamento interno */
            box-sizing: border-box; /* Para que o padding seja incluído no cálculo da largura */
            color: #fff; /* Cor do texto */

     
    }

    .artist-details-container {
            display: grid;
            grid-template-rows: auto 400px auto;
            grid-template-columns: auto;
            gap: 15px;
    }

    .artist-photo {
            max-width: 400px;   /* Limita a largura máxima da imagem */
            height: 350px;      /* Altura da imagem */
            object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
            display: block;      /* Torna a imagem um bloco */
            margin: 0 10px 20px; /* Centraliza a imagem e coloca um espaçamento abaixo */
            border-radius: 50%; /* Borda arredondada */
            box-shadow: 0px 4px 6px rgba(169, 169, 169, 0.7); 
            place-self: center;

            grid-row: 2;
    }

    .lblNomeArt {
        font-size: 30px;
        font-weight: bold;
        margin-bottom: 10px; /* Espaçamento abaixo */
        color: #f1f1f1; /* Cor mais clara para o título */
        text-align: center;

        grid-row: 1;
    }

    .infoDiv {
        font-size: 30px;
        grid-row: 3;
    }

.divRight {
            height: 100vh; /* 100% da altura da tela */
            max-width: 100%;

            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
            padding-left: 50px;
            padding-top: 50px;

            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 15px;
}


.album-container{
    text-align: center;
    width: 250px;
    height: 274px;
}

.album-image{
    width: 250px;
    height: 250px;
    border-radius: 15px;
}



    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>

        <div class="divLeft">
            <div class="artist-details-container">
                <asp:Image ID="fotoArt" runat="server" CssClass="artist-photo" AlternateText="Foto do Artista" />
                <asp:Label ID="lblNomeArt" runat="server" Text="Nome do Artista" CssClass="lblNomeArt"></asp:Label>

                <div class="infoDiv">
                    <p><strong>Nacionalidade:</strong> <asp:Label ID="lblNation" runat="server"></asp:Label></p>

                    <p><strong>Mais informações:</strong> 
                        <asp:Literal ID="wikiLink" runat="server"></asp:Literal>
                    </p>
                </div>
            </div>
        </div>



        <div class="divRight">
            <asp:Repeater ID="AlbumRepeater" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="album-container">
                         <a href='<%# "detalheAlbum.aspx?nome=" + Server.UrlEncode(Eval("Titulo").ToString()) %>'>
                        <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("CapaBinaria")) %>' class="album-image" alt='<%# Eval("Titulo") %>' />
                         </a>
                        <h5 class="album-title"><%# Eval("Titulo") %></h5>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:TuneRate %>"
        SelectCommand="SELECT Titulo, CapaBinaria FROM Albuns INNER JOIN Artistas ON Albuns.ArtistaId = Artistas.ArtistaID WHERE Artistas.Nome = @Nome">
        <SelectParameters>
            <asp:QueryStringParameter Name="Nome" QueryStringField="nome" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>

