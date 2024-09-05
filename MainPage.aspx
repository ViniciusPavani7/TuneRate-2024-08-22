<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="_2024_08_22_TuneRate.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            padding: 0;
            margin: 0;
        }

        .bodyPage {
           
        }

        .artsTag {
            font-weight: bold;
            font-size: 25px;
        }

        .mscsTag {
            font-weight: bold;
            font-size: 25px;
        }

       .artistDiv {
            position: absolute;
            left: 7%;
            top: 25%; 
        }

       .musicDiv {
            position: absolute; 
            left: 7%; 
            top: 60%; 
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
    <div class="bodyPage">
        <div>
                <div class="artistDiv">
                 <asp:Label ID="artTag" runat="server" class="artsTag" Text="Label">Artistas:</asp:Label>

                    <div class="artistDiv2">

                        <div class="indArtist">

                        </div>

                    </div>

                </div>

                <div class="musicDiv">
                 <asp:Label ID="mscTag" runat="server" class="mscsTag" Text="Label">Músicas:</asp:Label>

                    <div class="musicDiv2">

                        <div class="indMusic">

                        </div>

                    </div>

                </div>
        </div>


        <div class="imgIcon1">
            <asp:Image  ID="Image1" runat="server"  class="icon1"  ImageUrl="~/imgs/icon1.png" />
            <asp:Image  ID="Image2" runat="server"  class="icon1"  ImageUrl="~/imgs/icon1.png" />
            <asp:Image  ID="Image3" runat="server"  class="icon1"  ImageUrl="~/imgs/icon1.png" />
        </div>
    </div>
</asp:Content>
