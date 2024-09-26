<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="_2024_08_22_TuneRate.inicio" %>

﻿<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Homepage</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Julee&display=swap" rel="stylesheet">
    <style>
        body {
            background-color: #000000;
            font-family: 'Julee', cursive;
            margin: 0;
            padding: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
            color: #FFFFFF;
        }

        .container {
            text-align: center;
        }

        .photo {
            width: 498px;
            height: 499px;
            background-color: #FFFFFF;
            border-radius: 50%;
            margin-bottom: 40px;
            background-image: url('imagens/TuneRate.png')
        }

        .buttons {
            display: flex;
            justify-content: center;
            gap: 20px;
        }

        .buttons button {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #FF00FF;
            color: #FFFFFF;
            border-radius: 10px;
            padding: 20px 40px;
            font-size: 24px;
            cursor: pointer;
            transition: background-color 0.3s ease;
            font-family: 'Julee', cursive;
            width: 220px;
        }

        .buttons button:hover {
            background-color: #cc00cc;
        }
    </style>
</head>

<body>
  
        <form id="form1" runat="server">
  
        <div class="photo"></div>
        <div class="buttons">
            <button>Logar</button>
            <button>Cadastrar</button>
        </div>
    </div>
            <p>
                &nbsp;</p>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagens/pppp (1).png" Width="64px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image2" runat="server" ImageUrl="~/imagens/pppp (1).png" Width="62px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image3" runat="server" ImageUrl="~/imagens/pppp (1).png" Width="64px" />
        </form>
</body>

</html>