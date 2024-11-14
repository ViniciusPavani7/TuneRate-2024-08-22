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

        .musica-photo {
            width: 100%;        /* Largura da imagem em 100% do container */
            max-width: 400px;   /* Limita a largura máxima da imagem */
            height: 400px;      /* Altura da imagem */
            object-fit: cover;   /* Ajusta a imagem para preencher o espaço */
            display: block;      /* Torna a imagem um bloco */
            margin: 0 10px 20px; /* Centraliza a imagem e coloca um espaçamento abaixo */
            border-radius: 10px; /* Borda arredondada */
            box-shadow: 0px 4px 6px rgba(169, 169, 169, 0.7); 
        }

        .infoMusic {
            font-size: 30px;
        }

        #lblNomeAlb {
            font-size: 1.5rem;
            margin-bottom: 10px; /* Espaçamento abaixo */
            color: #f1f1f1; /* Cor mais clara para o título */
        }

        #lblAutor, #AnoLancamento {
            font-size: 1.1rem;
            margin-bottom: 8px; /* Adiciona espaçamento entre os itens */
        }

        .divRight {
            height: 100vh; /* 100% da altura da tela */
            width: 70%;
            float: right; /* Para que a div fique à esquerda */
            background-color:  #000000;
            overflow-y: auto; /* Permite rolagem vertical se o conteúdo exceder a altura da tela */
        }

        .CommentsDiv{
            margin-left: 20px;
        }

        .createCommentsArea{
            padding: 15px 0;
        }

        .commentsArea{
            border-top: 1px solid gray;
            padding: 15px 0;
        }

        .nomeMusica {
            font-weight: bold; /* Define o texto como negrito */
            font-size: 2em; /* Ajuste o valor para o tamanho desejado */
        }

        .nomeMusicaDiv{
            padding: 10px 0;
        }

        .txtBoxComentario{
            background-color: #1C1C1C;
            width: 100%;
            padding: 10px;
            height: 100px;
            font-size: 15px;
            color: white;

            border-radius: 10px;
        }

        .btnEnviar{
            font-family: 'Roboto', sans-serif;
            height: 50px;
            width: 140px;
            font-weight: bold;
            font-size: 18px;
            border-radius: 40px;
            background-color: #2e2d2d;
            color: gray;
            margin: 15px 0 0 0;
        }

        .comentario-item {
            background-color: #1C1C1C; /* Fundo do comentário */
            color: #ffffff; /* Cor do texto */
            padding: 15px;
            margin-bottom: 10px;
            border-radius: 5px;
            display: grid;
            grid-template-columns: 100px min-content auto 150px;
            grid-template-rows: auto auto;
        }

        .imgPerfil{
            margin: 0 20px 0 0;
            height: 90px;
            width: 90px;

            grid-row: 1 / span 2; 
            grid-column: 1;
        }

        .userName{
            max-width: 200px;
            margin-right: 15px;
            grid-row: 1;
            grid-column: 2;
            font-size: 1.5em;
        }

        .estrelinha{
            grid-row: 1;
            grid-column: 3;

            font-size: 2em;     /* Ajuste o tamanho das estrelas para um valor adequado */
            display: inline-block;
            line-height: 1; 
            color: purple;       
        }

        .data{
            grid-row: 1;
            grid-column: 4;
            display: flex;
            flex-direction: column;
            justify-content: flex-start; 
            justify-self: end;
        }

        .txtComentario{
            grid-row: 2;
            grid-column: 2 / span 2;
            white-space: normal; 
            word-wrap: break-word; 
            word-break: break-word;
            font-size: 1.1em;
        }

        .btnExcluir{
            grid-row: 2;
            grid-column: 4;
            display: flex;
            justify-content: center; 
            align-items: center; 
            border-radius: 30px;
            height: 40px;
            width: 100px;
            margin-top: auto; 
            font-weight: bold;
            font-size: 15px;
            font-family: 'Roboto', sans-serif;
            color: #ffffff;
            background-color: #e61919;
            justify-self: end;
        }

