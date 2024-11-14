<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="artistas.aspx.cs" Inherits="_2024_08_22_TuneRate.artistas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        body, html{
            padding: 0;
            margin: 0;
        }

        .backgroudNavOption3{
            display: flex;
            justify-content: center;  
            align-items: center; 
            height: 100px;
            background-color: #2b2b2b;
            
        }

        .artTag {
                display: flex; 
                flex-direction: column;
                font-weight: bold;
                font-size: 35px;
                max-height: 30px;
                text-align: center;

        }

        .artistGrid {
                display: flex;
                flex-direction: column;
                padding-top: 20px;
                justify-content: space-between;
        }

        .repeater-div {
            display: grid;
            grid-template-columns: repeat(6, auto);
            justify-content: center;
        }

            .artist-container{ /* Estilo aplicado para ambos */
              border: 1px solid rgba(0, 0, 0, 0.8);
              padding: 20px;
              font-size: 30px;
              text-align: center;
              width: 245px;
              margin: 10px;
              text-align: center;

                text-align: center;
                white-space: normal; /* Permite que o texto dentro quebre linha, se necessário */

            }

            .artist-image, .music-image {
                width: 200px; /* Tamanho da imagem */
                height: 200px; /* Tamanho da imagem */
                border-radius: 50%; /* Imagem circular para artistas */
                object-fit: cover; /* Garantir que a imagem cubra o espaço sem distorção */

                margin-bottom: 15px;
          }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Nome, FotoBinario FROM Artistas">
    </asp:SqlDataSource>
     
</asp:Content>