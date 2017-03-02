using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MongoDB.Bson;

namespace Twitch_core.WebForms
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                HyperLink link = new HyperLink();
                link.CssClass = "button with-image go-to-panel";
                link.Style.Add("margin-top", "35px");
                link.NavigateUrl = "/users/" + ((BsonDocument)Session["user"])["username"].AsString;
                link.Text = "Продолжить";
                page_top.FindControl("Content").Controls.Add(link);
            }
            else
            {
                Panel links = new Panel();
                links.Style.Add("margin", "35px auto");
                links.Style.Add("display", "table");

                HyperLink reg = new HyperLink();
                reg.CssClass = "button with-image signup";
                reg.Style.Add("margin-right", "20px");
                reg.Style.Add("float", "left");
                reg.NavigateUrl = "/signup";
                reg.Text = "Присоединиться";

                HyperLink log = new HyperLink();
                log.CssClass = "button with-image login";
                log.Style.Add("float", "left");
                log.NavigateUrl = "/login";
                log.Text = "Войти";

                links.Controls.Add(reg);
                links.Controls.Add(log);
                page_top.FindControl("Content").Controls.Add(links);
            }
        }

        protected void logOutButton_Click(object sender, EventArgs e)
        {

        }
    }
}