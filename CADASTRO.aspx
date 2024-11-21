<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="_2024_08_22_TuneRate.CADASTRO" %>

<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>TuneRate - Cadastre-se já!</title>

    <!-- Links para estilos externos -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;500;600&display=swap" rel="stylesheet">

    <style>
        /* Estilo geral da página */
        * {
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
            
        }

        /* Título */
        form h3 {
            font-size: 32px;
            font-weight: 500;
            color: #A458DC;
            margin-bottom: 10px;
        }

        /* Estilo dos rótulos e inputs */
        label {
            display: block;
            font-size: 14px;
            font-weight: 500;
            color: #ffffff;
            text-align: left;
        }

        input[type="text"], input[type="password"] {
            width: 100%;
            padding: 15px;
            border-radius: 5px;
            background-color: rgba(255, 255, 255, 0.07);
            color: #FFF;
            font-size: 16px;
            border: none;
        }

        input::placeholder {
            color: #e5e5e5;
        }

        /* Botão de Cadastro */
        .registerBtn {
            width: 100%;
            padding: 15px;
            background-color: #6700a5;
            color: #FFFFFF;
            border: none;
            border-radius: 5px;
            font-size: 18px;
            font-weight: 600;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .registerBtn:hover {
            background-color: #29135C;
        }

        /* Estilo de erro */
        .errorMessage {
            color: red;
            font-size: 12px;
            margin-top: 5px;
            text-align: left;
        }

        .painelCad{
            display: grid;
            grid-template-rows: 100px 100px 100px 100px 100px 80px;
            width: 400px;
            background-color: rgba(255, 255, 255, 0.13);
            border-radius: 10px;
            padding: 50px 35px;
            box-shadow: 0 0 40px rgba(8, 7, 16, 0.6);
            text-align: center;
            backdrop-filter: blur(10px);

            height: 640px;
        }

        .tagCad{
            grid-row: 1;
            height: 50px;
        }

        .userName{
            grid-row: 2;
            height: 100px;
        }

        .password1{
            grid-row: 3;
            height: 100px;
        }

        .password2{
            grid-row: 4;
            height: 100px;
        }

        .email{
            grid-row: 5;
            height: 100px;
        }

        .btnRegister{
            grid-row: 6;
            height: 100px;
        }

    </style>
</head>

<body>
    <form runat="server">
        <div class="painelCad">
        <h3 class="tagCad">Cadastre-se já!</h3>

            <div class="userName">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="Nome do Usuário:"></asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="inputField" MaxLength="23" placeholder="Nome do Usuário"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="O Nome do Usuário é necessário." CssClass="errorMessage" ValidationGroup="CreateUserWizard1" />
            </div>

            <div class="password1">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Senha:"></asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="inputField" TextMode="Password" MaxLength="15" placeholder="Senha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="A senha é necessária." CssClass="errorMessage" ValidationGroup="CreateUserWizard1" />
            </div>

            <div class="password2">
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" Text="Confirmar Senha:"></asp:Label>
                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="inputField" TextMode="Password" MaxLength="15" placeholder="Confirmar Senha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="É necessário confirmar a senha." CssClass="errorMessage" ValidationGroup="CreateUserWizard1" />
                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" ErrorMessage="A senha e a confirmação devem coincidir." CssClass="errorMessage" ValidationGroup="CreateUserWizard1" />
            </div>

            <div class="email">
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Text="Email:"></asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="inputField" placeholder="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="O email é necessário." CssClass="errorMessage" ValidationGroup="CreateUserWizard1" />
            </div>

            <div class="btnRegister">
                <asp:Button ID="RegisterButton" runat="server" Text="Cadastrar" CssClass="registerBtn" ValidationGroup="CreateUserWizard1" />
            </div>

            <div>
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False" ></asp:Literal>
            </div>

        </div>
    </form>
</body>
</html>
