using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2024_08_22_TuneRate
{
    public partial class LittleHeader : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = Path.GetFileName(Request.Path);

            if (currentPage == "resenhas.aspx")
            {
                navOption1.Attributes.Add("class", "active");
            }
            else if (currentPage == "albuns.aspx")
            {
                navOption2.Attributes.Add("class", "active");
            }
            else if (currentPage == "artistas.aspx")
            {
                navOption3.Attributes.Add("class", "active");
            }
        }

        protected void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string query = SearchBox.Text;

            Response.Redirect($"~/Resultados.aspx?search={query}");
        }

        protected void LogOut()
        {
            // Finaliza a sessão do usuário
            System.Web.Security.FormsAuthentication.SignOut();
            // Redireciona para a página de login ou outra página
            Response.Redirect("Login.aspx");
        }

        protected void LinkButtonLogout_Click(object sender, EventArgs e)
        {
            LogOut();
        }
    }
}