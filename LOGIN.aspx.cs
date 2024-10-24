using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class LOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string usuario = Login1.UserName;
            string senha = Login1.Password;

            // Valida o usuário usando o método Membership.ValidateUser
            if (Membership.ValidateUser(usuario, senha))
            {
                Session["UsuarioDoLogin"] = usuario;

                // Login com sucesso
                e.Authenticated = true;

                // Redireciona diretamente para MainPage.aspx
                Response.Redirect("default.aspx", false);
            }
            else
            {
                // Login sem sucesso
                e.Authenticated = false;
                Login1.FailureText = "Usuário ou senha inválidos. Por favor, tente novamente!";
            }
        }
    }
}