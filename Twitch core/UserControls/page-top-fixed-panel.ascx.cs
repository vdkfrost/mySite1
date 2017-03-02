using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core.UserControls
{
    public partial class page_top_fixed_panel : System.Web.UI.UserControl
    {
        public static Service service = new Service();
        public static IMongoDatabase mongodb = new Service.Mongo(System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString).db;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Session["user"] = new BsonDocument
            {
                { "_id", ObjectId.Parse("589e240b8014a22b0062c0e4") },
                { "display_name", "vlad" },
                { "username", "vlad" },
                { "email", "vlad.kibenko2@hotmail.com" },
                { "password", "44f1ae5b3872" },
                { "avatar", "/images/default/user-image.svg" },
                { "don_window_key", "d0d2df640e1d95014b2aaf6ea5d7474accf3967a52e08bdce412f6cdae772ef9" },
                { "exp", BsonInt32.Create(351) },
                { "balance", new BsonDocument {
                    { "current", 350.21 },
                    { "in_process", 100.40 } } },
                { "creation_date", "11.02.2017 01:12" },
                { "premium", false },
                { "type", BsonInt32.Create(1) }
            };*/
            if (Session["user"] != null)
            {
                Service.Postgre pg = new Service.Postgre(System.Configuration.ConfigurationManager.ConnectionStrings["PostgreConnection"].ConnectionString);
                BsonDocument user = (BsonDocument)pg.Read("select * from users.main where username = @username",
                    new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("@username", ((BsonDocument)Session["user"])["username"].AsString)
                    }, 1);
                
                _userPanel.Visible = true;
                _userLink.NavigateUrl = "/users/" + user["username"].AsString.ToLower() + "/bio";
                _userName.Text = user["display_name"].AsString;
                _userSpace.NavigateUrl = "/users/" + user["username"].AsString.ToLower();
                _userSettings.NavigateUrl = "/users/" + user["username"].AsString.ToLower() + "/settings/profile";
                _userDonations.NavigateUrl = "/users/" + user["username"].AsString.ToLower() + "/donations";
                _userImage.ImageUrl = user.Contains("avatar") ? user["avatar"].AsString : "/images/default/user-image.svg";
                _userImage.AlternateText = user["username"].AsString + "-avatar";
                _userBalanceCurrent.Text = user["balance.current"].AsDouble.ToString("N", CultureInfo.InvariantCulture) + " RUB";
                _userBalanceInProcess.Text = user["balance.in_process"].AsDouble.ToString("N", CultureInfo.InvariantCulture) + " RUB";
                Session["user"] = user;
            }
            else
            {
                _accountActions.Visible = true;
            }
        }

        protected void _logOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.RawUrl.ToString());
        }
    }
}