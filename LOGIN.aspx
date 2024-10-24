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

<!--Stylesheet-->
<style media="screen">
  *,
  *:before,
  *:after {
    padding: 0;
    margin: 0;
    box-sizing: border-box;
  }

  body {
    background-color: #080710;
    background-image: url('imgs/background.png'); /* Substitua pelo caminho da sua imagem */
    background-size: cover; /* Faz a imagem cobrir toda a tela */
    background-position: center; /* Centraliza a imagem */
    background-repeat: no-repeat;
    min-height: 100vh;
  }

  form {
    height: 380px;
    width: 400px;
    background-color: rgba(255, 255, 255, 0.13);
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    border-radius: 10px;
    backdrop-filter: blur(10px);
    border: 2px solid rgba(255, 255, 255, 0.1);
    box-shadow: 0 0 40px rgba(8, 7, 16, 0.6);
    padding: 50px 35px;
  }

  form * {
    font-family: 'Poppins', sans-serif;
    color: #ffffff;
    letter-spacing: 0.5px;
    outline: none;
    border: none;
  }

  ::placeholder {
    color: #e5e5e5;
  }

.loginBtn {
    width: 40%;
    color: #000; /* Cor do texto preta */
    padding: 8px 0;
    font-size: 15px;
    font-weight: 400;
    border-radius: 3px;
    cursor: pointer;
    display: block;
    text-align: center;
}


</style>

</head>

<body>
  <div class="background"></div>

  <form runat="server">
    <asp:Login 
      ID="Login1" 
      runat="server" 
      OnAuthenticate="Login1_Authenticate" 
      CreateUserText="Não possui uma conta? Cadastre-se!!" 
      CreateUserUrl="~/Cadastro.aspx">
        <LayoutTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td align="center" colspan="2">Fazer Logon</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nome do Usuário:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="O Nome do Usuário é obrigatório." ToolTip="O Nome do Usuário é obrigatório." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="A senha é obrigatória." ToolTip="A senha é obrigatória." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Lembrar na próxima vez." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <br />
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Fazer Logon" ValidationGroup="Login1" CssClass="loginBtn"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/Cadastro.aspx">Não possui uma conta? Cadastre-se!!</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
  </form>
</body>

</html>


