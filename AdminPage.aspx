<%@ Page Title="" Language="C#" MasterPageFile="~/LittleHeader.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="_2024_08_22_TuneRate.AdminPage"  Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            padding: 0;
            margin: 0;
            border: 0;
        }

        .all{
            padding-top: 100px;
        }

        .rdBtn {
            display: none; /* Esconde o controle padrão */
        }

        /* Estilizar o texto associado aos RadioButtons */
        .rdBtn + label {
            display: inline-block;
            text-align: center;
            padding: 5px 10px;
            width: auto;
            color: #FFF;
            font-family: 'Roboto', sans-serif;
            cursor: pointer;
            transition: background-color 0.2s ease-in-out, color 0.2s ease-in-out;
        }

        /* Estilo ao estar selecionado */
        .rdBtn:checked + label {
            background-color: red; /* Cor de fundo do botão selecionado */
            color: #fff; /* Cor do texto quando selecionado */
            border-bottom: 3px solid white; /* Borda de destaque na parte inferior */
        }
   
        .selectedTab {
            display: none;
        }

        .selectedTab + label {
            outline: 2px solid gray;
            color: #CF51CA;
        }

        /* Estilo ao passar o mouse */
        .rdBtn + label:hover {
            background-color: #444;
            color: #CF51CA;
        }

        .tabs input[type="radio"] {
            appearance: none; /* Para navegadores modernos */
            -webkit-appearance: none; /* Para navegadores WebKit */
            -moz-appearance: none; /* Para navegadores Firefox */
        }

        /* Contêiner das abas */
        .tabs {
            display: flex;
            flex-flow: row;
        }

        .tabcontent{
            border-top: 2px solid gray;
            width: 100%;
            padding-top: 50px;

            display: grid;
            grid-template-rows: auto auto;
            gap: 10px;
        }

        .tag{
            font-size: 20px;
        }

        

        .divArtAdd, .divMusic{
            display: grid;
            margin: 20px 0;
            grid-template-rows: repeat(7, auto);
            grid-template-columns: 330px auto;
            gap: 15px;
        }

        .divAlbum, .divArtDel, .divMusic, .divUserAdd {
            display: grid;
            grid-template-columns: 330px;
            gap: 15px;
        }

        .divArtDel, .divUserDel, .divUserRole{
            border-top: 1px solid gray;
            padding-top: 20px;

            display: grid;
            grid-template-rows: repeat(3, auto);
            grid-template-columns: 330px;
            gap: 15px;
        }

        .divUserRole{
            margin-top: 20px;
        }

        .AddArt{
            width: 330px;
            grid-column: 1;
        }

        .btnAddArt, .btnAddAlbum, .btnAddMusic, .btnAddUser, .btnPromote{
            font-weight: bold;
            border: none;
            width: 200px;
            border-radius: 15px;
            background-color: white;
            margin-left: 70px;
            grid-column: 1;
            transition: background-color 0.3s ease, transform 0.3s ease, box-shadow 0.3s ease;
        }

        .btnAddArt:hover, .btnAddAlbum:hover, .btnAddMusic:hover, .btnAddUser:hover, .btnPromote:hover{
            background-color: #A458DC;
        }

        .btnDelArt, .btnDelUser, .btnNoAdmin{
            font-weight: bold;
            border: none;
            width: 200px;
            border-radius: 15px;
            background-color: white;
            margin-left: 70px;
            grid-column: 1;
            transition: background-color 0.3s ease, transform 0.3s ease, box-shadow 0.3s ease;
        }

        .btnDelArt:hover, .btnDelUser:hover, .btnNoAdmin:hover{
            background-color: #970303;
            color: black;
        }

        .HyperLink{
            
           
            
        }

        .hLink{
            grid-column: 2;
            grid-row: 3 / 5;
            align-content: center;
        }

        .upload{
            grid-column: 1;
        }

        .divAlbum, .divUserAdd{
            margin: 20px 0;
            display: grid;
            grid-template-rows: repeat(5, auto);
        }

        .teste{
            width: 330px;
        }

        

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="all">

        <div class="tabs">
            <asp:RadioButton ID="rbArtistas" CssClass="rdBtn" GroupName="TabGroup" OnCheckedChanged="TabChanged" AutoPostBack="true" runat="server" ClientIDMode="Static" />
            <label for="rbArtistas">Artistas</label>

            <asp:RadioButton ID="rbAlbuns" CssClass="rdBtn" GroupName="TabGroup" OnCheckedChanged="TabChanged" AutoPostBack="true" runat="server" ClientIDMode="Static" />
            <label for="rbAlbuns">Álbuns</label>

            <asp:RadioButton ID="rbMusicas" CssClass="rdBtn" GroupName="TabGroup" OnCheckedChanged="TabChanged" AutoPostBack="true" runat="server" ClientIDMode="Static" />
            <label for="rbMusicas">Músicas</label>

            <asp:RadioButton ID="rbUsers" CssClass="rdBtn" GroupName="TabGroup" OnCheckedChanged="TabChanged" AutoPostBack="true" runat="server" ClientIDMode="Static" />
            <label for="rbUsers">Usuários</label>
        </div>

        <div class="tabcontent">
        
            <asp:Panel ID="pnlArtistas" runat="server" Visible="false">
                <div class="arts">
                    <div class="divArtAdd">
                        <asp:Label ID="lblArtAdd" CssClass="Tag" runat="server" Text="Adicionar Artista"></asp:Label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="AddArt" placeholder="Nome" />
                            <asp:DropDownList ID="ddlNacionalidades" CssClass="AddArt" runat="server"></asp:DropDownList>
                            
                            <div class="hLink">
                                <asp:HyperLink ID="hlTranslate" runat="server" NavigateUrl="https://translate.google.com/" 
                                   Text="Clique aqui para traduzir" Font-Size="16px" ForeColor="Blue" 
                                   Target="_blank" CssClass="HyperLink" />
                            </div>

                            <asp:DropDownList ID="ddlGeneros" CssClass="AddArt" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtDataNascimento" CssClass="AddArt" runat="server" placeholder="Data de Nascimento do Artista" onkeyup="formatDate(this);" MaxLength="8" />
                            <asp:FileUpload ID="FileUploadImagem" CssClass="upload" runat="server" />
                            <asp:Button ID="btnAdicionarArtista" CssClass="btnAddArt" runat="server" Text="Adicionar Artista" OnClick="btnAdicionarArtista_Click" />
                    </div>

                    <div class="divArtDel">
                        <asp:Label ID="lblArtDel" CssClass="Tag" runat="server" Text="Deletar Artista"></asp:Label>
                            <asp:DropDownList ID="ddlArtistasDel" runat="server">
                                <asp:ListItem Value="">Selecione um Artista</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnDeletarArtista" CssClass="btnDelArt" runat="server" Text="Deletar" OnClick="btnDeletarArtista_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAlbuns" runat="server" Visible="false">
                <div class="divAlbum">
                 <asp:Label ID="lblAlbum" CssClass="Tag" runat="server" Text="Adicionar Álbum"></asp:Label>
                    <asp:TextBox ID="txtTituloAlbum" runat="server" placeholder="Título do Álbum" />
                    <asp:DropDownList ID="ddlArtsAlb" runat="server">
                        <asp:ListItem Text="Selecione um Artista" Value="0" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtDataLancamentoAlbum" runat="server" onkeyup="formatDate(this);" MaxLength="8" placeholder="Data de Lançamento" />
                    <asp:FileUpload ID="FileUploadCapa" runat="server" />

                    <asp:Button ID="btnAdicionarAlbum" CssClass="btnAddAlbum" runat="server" Text="Adicionar Álbum" OnClick="btnAdicionarAlbum_Click" />
                </div>
             </asp:Panel>

            <asp:Panel ID="pnlMusicas" runat="server" Visible="false">
                <div class="divMusic">
                    <asp:Label ID="lblMusic" CssClass="Tag" runat="server" Text="Adicionar Música"></asp:Label>
                    <asp:TextBox ID="txtTitulo" runat="server" placeholder="Título"></asp:TextBox>

                    <asp:DropDownList ID="ddlArtsMsc" runat="server">
                        <asp:ListItem Value="">Selecione um Artista</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFeats" runat="server">
                        <asp:ListItem Value="">Selecione Participações (Feats)</asp:ListItem>
                    </asp:DropDownList>

                    <asp:TextBox ID="txtDataLancamento" runat="server" MaxLength="10" placeholder="Data de Lançamento (AAAA-MM-DD)"></asp:TextBox>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:DropDownList ID="ddlAlbuns" runat="server">
                        <asp:ListItem Value="SINGLE">SINGLE</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAdicionarMusica" CssClass="btnAddMusic" runat="server" Text="Adicionar Música" OnClick="btnAdicionarMusica_Click" />
               </div>
            </asp:Panel>

            <asp:Panel ID="pnlUsers" runat="server" Visible="false">
                <div class="divUsers">
                    <div class="divUserAdd">
                        <asp:Label ID="lblUserAdd" CssClass="Tag" runat="server" Text="Cria nova conta"></asp:Label>
                        <asp:TextBox ID="txtNovoUsuario" runat="server" placeholder="Nome de Usuário" />
                        <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password" placeholder="Senha" />
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" />
                        <asp:Button ID="btnCriarConta" CssClass="btnAddUser" runat="server" Text="Criar Conta" OnClick="btnCriarConta_Click" />
                    </div>

                    <div class="divUserDel">
                        <asp:Label ID="lblUserDel" CssClass="Tag" runat="server" Text="Excluir uma conta"></asp:Label>
                        <asp:DropDownList ID="ddlUsuariosDel" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnDeletarUsuario" CssClass="btnDelUser" runat="server" Text="Deletar Usuário" OnClick="btnDeletarUsuario_Click" />
                    </div>

                    <div class="divUserRole">
                        <asp:Label ID="Label1" CssClass="Tag" runat="server" Text="Controle cargos de usuários"></asp:Label>
                        <asp:DropDownList ID="ddlUsuariosRole" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnPraVirarAdmin" CssClass="btnPromote" runat="server" Text="Tornar Admin" OnClick="btnPraVirarAdmin_Click" />
                        <asp:Button ID="btnPraRemoverAdmin" CssClass="btnNoAdmin" runat="server" Text="Remover cargo Admin" OnClick="btnPraRemoverAdmin_Click" />
                    </div>
                </div>
            </asp:Panel>

                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblMensage" runat="server" Text="" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>

        </div>
    </div>

    <script type="text/javascript">
        function formatDate(input) {
            let value = input.value.replace(/[^0-9]/g, '');
            if (value.length > 4) {
                value = value.slice(0, 4) + '-' + value.slice(4);
            }
            if (value.length > 7) {
                value = value.slice(0, 7) + '-' + value.slice(7);
            }
            input.value = value;
        }
    </script>

    <script type="text/javascript">
        function formatDate(input) {
            let value = input.value.replace(/[^0-9]/g, '');
            if (value.length > 4) {
                value = value.slice(0, 4) + '-' + value.slice(4);
            }
            if (value.length > 7) {
                value = value.slice(0, 7) + '-' + value.slice(7);
            }
            input.value = value;
        }
    </script>

</asp:Content>

