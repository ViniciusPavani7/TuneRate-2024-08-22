<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="_2024_08_22_TuneRate.LOGIN" %>

<!DOCTYPE html>

<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>TuneRate - Faça já o seu login!</title>

    <!-- Links para estilos externos -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;500;600&display=swap" rel="stylesheet">

    <style>
        /* Estilo geral da página */
        *:before, *:after {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            background-color: #080710;
            background-image: url('imgs/background.png'); /* Substitua pelo caminho da sua imagem */
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            min-height: 100vh;
            font-family: 'Poppins', sans-serif;
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        form {
            width: 400px;
            background-color: rgba(255, 255, 255, 0.13);
            border-radius: 10px;
            padding: 50px 35px;
            box-shadow: 0 0 40px rgba(8, 7, 16, 0.6);
            text-align: center;
            backdrop-filter: blur(10px);
        }

        /* Título */
        form h3 {
            font-size: 36px;
            font-weight: 600;
            color: #A458DC; /* Título em roxo */
            margin-bottom: 30px;
        }

        /* Estilo dos rótulos e inputs */
        label {
            display: none; /* Oculta os rótulos */
        }

        input[type="text"], input[type="password"] {
            width: 366px;
            padding: 15px;
            margin: 15px 0;
            border-radius: 5px;
            background-color: rgba(255, 255, 255, 0.07);
            color: #FFF;
            font-size: 16px;
        }

        input[type="text"]::placeholder, input[type="password"]::placeholder {
            color: #e5e5e5;
            font-weight: 500;
        }

        /* Botão de Login */
        .loginBtn {
            width: 100%;
            padding: 15px;
            background-color: #A458DC; /* Cor roxa */
            color: #FFFFFF;
            border: none;
            border-radius: 5px;
            font-size: 18px;
            font-weight: 600;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .loginBtn:hover {
            background-color: #8B3FB7; /* Cor roxa mais escura */
        }

        /* Link de cadastro */
        .createUserLink {
            color: white; /* Cor preta */
            font-weight: 500;
            text-decoration: none;
            display: inline-block;
            margin-top: 20px;
        }

        .createUserLink:hover {
            text-decoration: underline;
        }

        /* Estilo de erro */
        .errorMessage {
            color: red;
            font-size: 14px;
            margin-top: 5px;
        }
    </style>
</head>

<body>
    <form runat="server">
        <h3>Login</h3>
        <asp:Login 
            ID="Login1" 
            runat="server" 
            OnAuthenticate="Login1_Authenticate" 
            CreateUserText="Não possui uma conta? Cadastre-se!!" 
            CreateUserUrl="~/Cadastro.aspx">
            <LayoutTemplate>
                <asp:TextBox ID="UserName" runat="server" CssClass="inputField" placeholder="Nome do Usuário"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="O Nome do Usuário é obrigatório." ToolTip="O Nome do Usuário é obrigatório." CssClass="errorMessage" ValidationGroup="Login1" />

                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="inputField" placeholder="Senha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="A senha é obrigatória." ToolTip="A senha é obrigatória." CssClass="errorMessage" ValidationGroup="Login1" />

                <asp:Literal ID="FailureText" runat="server" EnableViewState="False" ></asp:Literal>

                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Fazer Logon" ValidationGroup="Login1" CssClass="loginBtn" />

                <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/CADASTRO.aspx" CssClass="createUserLink">Não possui uma conta? Cadastre-se!!</asp:HyperLink>
            </LayoutTemplate>
        </asp:Login>
    </form>
</body>
</html>


