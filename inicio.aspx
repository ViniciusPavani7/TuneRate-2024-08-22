<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="_08_22_24_ratetune.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Julee&display=swap" rel="stylesheet"> />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-color: #000000">
    <form id="form1" runat="server">
        <div>
           <asp:Button ID="Button1" runat="server" Text="Logar"
    style="color: #FFFFFF; background-color: #FF00FF; 
    height: 62px; width: 180px; border-radius: 50px; float: right; font-size:20px; font-family:'Julee', cursive;" />

<asp:Button ID="Cadastrar" runat="server" Text="Cadastrar"
    style="color: #FFFFFF; background-color: #FF00FF; 
    height: 63px; width: 180px; border-radius: 50px; float: right; margin-right: 10px; font-size:20px; font-family:'Julee', cursive;" />
 " />
        </div>
        <ul>
            <li>
                <pre class="auto-style1" style="font-family: 'Julee', cursive;">  Bem Vindo(a)!
                             <asp:Image ID="Image2" runat="server" Height="344px" ImageUrl="~/imagens/TuneRate (1).png" style="margin-left: 50px; margin-top: 0px; margin-bottom: 0px; float:right" /></pre>
            </li>
        </ul>
        <span class="auto-style2" style="font-size: 20px; margin-top: 100px; font-family:'Julee', cursive;">TuneRate permite que os usuários escutem, avaliem e compartilhem suas opiniões sobre músicas.
        <br />
        &nbsp;O app geralmente oferece funcionalidades como a criação de playlists personalizadas, sugestões<br />
&nbsp;de músicas com base nas avaliações anteriores do usuário, e a possibilidade de seguir outros 
        <br />
        usuários para
            descobrir novas músicas.<br />
&nbsp;Os usuários podem dar notas ou classificações às músicas, escrever resenhas e 
        <br />
        ver as<br />
&nbsp;avaliações de outros membros da comunidade. Alguns apps também integram 
        <br />
        com plataformas de streaming,<br />
&nbsp;facilitando a escuta e avaliação diretamente dentro do aplicativo.<br />
&nbsp;Essas avaliações ajudam a criar rankings de músicas populares e oferecem 
        <br />
        insights sobre tendências musicais.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
        <p>
            &nbsp;</p>
    </form>
</body>
    <style>
        body
        .auto-style1 {
            font-size:100px;
            background-color: #000000;
            color:#FF00FF;
        }


        .auto-style1 {
            color: #FF00FF;
        }


        .auto-style2 {
            color: #FFFFFF;
        }


    </style>
</html>
