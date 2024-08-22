<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Happier Than Ever Resenha</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            overflow: hidden;
            background: url('imagens/happier.png') no-repeat center center fixed;
            background-size: cover;
            font-family: 'Julee', cursive; /* Usa a fonte Julee */
        }

        .header {
            position: absolute;
            top: 20px; /* Ajusta a distância do topo */
            left: 20px; /* Ajusta a distância da esquerda */
            display: flex;
            align-items: center;
        }

        .header img {
            height: 60px; /* Ajusta a altura da logo */
            width: 60px; /* Ajusta a largura da logo */
            margin-right: 10px; /* Espaço entre a logo e o texto */
        }

        .header h1 {
            margin: 0;
            font-size: 40px;
            color: #A458DC;
        }

        .button-container {
            position: absolute;
            top: 20px; /* Ajusta a distância do topo */
            right: 20px; /* Ajusta a distância da direita */
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .button-container img {
            width: 50px; /* Ajusta a largura da imagem do botão */
            height: 50px; /* Ajusta a altura da imagem do botão */
            border-style: none;
            border-color: inherit;
            border-width: medium;
            cursor: pointer;
        }

        .button-container p {
            margin: 5px 0 0 0;
            color: #FFFFFF;
            font-size: 14px;
            text-align: center;
        }

        .content {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100%;
            color: #FFFFFF;
            text-align: left;
            padding: 20px; /* Espaço ao redor do conteúdo */
        }

        .content .image-container {
            display: flex;
            align-items: flex-start;
            margin-top: 60px; /* Espaço acima da imagem e título */
            margin-bottom: 20px; /* Espaço abaixo da imagem e título */
        }

        .content .image-container img {
            /* Ajusta o tamanho da imagem */
            /* Ajusta o tamanho da imagem */
            margin-right: 20px; /* Espaço entre a imagem e o texto */
        }

        .content .image-container .text-container {
            display: flex;
            flex-direction: column;
        }

        .content .image-container h3 {
            font-size: 2.5em;
            margin: 0;
            color: #FFFFFF;
        }

        .content .text-container p {
            font-size: 18px;
            margin-top: 20px; /* Espaço acima do texto */
        }
    </style>

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Julee&display=swap" rel="stylesheet">
</head>

<body>
    <form id="form1" runat="server">
        <div class="header">
            <img src="imagens/TuneRate.png" alt="Logo"> <!-- Substitua pelo caminho da sua logo -->
            <h1>TuneRate</h1> <!-- Texto ao lado da logo -->
        </div>

        <div class="button-container">
            <asp:Image ID="Image1" runat="server" Height="62px" ImageUrl="~/imagens/logo1.png" Width="62px" />
            <p style="font-size:15px">Meu Perfil</p> <!-- Texto abaixo da imagem do botão -->
        </div>

        <div class="content">
            <br /> 
            <div class="image-container">
                <img src="imagens/thanever.jfif" alt="Imagem Exemplo"> <!-- Substitua pelo caminho da sua imagem -->
                <div class="text-container">
                    <h3>Happier Than Ever</h3>
                    <p>
                        <asp:Image ID="Image2" runat="server" Height="110px" ImageUrl="~/imagens/estrela-removebg-preview.png" Width="593px" />
                    </p>

                    

                    <p>A canção começa de forma suave e introspectiva, com uma melodia delicada e </p>
                    <p>minimalista que destaca a vulnerabilidade da letra. Eilish canta sobre a dor e a</p>
                    <p>desilusão em um relacionamento, onde seu sofrimento é velado pela calma da melodia.</p>
                    <p>As letras expressam sentimentos de frustração e desencanto, abordando temas de manipulação</p>
                    <p>emocional e a luta para encontrar a felicidade em meio ao caos.</p>
                </div>
            </div>
        </div>
    </form>
</body>

</html>
