using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core.Views.User
{
    public partial class Show_info : ViewPage
    {
        Service service = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewBag.user != null)
            {
                _infoBlock.Visible = true;
                BsonDocument user = ViewBag.user;
                Title = "Информация о " + user["username"].AsString + ". Streamerspace";
                int level = 1;
                int exp = user["exp"].AsInt32;
                int neededExp = (int)(Math.Pow(level, 2) * 20);
                while (exp >= neededExp)
                {
                    exp -= neededExp;
                    level++;
                    neededExp = (int)(Math.Pow(level, 2) * 20);
                }
                _userLevel.Text = "ур." + level.ToString();
                _userLevelProgressBack.Style.Add("width", ((exp * 100) / neededExp).ToString() + "%");
                _userLevelProgress.Text = exp.ToString("N0", CultureInfo.InvariantCulture) + " / " + neededExp.ToString("N0", CultureInfo.InvariantCulture) + " XP";
                _userImage.ImageUrl = user.Contains("avatar") ? user["avatar"].AsString : "/images/default/user-image.svg";
                _userImage.AlternateText = user.Contains("avatar") ? user["avatar"].AsString + "-avatar" : "default-user-image";
                if (user.Contains("twitch_username") || user.Contains("vk_id") || user.Contains("hitbox_id") || user.Contains("facebook_id") || user.Contains("youtube_id"))
                {
                    _userConnections.Visible = true;
                    if (user.Contains("twitch_username"))
                    {
                        _userTwitchConnection.Visible = true;
                        _userTwitchConnection.NavigateUrl = "https://twitch.tv/" + user["twitch_username"].AsString;
                    }
                    if (user.Contains("vk_id"))
                    {
                        _userVKConnection.Visible = true;
                        _userVKConnection.NavigateUrl = "https://vk.com/id" + user["vk_id"].AsInt32.ToString();
                    }
                    if (user.Contains("facebook_id"))
                    {
                        _userFBConnection.Visible = true;
                        _userFBConnection.NavigateUrl = "https://facebook.com/" + user["facebook_id"].AsString;
                    }
                    if (user.Contains("youtube_channel"))
                    {
                        _userYTConnection.Visible = true;
                        _userYTConnection.NavigateUrl = "https://youtube.com/channel/" + user["youtube_channel"].AsString;
                    }
                }

                if (user["donations_sum"].AsDouble != 0 || user["type"].AsInt32 != 0)
                {
                    _userProfileLines.Visible = true;
                    if (user["donations_sum"].AsDouble != 0)
                    {
                        Label dons = new Label();
                        dons.Text = "Пожертвовал " + user["donations_sum"].AsDouble.ToString("N0", CultureInfo.InvariantCulture) + " RUB";
                        dons.CssClass = "user-donations";
                        _userProfileLines.Controls.Add(dons);
                    }

                    if (user["type"].AsInt32 != 0)
                    {
                        Label userType = new Label();
                        switch (user["type"].AsInt32)
                        {
                            case 50:
                                userType.Text = "Техническая поддержка";
                                userType.CssClass = "tech-support";
                                _userProfileLines.Controls.Add(userType);
                                break;
                            case 100:
                                userType.Text = "Администратор";
                                userType.CssClass = "admin";
                                _userProfileLines.Controls.Add(userType);
                                break;
                        }
                    }
                }
                _userName.Text += user["display_name"].AsString;
                _userCreationDate.Text = "С нами с " + service.formatDate(user["creation_date"].AsDateTime, 0, true, false, false, true) + " года";

                if (Session["user"] != null)
                    if (((BsonDocument)Session["user"])["username"] == user["username"])
                    {
                        _changeInfo.Visible = true;
                        _changeInfo.NavigateUrl = "/users/" + user["username"].AsString + "/settings/profile";
                    }
                _bioHeader.Text = "Информация о " + user["username"].AsString;

                if (user.Contains("about"))
                {
                    Label aboutSpan = new Label();
                    aboutSpan.Text = "О себе";
                    aboutSpan.CssClass = "option-text";
                    Label about = new Label();
                    about.Text = user["about"].AsString;
                    about.CssClass = "option-value";
                    _userBioLines.Controls.Add(aboutSpan);
                    _userBioLines.Controls.Add(about);
                }

                Label achievementsSpan = new Label();
                achievementsSpan.Text = "Награды";
                achievementsSpan.CssClass = "option-text";
                Panel medalsPanel = new Panel();
                medalsPanel.CssClass = "medals";
                Label rework = new Label();
                rework.Text = "В РАЗРАБОТКЕ";
                medalsPanel.Controls.Add(rework);
                _userBioLines.Controls.Add(achievementsSpan);
                _userBioLines.Controls.Add(medalsPanel);

                if (user.Contains("twitch_username"))
                    GenerateSocial("twitch", user["twitch_username"].AsString, user["username"].AsString);
            }
            else
                _userNotFoundPanel.Visible = true;
        }
        public void GenerateSocial(string target, string param, string ssUsername)
        {
            Panel hood = new Panel();
            hood.CssClass = "social " + target + "-hood";

            Image userSocialImage = new Image();
            userSocialImage.CssClass = "social-image";
            Panel info = new Panel();
            info.CssClass = "info";

            WebClient getInfoClient = new WebClient();
            getInfoClient.Encoding = Encoding.UTF8;

            switch (target)
            {
                case "twitch":
                    BsonDocument channel = new BsonDocument();
                    BsonDocument stream = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/streams/" + param.ToLower() + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));
                    if (!stream["stream"].IsBsonNull)
                        channel = stream["stream"]["channel"].AsBsonDocument;
                    else
                        channel = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/channels/" + param.ToLower() + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));

                    if (!channel["logo"].IsBsonNull)
                    {
                        userSocialImage.ImageUrl = channel["logo"].AsString;
                        userSocialImage.AlternateText = channel["display_name"].AsString + "-twitch-image";
                    }
                    else
                    {
                        userSocialImage.ImageUrl = "/images/svg/default-user.svg";
                        userSocialImage.AlternateText = "default-user-image";
                    }
                    hood.Controls.Add(userSocialImage);
                    HyperLink socialName = new HyperLink();
                    socialName.Text = channel["display_name"].AsString;
                    socialName.CssClass = "social-name";
                    socialName.NavigateUrl = channel["url"].AsString;
                    hood.Controls.Add(socialName);

                    Panel channelInfo = new Panel();
                    channelInfo.CssClass = "channel-info";
                    Label folCount = new Label();
                    folCount.CssClass = "followers";
                    folCount.Text = "Подписчиков: <font style=\"color: white\">" + channel["followers"].AsInt32.ToString("N0", CultureInfo.InvariantCulture) + "</font>";
                    Label viewsCount = new Label();
                    viewsCount.CssClass = "views";
                    viewsCount.Text = "Просмотров: <font style=\"color: white\">" + channel["views"].AsInt32.ToString("N0", CultureInfo.InvariantCulture) + "</font>";
                    channelInfo.Controls.Add(folCount);
                    channelInfo.Controls.Add(viewsCount);

                    info.Controls.Add(socialName);
                    info.Controls.Add(channelInfo);
                    hood.Controls.Add(info);

                    if (!stream["stream"].IsBsonNull)
                    {
                        Panel streamStatus = new Panel();
                        streamStatus.CssClass = "stream-status";

                        Label online = new Label();
                        online.Text = "Идет трансляция";
                        HyperLink watch = new HyperLink();
                        watch.Text = "Смотреть";
                        watch.NavigateUrl = "/users/" + ssUsername;
                        Label viewersCount = new Label();
                        viewersCount.CssClass = "viewers";
                        viewersCount.Text = "Зрителей: <font style=\"color: red\">" + stream["stream"]["viewers"].AsInt32.ToString("N0", CultureInfo.InvariantCulture) + "</font>";
                        streamStatus.Controls.Add(online);
                        streamStatus.Controls.Add(watch);
                        streamStatus.Controls.Add(viewersCount);
                        hood.Controls.Add(streamStatus);
                    }

                    _userSocial.Controls.Add(hood);
                    break;
            }

        }
    }
}