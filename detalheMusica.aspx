 <%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="detalheMusica.aspx.cs" Inherits="_2024_08_22_TuneRate.detalheMusica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/raty/2.9.0/jquery.raty.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raty/2.9.0/jquery.raty.min.js"></script>

    <style>
        html, body {
            height: 100%;
            margin: 0;
        }
        /* Estilo para o contêiner principal */

        .divLeft {
            height: 100vh; /* 100% da altura da tela */
            width: 30%; /* 40% da largura da tela */
            float: left; /* Para que a div fique à esquerda */
            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
            border-right: 5px;
        }

        .divRight {
            height: 100vh; /* 100% da altura da tela */
            width: 70%;
            float: right; /* Para que a div fique à esquerda */
            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
        }

        /* Estilo para a imagem do álbum ou música */
        .musica-photo {
            width: 400px;        /* Largura da imagem */
            height: 400px;       /* Altura da imagem */
            object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
            display: block;      /* Torna a imagem um bloco */
            margin: 0 auto;     /* Centraliza horizontalmente */
            margin-top: 30px;
            margin-bottom: 30px;
            border-radius: 3px;
        }

        .CommentsDiv{
            border: 1px solid red;
            
        }

        .nomeMusica {
            font-weight: bold; /* Define o texto como negrito */
            font-size: 1.5em; /* Ajuste o valor para o tamanho desejado */
        }

        /* Estilo para cada item de comentário */
        .comentario-item {
            background-color: #1a1a1a; /* Fundo do comentário */
            color: #ffffff; /* Cor do texto */
            padding: 15px;
            margin-bottom: 10px;
            border-radius: 5px;
        }

  .star-rating {
    direction: rtl; /* Para permitir a seleção da estrela da direita para a esquerda */
    display: inline-block;
    font-size: 2em;
}
.star-rating input {
    display: none; /* Oculta os inputs radio */
}
.star-rating label {
    color: gray; /* Cor inicial */
    cursor: pointer;
}
.star-rating input:checked ~ label {
    color: purple; /* Cor quando selecionada */
}
.star-rating label:hover,
.star-rating label:hover ~ label {
    color: gold; /* Cor ao passar o mouse */
}

.like-button.active {
    color: mediumpurple; /* Cor do coração quando ativo */
    animation: sparkle 0.5s forwards; /* Animação de purpurina */
}

        .text-box {
            width: 400px; /* Largura do TextBox */
            padding: 10px; /* Espaço interno */
            border-radius: 4px; /* Bordas arredondadas */
            border: 1px solid #ccc; /* Borda leve */
            box-sizing: border-box; /* Inclui padding e borda no tamanho total */
            margin: 20px 0; /* Margem superior e inferior */
        }

        /* Estilo para o botão */
        .button {
            margin-top: 10px; /* Margem superior do botão */
        }
    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="divLeft">
            <div class="musicas-details-container">
                <asp:Image ID="fotoMsc" runat="server" CssClass="musica-photo" AlternateText="Foto da Música" />
                <h3>
                    <asp:Literal ID="lblNomeAlb" runat="server"></asp:Literal>
                    <br />
                    <asp:Label ID="lblAutor" runat="server" Text="Autor: "></asp:Label>
                    <br />
                    <asp:Label ID="AnoLancamento" runat="server" Text="Data de Lançamento: "></asp:Label>
                </h3>
            </div>
        </div>

        <div class="divRight">

            <div class="CommentsDiv">

                <asp:Label ID="lblNomeMusica" runat="server" CssClass="nomeMusica"></asp:Label>

                <asp:RadioButtonList ID="rblRating" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                </asp:RadioButtonList>

                        <br />

                        <!-- TextBox para Comentário -->
                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="5" Width="400px" placeholder="Escreva seu comentário aqui..."></asp:TextBox>
                        <br />
        
                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />

                        <br /><br />
                            
                    <div>
                        <asp:Repeater ID="rptComentarios" runat="server" OnItemDataBound="rptComentarios_ItemDataBound" OnItemCommand="rptComentarios_ItemCommand">
                            <ItemTemplate>
                                <div class="comentario-item">
                                    <asp:Image ID="imgPerfil" runat="server" class="imgPerfil" ImageUrl="~/imgs/unknown.png" />
                                    <p><strong><%# Eval("Usuario") %>:</strong> <%# Eval("Comentario") %></p>
                                    <p><small>Data: <%# Eval("Data", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                                    <p><strong>Avaliação: <%# Eval("Rating") %></strong></p>
                                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("CommentID") %>' Visible="false" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>


                  <asp:Label ID="lblMensagemErro" runat="server" ForeColor="Red" Visible="false"></asp:Label>
              </div>

         </div>

    <script>
        // Adiciona um evento de clique a cada estrela
        document.querySelectorAll('.star-rating input').forEach((input) => {
            input.addEventListener('click', function () {
                // Remove a seleção de todas as estrelas
                document.querySelectorAll('.star-rating label').forEach(label => {
                    label.style.color = 'gray'; // Cor inicial
                });

                // Muda a cor das estrelas até a estrela selecionada
                let index = Array.from(document.querySelectorAll('.star-rating input')).indexOf(input);
                for (let i = 0; i <= index; i++) {
                    document.querySelectorAll('.star-rating label')[i].style.color = 'purple'; // Cor quando selecionada
                }
            });
        });
    </script>

</asp:Content>


