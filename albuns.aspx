<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="albuns.aspx.cs" Inherits="_2024_08_22_TuneRate.albuns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            padding: 0;
            margin: 0;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TuneRate %>" 
        SelectCommand="SELECT Titulo, CapaBinaria FROM Albuns">
    </asp:SqlDataSource>

</asp:Content>