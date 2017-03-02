using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Insight.Database;
using Npgsql;

namespace Twitch_core.WebForms
{
    public partial class login : System.Web.UI.Page
    {
        [WebMethod(EnableSession = true)]
        public static string[] logIn(string username, string password)
        {
            Service.Postgre pg = new Service.Postgre(System.Configuration.ConfigurationManager.ConnectionStrings["PostgreConnection"].ConnectionString);
            BsonDocument u = (BsonDocument)pg.Read("select * from users.main where (username = @username or email = @username) and password = @password",
                new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("@username", username),
                    new KeyValuePair<string, string>("@password", password)
                }, 1);
            if (u.Elements.Count() != 0)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session["user"] = u;
                return new string[] { "success", u["username"].AsString };
            }
            else
                return new string[] { "error" };
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink link = new HyperLink();
            link.CssClass = "button with-image home";
            link.Style.Add("margin-top", "35px");
            link.NavigateUrl = "/";
            link.Text = "На главную";
            _topPage.FindControl("Content").Controls.Add(link);
        }
    }
}