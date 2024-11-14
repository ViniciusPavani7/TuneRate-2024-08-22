<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="albuns.aspx.cs" Inherits="_2024_08_22_TuneRate.albuns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

    body, html{
        padding: 0;
        margin: 0;
    }

    .backgroudNavOption2{
    display: flex;
    justify-content: center;  
    align-items: center; 
    height: 100px;
    background-color: #2b2b2b;
    
    }

    .albTag {
        display: flex; 
        flex-direction: column;
        font-weight: bold;
        font-size: 35px;
        max-height: 30px;
        text-align: center;
    }

    .albumGrid {
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

    .album-container{ /* Estilo aplicado para ambos */
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

    .album-image {
        width: 200px; /* Tamanho da imagem */
        height: 200px; /* Tamanho da imagem */
        border-radius: 10px; /* Imagem circular para artistas */
        object-fit: cover; /* Garantir que a imagem cubra o espaço sem distorção */
    }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="albumGrid">
   <asp:Label ID="albTag" runat="server" CssClass="albTag" Text="Álbuns"></asp:Label>
       <div class="repeater-div">
       <asp:Repeater ID="AlbumRepeater" runat="server" DataSourceID="SqlDataSource1">

           <ItemTemplate>

               <div class="album-container">

                   <a href='<%# "detalheAlbum.aspx?nome=" + Server.UrlEncode(Eval("Titulo").ToString()) %>'>
                       <img src='data:image/png;base64,<%# Convert.ToBase64String((byte[])Eval("CapaBinaria")) %>' class="album-image" alt='<%# Eval("Titulo").ToString() %>' />
                   </a>
                   <h5 class="artist-name"><%# Eval("Titulo").ToString() %></h5>

               </div>

           </ItemTemplate>

       </asp:Repeater>
       </div>
   </div>
  
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Titulo, CapaBinaria FROM Albuns">
    </asp:SqlDataSource>

</asp:Content>