<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="perfil.aspx.cs" Inherits="_2024_08_22_TuneRate.perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        body{
            padding: 0;
            margin: 0;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:LinkButton ID="ShowMenu" runat="server" OnClick="btnShowMenu_Click">
            <asp:Image ID="imgPerfil" runat="server" class="imgPerfil" ImageUrl="~/imgs/unknown.png" />
        </asp:LinkButton>


        <!-- Painel de upload de imagem (invisível inicialmente) -->
    <div>
        <asp:Panel ID="panelUpload" runat="server" Visible="false">
            <asp:FileUpload ID="FileUploadImagem" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Alterar Foto de Perfil" OnClick="btnAlterarFoto_Click" />
            <asp:Button ID="btnDeletarFoto" runat="server" Text="Deletar Foto de Perfil" OnClick="btnDeletarFoto_Click" CssClass="btn btn-danger" />

            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red" />
        </asp:Panel>         
     </div>

</asp:Content>

