<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Faça sua resenha de "SuperCut!"</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            overflow: hidden;
            background: url('imagens/supercut.png') no-repeat center center fixed;
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

        .resenha-input {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            width: 5000px;
            height: 350px;
            max-width: 400px;
            padding: 10px;
            background-color: rgba(0, 0, 0, 0.5); /* Fundo preto transparente */
            border-radius: 5px;
            color: white;
            font-size: 16px;
            box-sizing: border-box;
            margin-top: 2px;
        }

        .resenha-input::placeholder {
            color: rgba(255, 255, 255, 0.7); /* Placeholder branco transparente */
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
                <img src="imagens/lorde.jfif" alt="Imagem Exemplo"> <!-- Substitua pelo caminho da sua imagem -->
                <div class="text-container">
                    <h3>SuperCut</h3>
                    <p>
                      
                    </p>
                    //estrelas aqui
                    

                <input type="text" class="resenha-input" placeholder="Digite sua resenha aqui:" />
            </div>
        </div>
    </form>
</body>

</html>