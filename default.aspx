<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="_2024_08_22_TuneRate.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       body{
            padding: 0;
            margin: 0;
        }

       .bodyPage {
           
        }

       .artsTag {
            font-weight: bold;
            font-size: 25px;
        }

       .mscsTag {
            font-weight: bold;
            font-size: 25px;
        }

       .artistDiv {
            position: absolute;
            left: 7%;
            right:7%;
            top: 25%;
            height: auto;

        }

       .musicDiv {
            position: absolute; 
            left: 7%; 
            right: 7%;
            top: 60%; 
            height: auto;
        }

       

        .artistDiv2, .musicDiv2 {
            overflow: hidden;
        }

        .carousel-inner {
            display: flex;
            overflow: hidden;
        }

        .carousel-item {
            display: flex;
            flex: 0 0 auto;
            width: 100%;
        }

        .carousel-container {
            display: flex;
            flex-direction: row;
            gap: 10px;
        }

        .indArtist, .indMusic {
            flex: 0 0 auto;
            width: 300px; /* Ajuste conforme necessário */
        }

        .carousel-control-prev-icon, .carousel-control-next-icon {
            background-color: #000;
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
    </style>

    <script>
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bodyPage">
        <div class="artistDiv">
            <asp:Label ID="artTag" runat="server" class="artsTag" Text="Artistas"></asp:Label>
            <div class="artistDiv2">
                <div id="artistCarousel" class="carousel slide" data-bs-ride="carousel">
                    <!-- Indicadores do Carrossel -->
                    <div class="carousel-indicators">
                        <button type="button" data-bs-target="#artistCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                        <button type="button" data-bs-target="#artistCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                        <button type="button" data-bs-target="#artistCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TuneRate %>" SelectCommand="SELECT * FROM USERS
"></asp:SqlDataSource>
                    </div>

                    <!-- Slides do Carrossel -->
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <div class="carousel-container">
                                <div class="indArtist">
                                    <img src="image1.jpg" class="d-block w-100" alt="Artista 1">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Artista 1</h5>
                                        <p>Descrição do Artista 1</p>
                                    </div>
                                </div>
                                <div class="indArtist">
                                    <img src="image2.jpg" class="d-block w-100" alt="Artista 2">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Artista 2</h5>
                                        <p>Descrição do Artista 2</p>
                                    </div>
                                </div>
                                <div class="indArtist">
                                    <img src="image3.jpg" class="d-block w-100" alt="Artista 3">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Artista 3</h5>
                                        <p>Descrição do Artista 3</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Controles de Navegação -->
                    <button class="carousel-control-prev" type="button" data-bs-target="#artistCarousel" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#artistCarousel" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>
        </div>

        <div class="musicDiv">
            <asp:Label ID="mscTag" runat="server" class="mscsTag" Text="Músicas"></asp:Label>
            <div class="musicDiv2">
                <div id="musicCarousel" class="carousel slide" data-bs-ride="carousel">
                    <!-- Indicadores do Carrossel -->
                    <div class="carousel-indicators">
                        <button type="button" data-bs-target="#musicCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                        <button type="button" data-bs-target="#musicCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                        <button type="button" data-bs-target="#musicCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                    </div>

                    <!-- Slides do Carrossel -->
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <div class="carousel-container">
                                <div class="indMusic">
                                    <img src="music1.jpg" class="d-block w-100" alt="Música 1">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Música 1</h5>
                                        <p>Descrição da Música 1</p>
                                    </div>
                                </div>
                                <div class="indMusic">
                                    <img src="music2.jpg" class="d-block w-100" alt="Música 2">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Música 2</h5>
                                        <p>Descrição da Música 2</p>
                                    </div>
                                </div>
                                <div class="indMusic">
                                    <img src="music3.jpg" class="d-block w-100" alt="Música 3">
                                    <div class="carousel-caption d-none d-md-block">
                                        <h5>Música 3</h5>
                                        <p>Descrição da Música 3</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Controles de Navegação -->
                    <button class="carousel-control-prev" type="button" data-bs-target="#musicCarousel" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#musicCarousel" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>
        </div>

        <div class="imgIcon1">
            <asp:Image ID="Image1" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image2" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image3" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
        </div>
    </div>
</asp:Content>

