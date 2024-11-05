<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="detalheAlbum.aspx.cs" Inherits="_2024_08_22_TuneRate.detalheAlbum" %>
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

        .divRight {
            height: 100vh; /* 100% da altura da tela */
            width: 70%;
            float: right; /* Para que a div fique à esquerda */
            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
        }

        .album-photo {
            width: 400px;        /* Largura da imagem */
            height: 400px;       /* Altura da imagem */
            object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
            display: block;      /* Torna a imagem um bloco */
            margin: 0 auto;     /* Centraliza horizontalmente */
            margin-top: 30px;
            margin-bottom: 30px;
            border-radius: 3px;
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
            <h3>
                <!-- Nome do Álbum -->
                <asp:Label ID="lblNomeAlb" runat="server" Text="Álbum: "></asp:Label>
                <br />

                <!-- Autor com link para a página do artista -->
                <asp:Label ID="lblAutor" runat="server" Text="Autor: "></asp:Label>
                <br />
            
                <!-- Ano de Lançamento -->
                <asp:Label ID="AnoLancamento" runat="server" Text="Data de Lançamento: "></asp:Label>
            </h3>

            <!-- Link para Wikipedia -->
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
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
                <div style="padding: 10px; margin: 5px 0;">
                    <!-- Nome da música como LinkButton -->
                    <h3 style="margin: 0; font-weight: normal;">
                        <asp:LinkButton 
                            ID="lnkTituloMusica" 
                            runat="server" 
                            CommandName="SelecionarMusica" 
                            CommandArgument='<%# Eval("Titulo") %>'>
                            <%# Eval("Titulo") %>
                        </asp:LinkButton>
                    </h3>

                    <!-- Artista e Feats (texto discreto) -->
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

