<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="detalheAlbum.aspx.cs" Inherits="_2024_08_22_TuneRate.detalheAlbum" %>
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

        .infoAlb {
            font-size: 30px;
        }

        #lblNomeAlb {
            font-size: 1.5rem;
            font-weight: bold;
            margin-bottom: 10px; /* Espaçamento abaixo */
            color: #f1f1f1; /* Cor mais clara para o título */
        }

        #lblAutor, #AnoLancamento {
            font-size: 1.1rem;
            color: #ccc; /* Cor suave para texto secundário */
            margin-bottom: 8px; /* Adiciona espaçamento entre os itens */
        }
        
        .album-photo {
            width: 100%;        /* Largura da imagem em 100% do container */
            max-width: 400px;   /* Limita a largura máxima da imagem */
            height: 400px;      /* Altura da imagem */
            object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
            display: block;      /* Torna a imagem um bloco */
            margin: 0 10px 20px; /* Centraliza a imagem e coloca um espaçamento abaixo */
            border-radius: 10px; /* Borda arredondada */
            box-shadow: 0px 4px 6px rgba(169, 169, 169, 0.7); 
        }

        .divRight {
            height: 100vh; /* 100% da altura da tela */
            width: 70%;
            float: right; /* Para que a div fique à esquerda */
            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
        }

        .musicDiv{
            padding: 10px; 
            margin: 5px 0;
        }

        .gvMusicasSemBorda {
            width: 100%; /* Largura total */

        }
    
        .musica-container {
            padding: 10px; /* Espaçamento interno */
            margin: 10px 0; /* Espaçamento entre retângulos */
        }

        .nome-musica {
            font-size: 1.5em; /* Tamanho do texto para o nome da música */
            font-weight: normal; /* Remove o negrito */
            margin: 0; /* Remove margem padrão */
        }

        .artista {
            color: #999; /* Cor mais clara para o artista */
            font-size: 0.9em; /* Tamanho menor para disfarçar */
            margin-top: 5px; /* Espaço acima */
        }

        .contribuintes {
            color: #999; /* Cor mais clara para os contribuentes */
            font-size: 0.9em; /* Tamanho menor para disfarçar */
            margin-top: 5px; /* Espaço acima */
        }

        .gvMusicasSemBorda {
            border: none;
            border-collapse: collapse;
            width: 100%;
        }

        .gvMusicasSemBorda td,
        .gvMusicasSemBorda th,
        .gvMusicasSemBorda tr {
            border: none;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divLeft">
        <div class="albuns-details-container">
            <asp:Image ID="fotoAlb" runat="server" CssClass="album-photo" AlternateText="Foto do Álbum" />
            <div class="infoAlb">
                <asp:Label ID="lblNomeAlb" runat="server" Text="Álbum: "></asp:Label>
                <br />
                <asp:Label ID="lblAutor" runat="server" Text="Autor: "></asp:Label>
                <br />
                <asp:Label ID="AnoLancamento" runat="server" Text="Data de Lançamento: "></asp:Label>
                <br />
                <br />

                <!-- Link para Wikipedia -->
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    <div class="divRight">

        <asp:GridView 
            ID="gvMusicas" 
            runat="server" 
            AutoGenerateColumns="false" 
            CssClass="gvMusicasSemBorda" 
            GridLines="None" 
            BorderStyle="None" 
            CellPadding="0" 
            CellSpacing="0"
            OnRowCommand="gvMusicas_RowCommand">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="musicDiv">

                            <h3 style="margin: 0; font-weight: normal;">
                                <asp:LinkButton 
                                    ID="lnkTituloMusica" 
                                    runat="server" 
                                    CommandName="SelecionarMusica" 
                                    CommandArgument='<%# Eval("Titulo") %>'>
                                    <%# Eval("Titulo") %>
                                </asp:LinkButton>
                            </h3>

                            <div style="font-size: 0.9em; color: #777;">
                                <%# Eval("ArtistaOuFeats") %>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </div>

</asp:Content>

