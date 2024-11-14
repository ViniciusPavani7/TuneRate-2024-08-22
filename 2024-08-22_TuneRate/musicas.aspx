<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="musicas.aspx.cs" Inherits="_2024_08_22_TuneRate.resenhas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            padding: 0;
            margin: 0;
        }

        .imgIcon1 {
            position: fixed;
            bottom: 47px;
            right: 10px;
            width: 70px;
            height: 180px;
            justify-content: space-between;
        }
       .icon1 {
            height: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="imgIcon1">
            <asp:Image ID="Image1" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image2" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
            <asp:Image ID="Image3" runat="server" class="icon1" ImageUrl="~/imgs/icon1.png" />
        </div>
</asp:Content>
