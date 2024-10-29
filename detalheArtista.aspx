﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="detalheArtista.aspx.cs" Inherits="_2024_08_22_TuneRate.Detalhes.detalheArtista" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

html, body {
    height: 100%; /* Para garantir que o corpo da página ocupe 100% da altura da tela */
    margin: 0; /* Remove margens padrão do corpo */
    
}

.divLeft {
    height: 100vh; /* 100% da altura da tela */
    width: 30%; /* 40% da largura da tela */
    float: left; /* Para que a div fique à esquerda */
    background-color:  #000000;
    overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
 border-right: 5px solid gray;
}

.artist-photo {
    width: 300px;        /* Largura da imagem */
    height: 300px;       /* Altura da imagem */
    object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
    display: block;      /* Torna a imagem um bloco */
    margin: 0 auto;     /* Centraliza horizontalmente */
    border-radius: 100%;
}

.divRight {
    height: 100vh; /* 100% da altura da tela */
    width: 70%;
    float: right; /* Para que a div fique à esquerda */
    background-color:  #000000;
    overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
}


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="divLeft">
        <div class="artist-details-container">
            <h1><asp:Label ID="lblNomeArt" runat="server" Text="Nome do Artista"></asp:Label></h1>
            <asp:Image ID="fotoArt" runat="server" CssClass="artist-photo" AlternateText="Foto do Artista" />
            <p><strong>Nacionalidade:</strong> <asp:Label ID="lblNation" runat="server"></asp:Label></p>

            <p><strong>Mais informações:</strong> 
                <asp:Literal ID="wikiLink" runat="server"></asp:Literal>
            </p>
        </div>
    </div>



    <div class="albumGrid">
        <asp:Repeater ID="AlbumRepeater" runat="server" DataSourceID="SqlDataSourceAlbuns">
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

    <asp:SqlDataSource ID="SqlDataSourceAlbuns" runat="server"
        ConnectionString="<%$ ConnectionStrings:TuneRate %>"
        SelectCommand="SELECT Titulo, CapaBinaria FROM Albuns INNER JOIN Artistas ON Albuns.ArtistaId = Artistas.ArtistaID WHERE Artistas.Nome = @Nome">
        <SelectParameters>
            <asp:QueryStringParameter Name="Nome" QueryStringField="nome" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>

