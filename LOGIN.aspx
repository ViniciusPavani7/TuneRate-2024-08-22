<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="_2024_08_22_TuneRate.LOGIN" %>

<!DOCTYPE html>

<html lang="en">
<head>
  <!-- Design by foolishdeveloper.com -->
    <title>TuneRate - Login</title>
 
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;500;600&display=swap" rel="stylesheet">
    <!--Stylesheet-->

    <style>
    *.{
        padding: 0;
        margin: 0;
    }
    body{
        background-color: #080710;
        background-image: url('imgs/background.png'); /* Substitua pelo caminho da sua imagem */
        background-size: cover; /* Faz a imagem cobrir toda a tela */
        background-position: center; /* Centraliza a imagem */
        background-repeat: no-repeat;
        min-height: 100vh;
        width: 100%;
        height:100%;

    </style>

</head>

<body>
    <div class="background"></div>
    <form runat="server">
        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" CreateUserText="Não possui uma conta? Cadastre-se!!" CreateUserUrl="~/Cadastro.aspx">
        </asp:Login>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>

</html>

