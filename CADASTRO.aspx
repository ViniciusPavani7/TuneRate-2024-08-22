<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CADASTRO.aspx.cs" Inherits="_2024_08_22_TuneRate.CADASTRO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title></title>
 
    <link rel="preconnect" href="https://fonts.gstatic.com"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;500;600&display=swap" rel="stylesheet"/>

    <!--Stylesheet-->
    <style media="screen">
      *,
*:before,
*:after{
    padding: 0;
    margin: 0;
    box-sizing: border-box;
}
body{
    background-color: #080710;
     background-image: url('imgs/background.png'); /* Substitua pelo caminho da sua imagem */
    background-size: cover; /* Faz a imagem cobrir toda a tela */
    background-position: center; /* Centraliza a imagem */
    background-repeat: no-repeat;
     min-height: 100vh;
 
}
.background{
    width: 430px;
    height: 520px;
    position: absolute;
    transform: translate(-50%,-50%);
    left: 50%;
    top: 50%;
    
}
.background .shape{
    height: 200px;
    width: 200px;
    position: absolute;
    border-radius: 50%;
}
.shape:first-child{
    background: linear-gradient(
        #1845ad,
        #23a2f6
    );
    left: -80px;
    top: -80px;
}
.shape:last-child{
    background: linear-gradient(
        to right,
        #ff512f,
        #f09819
    );
    right: -30px;
    bottom: -80px;
}
form{
    height: 520px;
    width: 400px;
    background-color: rgba(255,255,255,0.13);
    position: absolute;
    transform: translate(-50%,-50%);
    top: 50%;
    left: 50%;
    border-radius: 10px;
    backdrop-filter: blur(10px);
    border: 2px solid rgba(255,255,255,0.1);
    box-shadow: 0 0 40px rgba(8,7,16,0.6);
    padding: 50px 35px;
}
form *{
    font-family: 'Poppins',sans-serif;
    color: #ffffff;
    letter-spacing: 0.5px;
    outline: none;
    border: none;
}
form h3{
    font-size: 32px;
    font-weight: 500;
    line-height: 42px;
    text-align: center;
}

label{
    display: block;
    margin-top: 30px;
    font-size: 16px;
    font-weight: 500;
}
input{
    display: block;
    height: 50px;
    width: 100%;
    background-color: rgba(255,255,255,0.07);
    border-radius: 3px;
    padding: 0 10px;
    margin-top: 8px;
    font-size: 14px;
    font-weight: 300;
}
::placeholder{
    color: #e5e5e5;
}
button{
    margin-top: 50px;
    width: 100%;
    background-color: #391477;
    color: #080710;
    padding: 15px 0;
    font-size: 18px;
    font-weight: 600;
    border-radius: 5px;
    cursor: pointer;
}
.social{
  margin-top: 30px;
  display: flex;
}
.social div{
  background: red;
  width: 150px;
  border-radius: 3px;
  padding: 5px 10px 10px 5px;
  background-color: rgba(255,255,255,0.27);
  color: #eaf0fb;
  text-align: center;
}
.social div:hover{
  background-color: rgba(255,255,255,0.47);
}
.social .fb{
  margin-left: 25px;
}
.social i{
  margin-right: 4px;
}

    </style>
</head>
<body>
    
    <div class="background"></div>
    
    <form runat="server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="LOGIN.aspx"
    OnCreatedUser="CreateUserWizard1_CreatedUser" OnNextButtonClick="CreateUserWizard1_NextButtonClick">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="CreateUserWizardStep1">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Nome Completo:</td>
                            <td>
                                <asp:TextBox ID="FullName" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="FullName" runat="server" ErrorMessage="O nome completo é obrigatório." />
                            </td>
                        </tr>
                        <tr>
                            <td>Nome de Usuário:</td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="UserName" runat="server" ErrorMessage="O nome de usuário é obrigatório." />
                            </td>
                        </tr>
                        <tr>
                            <td>Senha:</td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="Password" runat="server" ErrorMessage="A senha é obrigatória." />
                            </td>
                        </tr>
                        <tr>
                            <td>Confirme a Senha:</td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" />
                                <asp:CompareValidator ControlToValidate="ConfirmPassword" ControlToCompare="Password" runat="server" ErrorMessage="As senhas não coincidem." />
                            </td>
                        </tr>
                        <tr>
                            <td>E-mail:</td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="Email" runat="server" ErrorMessage="O e-mail é obrigatório." />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server" ID="CompleteWizardStep1">
                <ContentTemplate>
                    Registro concluído com sucesso! Um e-mail de confirmação foi enviado para você.
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>


    </form>
</body>
</html>
