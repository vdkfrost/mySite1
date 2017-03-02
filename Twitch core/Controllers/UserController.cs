using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Twitch_core.Controllers
{
    public class UserController : Controller
    {
        Service service = new Service();
        Service.Postgre pg = new Service.Postgre(System.Configuration.ConfigurationManager.ConnectionStrings["PostgreConnection"].ConnectionString);
        List<BsonDocument> user = new List<BsonDocument>();
        public ActionResult Show(string username, string page, string setting = null)
        {
            if (username != null)
            {
                Service.Postgre pg = new Service.Postgre(System.Configuration.ConfigurationManager.ConnectionStrings["PostgreConnection"].ConnectionString);
                BsonDocument u = new BsonDocument();
                switch (page)
                {
                    case "":
                    case "bio":
                    case null:
                        u = (BsonDocument)pg.Read("select * from users.main where username = @username",
                            new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("@username", username)
                            }, 1);
                        if (u.Elements.Count() != 0)
                        {
                            u.AddRange((BsonDocument)pg.Read("select sum(value) as \"donations_sum\" from donations.main where owner_id = " + u["user_id"].AsInt32.ToString(), null, 1));
                            ViewBag.user = u;
                        }

                        switch (page)
                        {
                            case "bio":
                                return View("Show_bio");
                            default:
                                return View("Show_space");
                        }
                    case "settings":
                    case "donations":
                        bool hasAccess = false;
                        u = (BsonDocument)pg.Read("select * from users.main where username = @username",
                            new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("@username", username)
                            }, 1);
                        if (u.Elements.Count() != 0)
                        {
                            ViewBag.user = u;
                            if (Session["user"] != null)
                                if (((BsonDocument)Session["user"])["username"].AsString == username)
                                    hasAccess = true;

                            if (hasAccess)
                                switch (setting)
                                {
                                    case "profile":
                                        ViewBag.setting = setting;
                                        break;
                                    case "widgets":
                                        ViewBag.setting = setting;
                                        break;
                                    case "security":
                                        ViewBag.setting = setting;
                                        break;
                                    case "connections":
                                        ViewBag.setting = setting;
                                        break;
                                    default:
                                        ViewBag.errorcode = 404;
                                        ViewBag.error = "Данная опция не найдена";
                                        break;
                                }
                            else
                            {
                                ViewBag.errorcode = 403;
                                ViewBag.error = "У Вас нет доступа к этой странице";
                            }
                        }
                        else
                        {
                            ViewBag.errorcode = 404;
                            ViewBag.error = "Этот пользователь не найден";
                        }

                        if (page == "settings")
                            return View("Show_settings");
                        else
                            return View("Show_donations");
                    default:
                        Response.Redirect("/", true);
                        return null;
                }
            }
            else
            {
                Response.Redirect("/", true);
                return null;
            }
        }

        public JsonResult RefreshDashboard(string username)
        {
            WebClient getInfoClient = new WebClient();
            getInfoClient.Encoding = Encoding.UTF8;

            string[] result = new string[] { "", "", "" };
            try
            {
                BsonDocument channel = new BsonDocument();
                BsonDocument stream = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/streams/" + username + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));
                if (!stream["stream"].IsBsonNull)
                    channel = stream["stream"]["channel"].AsBsonDocument;
                else
                    channel = BsonDocument.Parse(getInfoClient.DownloadString("https://api.twitch.tv/kraken/channels/" + username + "?client_id=s8i04ddld4n02re2brf1gb7flh74jl"));

                result[0] = channel["game"].AsString;
                if (!stream["stream"].IsBsonNull)
                    result[1] = stream["stream"]["viewers"].AsInt32.ToString("N0", CultureInfo.InvariantCulture);
                result[2] = channel["views"].AsInt32.ToString("N0", CultureInfo.InvariantCulture);
            }
            catch { }
            JsonResult res = new JsonResult();
            res.Data = result;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
    }
}
