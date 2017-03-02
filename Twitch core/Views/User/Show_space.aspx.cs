using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core.Views.User
{
    public partial class Show : ViewPage
    {
        public Service service = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewBag.user != null)
            {
                BsonDocument userInfo = (BsonDocument)ViewBag.user;
                Title = "Пространство " + userInfo["username"].AsString + ". Streamerspace";
                _userNotFoundPanel.Visible = false;
                _userContent.Visible = true;

                if (userInfo.Contains("twitch_username"))
                {
                    _userTwitch.Visible = true;

                    WebClient getInfoClient = new WebClient();
                    getInfoClient.Encoding = Encoding.UTF8;

                    BsonDocument channel = new BsonDocument();
                    BsonDocument stream = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/streams/" + userInfo["twitch_username"].AsString.ToLower() + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));
                    if (!stream["stream"].IsBsonNull)
                        channel = stream["stream"]["channel"].AsBsonDocument;
                    else
                        channel = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/channels/" + userInfo["twitch_username"].AsString.ToLower() + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));

                    _twicthUserLink.NavigateUrl = channel["url"].AsString;
                    _twitchUserSSLink.NavigateUrl = "/users/" + userInfo["username"].AsString.ToLower() + "/bio";
                    if (!channel["logo"].IsBsonNull)
                        _twitchUserImage.ImageUrl = channel["logo"].AsString;
                    else
                        _twitchUserImage.ImageUrl = "images/svg/default-user.svg";
                    _twitchUserName.Text = channel["display_name"].AsString;
                    _twitchUserName.NavigateUrl = channel["url"].AsString;
                    _twitchUserStreamStatusGame.Text = channel["game"].AsString;
                    _twitchUserStreamStatusGame.NavigateUrl = "https://twitch.tv/directory/game/" + channel["game"].AsString.ToLower();
                    _userDonationLink.NavigateUrl = "/users/" + userInfo["username"].AsString.ToLower() + "/donate";

                    if (!stream["stream"].IsBsonNull)
                        _twitchCurrentViewers.Text = stream["stream"]["viewers"].AsInt32.ToString("N0", CultureInfo.InvariantCulture);
                    else
                        _twitchCurrentViewers.Style.Add("visibility", "hidden");

                    _twitchViews.Text = channel["views"].AsInt32.ToString("N0", CultureInfo.InvariantCulture);

                    _twitchStreamWindow.Attributes.Add("src", "https://player.twitch.tv/?channel=" + userInfo["twitch_username"].AsString.ToLower());
                    _twitchChat.Attributes.Add("src", "https://www.twitch.tv/" + userInfo["twitch_username"].AsString.ToLower() + "/chat?popout=");

                    ScriptManager.RegisterStartupScript(this, GetType(), "ref", "spaceRefresh(\"" + userInfo["twitch_username"].AsString.ToLower() + "\")", true);
                }
                else
                {
                    _resultBox.Style.Add("background-color", "white");
                    _userNotFoundPanel.Visible = true;
                    _notFoundText.Text = "<font style=\"font-family: 'PT Sans Bold'\">" + userInfo["username"].AsString + "</font> пока что не привязал свой аккаунт Twitch.tv";
                }
            }
            else
            {
                _resultBox.Style.Add("background-color", "white");
                Title = "Пользователь " + (string)ViewBag.requestedUser + " не найден";
                _notFoundText.Text = "Страница не существует или не была найдена";
                _userNotFoundPanel.Visible = true;
            }
        }
    }
}