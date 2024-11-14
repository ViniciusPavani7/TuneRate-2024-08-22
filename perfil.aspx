<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="_2024_08_22_TuneRate.perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        body, html{
            padding: 0;
            margin: 0;
            border: 0;
        }

        .imgPerfilPerfil{
            width: 200px;
            height: 200px;
            border-radius: 50%;
        }

        .perfilDiv{

            margin: 0 10% 0 10%;
            display: grid;
            grid-template-columns: repeat(1, auto);
        }

        .infoPerfil{
            max-height: 1000px;
            margin: 50px 0;
        }

        .alterarImgPerfilDiv {
            background-color: #1C1C1C;
            width: 40vh;
            padding: 10px;
            border-radius: 15px;

            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        .divPerfilPerfil {
    display: grid;
    grid-template-columns: 200px min-content; /* Primeira coluna para a imagem, segunda coluna para as informações */
    grid-template-rows: auto auto; /* Linhas automáticas para organizar os textos */
    gap: 30px;
    align-items: center; /* Centraliza verticalmente os elementos */
}

.imgPerfilPerfil {
    grid-row: 1 / span 2; /* Faz a imagem ocupar duas linhas na primeira coluna */
    height: 200px;
    width: 200px;
    border-radius: 50%; /* Torna a imagem circular */
}

.userNameLabel {
    grid-column: 2; /* Coloca o nome de usuário na segunda coluna */
    grid-row: 1; /* Primeira linha da segunda coluna */
}

.emailLabel {
    grid-column: 3; /* Coloca o email na segunda coluna */
    grid-row: 1; /* Segunda linha da segunda coluna */
}

.emailLabel, .userNameLabel{
    font-size: 25px;
}

.btnAlterarFoto {
background-color: transparent; /* Sem fundo */
border: none;
color: #ffffff; /* Cor do texto */
padding: 10px 15px;
width: 100%; /* Ocupa toda a largura disponível */
cursor: pointer;
transition: all 0.3s ease; /* Transição suave para hover */
}

        /* Efeito de hover no botão */
        .btnAlterarFoto:hover {
            background-color: #333; /* Fundo escuro quando passar o mouse */
        }

        /* Estilo para o Button de Deletar Foto (com margem adicional) */
        .btnDeletarFoto {
            background-color: transparent; /* Sem fundo */
            border-top: 2px solid #ffffff; /* Borda superior */
            border-bottom: 2px solid #ffffff; /* Borda inferior */
            color: #ffffff; /* Cor do texto */
            padding: 10px 15px;
            width: 100%; /* Ocupa toda a largura disponível */
            cursor: pointer;
            margin-top: 20px; /* Distância maior para descer mais */
            transition: all 0.3s ease; /* Transição suave para hover */
            border: none;
        }

        /* Efeito de hover no botão */
        .btnDeletarFoto:hover {
            background-color: #333; /* Fundo escuro quando passar o mouse */
        }

        .file-upload {
            display: none;
        }

        /* Estilizando o botão customizado */
        .custom-button {
            background-color: transparent;
            border: 2px solid #ffffff;
            color: #ffffff;
            padding: 10px 15px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .custom-button:hover {
            background-color: #333;
        }

        /* Estilizando o nome do arquivo */
        .file-name {
            color: #fff;
            margin-left: 10px;
        }

        .commentsArea{
            border-top: 1px solid gray;
            padding-top: 30px;
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

        .comentario-item:hover {
            transform: scale(1.05); /* Aumenta em 5% */
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

        .grid-container{
            display: grid;
            grid-auto-rows: auto auto;

            height: 100vh;
            width: 100%;
        }


    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="grid-container">
            <div class="perfilDiv">

                <div class="infoPerfil">
                    <div class="divPerfilPerfil">
                        <asp:LinkButton ID="ShowMenu" runat="server" OnClientClick="mostrarDiv(event); return false;">
                            <asp:Image ID="imgPerfil" runat="server" class="imgPerfilPerfil" ImageUrl="~/imgs/unknown.png" />
                        </asp:LinkButton>
                        <asp:Label ID="lblUserName" runat="server" CssClass="userNameLabel"></asp:Label>
                        <asp:Label ID="lblUserEmail" runat="server" CssClass="emailLabel"></asp:Label>
                    </div>
                </div>
        
            </div>

             <div class="commentsArea">
                 <asp:Repeater ID="rptComentarios" runat="server" OnItemDataBound="rptComentarios_ItemDataBound">
                     <ItemTemplate>
                         <div class="comentario-item" onclick="window.location.href='<%# "detalheMusica.aspx?nome=" + Server.UrlEncode(Eval("Titulo").ToString()) %>';">
                            
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
      </div>


    <div id="actShowMenu" style="display:none;">
        <asp:Panel ID="panelUpload" runat="server">
            <div ID="altImgPerfil" runat="server" class="alterarImgPerfilDiv">
                <asp:FileUpload ID="FileUploadImagem" runat="server" CssClass="FileUpload"/>
                <asp:Button ID="Button1" runat="server" Text="Alterar Foto de Perfil" OnClick="btnAlterarFoto_Click" CssClass="btnAlterarFoto"/>


                <asp:Button ID="btnDeletarFoto" runat="server" Text="Deletar Foto de Perfil" OnClick="btnDeletarFoto_Click" CssClass="btnDeletarFoto" />
            </div>
         </asp:Panel>         
         
    </div>

<script>
    // Função para mostrar a div quando o botão for clicado
    function mostrarDiv(event) {
        // Evita que o clique no botão seja capturado pelo evento de clique no documento
        event.stopPropagation();
        document.getElementById('actShowMenu').style.display = 'block'; // Torna a div visível
    }

    // Função para esconder as divs quando clicar fora delas
    document.addEventListener('click', function (event) {
        var divActShowMenu = document.getElementById('actShowMenu');
        var button = document.getElementById('ShowMenu');

        // Verifica se o clique foi fora da div 'actShowMenu' e do botão
        if (!divActShowMenu.contains(event.target) && event.target !== button) {
            divActShowMenu.style.display = 'none'; // Esconde a div
        }
    });
</script>

</asp:Content>

