<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="_2024_08_22_TuneRate.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/5.1.0/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.1.0/js/bootstrap.bundle.min.js"></script>

<style>

            * {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
            }

            body {
            padding: 0;
            margin: 0;

            }
            .bodyPage {
                padding: 20px;
                margin: 0;
            }

            .welcome-section {
                text-align: center;
                padding: 200px 20px 305px;
                color: #A458DC;
                font-family: 'Julee', cursive;
                font-size: 30px;
            }

            .welcome-section h1 {
                font-size: 48px;
                margin-bottom: 10px;
            }

            .welcome-section p {
                font-size: 20px;
                color: #ffffff;
                max-width: 800px;
                margin: 0 auto;
            }


            .artTag, .MscTag {
                display: flex; 
                flex-direction: column;

                font-weight: bold;
                font-size: 35px;
                max-height: 30px;
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

            .conteudos-database{
                display: flex;
                flex-direction: column;
                justify-content: space-between;
                gap: 10px; /* Define o espaço entre os elementos */
            }

            .artistGrid, .musicGrid {
                display: flex;
                flex-direction: column;
                overflow-x: auto; 
                white-space: nowrap; 
                padding-top: 20px;
                justify-content: flex-start;


                height: 330px;
                border: 0;
                margin: 0;
            }

            .repeater-div{
                display:flex;
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
            
        .about-section {
            padding: 200px 20px 50px;
            color: #ffffff;
            font-family: 'Julee', cursive;
            text-align: center;
        }

        .about-section h2 {
            font-size: 36px;
            color: #A458DC;
            margin-bottom: 20px;
        }

        .about-section p {
            font-size: 18px;
            max-width: 800px;
            margin: 0 auto 20px;
        }

        .social-links {
            list-style: none;
            padding: 0;
            display: flex;
            justify-content: center;
        }

        .social-links li {
            margin: 0 10px;
        }

        .social-links a {
            color: #CF51CA;
            text-decoration: none;
            font-size: 18px;
        }

        .social-links a:hover {
            color: #ffffff;
        }
            
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bodyPage">

        <div class="bem-vindoDiv">
            <section class="welcome-section">
                <h1>Bem-vindo ao TuneRate!</h1>
                <p>Descubra e compartilhe suas músicas favoritas. Avalie álbuns, conheça artistas e explore novos sons.</p>
                <p>Aqui, a música está em primeiro lugar!</p>
            </section>
        </div>

        <div class="conteudos-database">


            <div class="artistGrid">
                <asp:Label ID="artTag" runat="server" CssClass="artTag" Text="Artistas"></asp:Label>
                    <div class="repeater-div">
                    <asp:Repeater ID="ArtistRepeater" runat="server" DataSourceID="SqlDataSource1">

                        <ItemTemplate>

                            <div class="artist-container">

                                <a href='<%# "detalheArtista.aspx?nome=" + Server.UrlEncode(Eval("Nome").ToString()) %>'>
                                    <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("FotoBinario")) %>' class="artist-image" alt='<%# Eval("Nome").ToString() %>' />
                                </a>
                                <h5 class="artist-name"><%# Eval("Nome").ToString() %></h5>

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>
                    </div>
                </div>


            <asp:Label ID="Label1" runat="server" CssClass="MscTag" Text="Músicas"></asp:Label>

                <div class="musicGrid">
                            <div class="repeater-div">
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
                                    <ItemTemplate>
                                        <div class="music-container">
                                            <a href='<%# "detalheMusica.aspx?nome=" + Server.UrlEncode(Eval("Titulo").ToString()) %>'>
                                                <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("Capa")) %>' class="music-image" alt='<%# Eval("Titulo") %>' />
                                                <h5 class="music-title"><%# Eval("Titulo") %></h5>
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                           </div>

                </div>


            </div>

                <div>
                    <section id="about" class="about-section">
                        <h2>Sobre Nós</h2>
                        <p>O TuneRate é um espaço para amantes da música. Criamos essa plataforma para quem quer explorar novos artistas, compartilhar opiniões e se conectar através da música.</p>

                        <h3>Contatos</h3>
                        <p>Email: contato@tunerate.com</p>
                        <p>Telefone: (11) 1234-5678</p>

                        <h3>Redes Sociais</h3>
                        <ul class="social-links">
                            <li><a href="#">Instagram</a></li>
                            <li><a href="#">Facebook</a></li>
                            <li><a href="#">Twitter</a></li>
                        </ul>
                    </section>
                </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Nome, FotoBinario FROM Artistas ORDER BY NEWID()">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Titulo, Capa FROM Musicas ORDER BY NEWID()">
    </asp:SqlDataSource>



</asp:Content>