.star-rating {
    direction: ltr; /* Direção das estrelas da esquerda para a direita */
    display: inline-block;
    font-size: 2em;
}

.star-rating input {
    display: none; /* Esconde o input radio */
}

.star-rating label {
    color: gray; /* Cor inicial das estrelas */
    cursor: pointer;
}

.star-rating label:hover,
.star-rating label:hover ~ label {
    color: purple; /* Cor das estrelas ao passar o mouse */
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
                <div class="infoMusic">
                    <asp:Literal ID="lblNomeAlb" runat="server"></asp:Literal>
                    <br />
                    <asp:Label ID="lblAutor" runat="server" ></asp:Label>
                    <br />
                    <asp:Label ID="AnoLancamento" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div class="divRight">

            <div class="CommentsDiv">

                <div class="createCommentsArea">

                <div class="nomeMusicaDiv">
                    <asp:Label ID="lblNomeMusica" runat="server" CssClass="nomeMusica"></asp:Label>
                </div>

                <div class="star-rating">
                    <asp:RadioButtonList ID="rblRating" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" CssClass="star-rating">
                        <asp:ListItem Value="1" Text="&#9733;" />
                        <asp:ListItem Value="2" Text="&#9733;" />
                        <asp:ListItem Value="3" Text="&#9733;" />
                        <asp:ListItem Value="4" Text="&#9733;" />
                        <asp:ListItem Value="5" Text="&#9733;" />
                    </asp:RadioButtonList>
                </div>

                        <br />

                        <!-- TextBox para Comentário -->
                        <asp:TextBox ID="txtComentario" runat="server" CssClass="txtBoxComentario" MaxLength="1000" TextMode="MultiLine" placeholder="Escreva seu comentário aqui..."  onkeydown="checkEnter(event)"></asp:TextBox>
                        <br />
        
                        <asp:Button ID="btnEnviar" runat="server" CssClass="btnEnviar" Text="Comentar" OnClick="btnEnviar_Click" />

                        <br /><br />

                </div>
                            
                <div class="commentsArea">
                    <asp:Repeater ID="rptComentarios" runat="server" OnItemDataBound="rptComentarios_ItemDataBound" OnItemCommand="rptComentarios_ItemCommand">
                        <ItemTemplate>
                            <div class="comentario-item">
                                <!-- A imagem de perfil -->
                                <asp:Image ID="imgPerfil" runat="server" class="imgPerfil" ImageUrl="~/imgs/unknown.png" />
                
                                <!-- Outros campos do comentário -->
                                <p class="userName"><strong><%# Eval("Usuario") %></strong></p>
                                <p class="estrelinha">
                                    <strong><%# GenerateStarRating(Convert.ToInt32(Eval("Rating"))) %></strong>
                                </p>
                                <p class="data"><small><%# Eval("Data", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                                <p class="txtComentario"><%# Eval("Comentario") %></p>

                                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btnExcluir" CommandName="Excluir" CommandArgument='<%# Eval("CommentID") %>' Visible="false" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>                                    



                  <asp:Label ID="lblMensagemErro" runat="server" ForeColor="Red" Visible="false"></asp:Label>
              </div>

         </div>

    <script type="text/javascript">
        // Função para verificar o Enter no TextBox
        function checkEnter(event) {
            if (event.key === 'Enter') {  // Verifica se a tecla pressionada é 'Enter'
                event.preventDefault();    // Impede que o Enter seja tratado como nova linha (no caso de um TextBox multiline)
                document.getElementById('<%= btnEnviar.ClientID %>').click();  // Dispara o click do botão
            }
        }

        // Adiciona um evento de clique a cada estrela para avaliação
        document.addEventListener('DOMContentLoaded', function () {
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
        });
</script>

</asp:Content>


