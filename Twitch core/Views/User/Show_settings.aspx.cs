using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core.Views.User
{
    public partial class Show_edit : ViewPage
    {
        public Service service = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewBag.user != null && ViewBag.error == null)
            {
                BsonDocument user = ViewBag.user;
                _profile.NavigateUrl = "/users/" + user["username"].AsString + "/settings/profile";
                _widgets.NavigateUrl = "/users/" + user["username"].AsString + "/settings/widgets";
                _security.NavigateUrl = "/users/" + user["username"].AsString + "/settings/security";
                _connections.NavigateUrl = "/users/" + user["username"].AsString + "/settings/connections";
                _pageContent.Visible = true;
                switch ((string)ViewBag.setting)
                {
                    case "profile":
                        _profile.CssClass = "selected";
                        _username.Text = user["username"].AsString;
                        _display_name.Text = user["display_name"].AsString;
                        _avatar.ImageUrl = user.Contains("avatar") ? user["avatar"].AsString : "/images/default/user-image.svg";
                        _profile_content.Visible = true;
                        _email.Text = user["email"].AsString;
                        _about.Text = user.Contains("about") ? user["about"].AsString : "";

                        
                        break;
                    case "widgets":
                        _widgets.CssClass = "selected";
                        _widgets_content.Visible = true;
                        break;
                    case "security":
                        _security.CssClass = "selected";
                        _security_content.Visible = true;
                        break;
                    case "connections":
                        _connections.CssClass = "selected";
                        _connections_content.Visible = true;

                        if (user.Contains("twitch_username"))
                        {
                            _connectedTwitch.Visible = true;
                            _userTwitchUsername.NavigateUrl = "https://www.twitch.tv/" + user["twitch_username"].AsString;
                            _userTwitchUsername.Text = user["twitch_username"].AsString;
                            _actionTwitch.Text = "Отключить";
                            _actionTwitch.Style.Clear();
                        }
                        break;
                }
            }
            else
            {
                _userNotFoundPanel.Visible = true;
                _notFoundText.Text = ViewBag.error + (ViewBag.errorcode == 403 ? ". Если Вы - владелец этой страницы, то Вам необходимо <a href=\"/login\">войти</a>" : "");
                _errorNum.Text = "ОШИБКА " + ViewBag.errorcode;
            }
        }
    }
}