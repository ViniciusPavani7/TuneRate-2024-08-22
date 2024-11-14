<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="_2024_08_22_TuneRate.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/5.1.0/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.1.0/js/bootstrap.bundle.min.js"></script>

    <style>
            body {
            padding: 0;
            margin: 0;
            }
            .bodyPage {
            }
            .artTag, .MscTag {
                font-weight: bold;
                font-size: 35px;
                max-height: 30px;
                position:relative;
                left: 180px;
                bottom: 20px;
            }
            .imgIcon1 {
                position: fixed;
                bottom: 47px;
                right: 10px;
                width: 70px;
                height: 180px;
                justify-content: space-between;
            }
            .icon1 {
                height: 70px;
            }
            .artistGrid,  .musicGrid {
                display: flex;
                overflow-x: auto; /* Adiciona rolagem horizontal se os itens ultrapassarem a largura da tela */
                white-space: nowrap; /* Impede que os itens quebrem linha */
                margin: 20px;
                padding-top: 20px;
            }
            .artist-container, .music-container { /* Estilo aplicado para ambos */
                width: 220px; /* Largura de cada artista/música */
                margin: 10px; /* Espaçamento entre os itens */
                margin-top: 30px;
                margin-bottom: 0px;
                text-align: center;
                white-space: normal; /* Permite que o texto dentro quebre linha, se necessário */
            }

            .artist-image, .music-image {
                width: 200px; /* Tamanho da imagem */
                height: 200px; /* Tamanho da imagem */
                border-radius: 50%; /* Imagem circular para artistas */
                object-fit: cover; /* Garantir que a imagem cubra o espaço sem distorção */
            }

            .music-image{
                width: 200px; /* Tamanho da imagem */
                height: 200px; /* Tamanho da imagem */
                border-radius: 2px; 
                object-fit: cover; 
            }

            .music-title, .artist-name {
                margin-top: 5px;
                font-size: 20px;
            }            
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bodyPage">


        <!-- -->
        <div class="artistGrid">
            <asp:Label ID="artTag" runat="server" class="artTag" Text="Artistas"></asp:Label>
            <asp:Repeater ID="ArtistRepeater" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="artist-container">
                        <a href='<%# "detalheArtista.aspx?nome=" + Server.UrlEncode(Eval("Nome").ToString()) %>'>
                            <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("FotoBinario")) %>' class="artist-image" alt='<%# Eval("Nome") %>' />
                        </a>
                        <h5 class="artist-name"><%# Eval("Nome") %></h5>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>


        <!--<div class="imgIcon1">
            <asp:Image ID="Image1" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image2" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image3" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
        </div>--!>

        <!-- -->
        <div class="musicGrid">
    <asp:Label ID="MscTag" runat="server" class="MscTag" Text="Músicas"></asp:Label>
    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
        <ItemTemplate>
            <div class="music-container">
                <!-- Link para detalheMusica.aspx com o título da música na URL -->
                <a href='<%# "detalheMusica.aspx?nome=" + Server.UrlEncode(Eval("Titulo").ToString()) %>'>
                    <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("Capa")) %>' class="music-image" alt='<%# Eval("Titulo") %>' />
                    <h5 class="music-title"><%# Eval("Titulo") %></h5>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Nome, FotoBinario FROM Artistas">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Titulo, Capa FROM Musicas">
    </asp:SqlDataSource>



</asp:Content>


