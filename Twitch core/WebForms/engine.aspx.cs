using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core.WebForms
{
    public partial class engine : System.Web.UI.Page
    {
        [WebMethod(EnableSession = true)]
        public static void LogOut()
        {
            HttpContext.Current.Session.Clear();
        }
        public Service service = new Service();
        public static IMongoDatabase mongodb = new Service.Mongo(System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString).db;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Params["action"] != null)
            {
                switch (Request.Params["action"].ToString())
                {
                    case "create_user":
                        if (Request.Params["token"] == null)
                            Response.Redirect("/", true);
                        else
                        {
                            string token = Request.Params["token"].ToString().ToLower();
                            if (token.Length != 64)
                            {
                                text.Text = "Этот токен не был найден.";
                                text.CssClass = "engine error";
                            }
                            else
                            {
                                BsonDocument filter = new BsonDocument {
                                    { "action", "create_user" },
                                    { "value", token }
                                };
                                List<BsonDocument> tokens = mongodb.GetCollection<BsonDocument>("tokens").Find(filter).ToList();
                                if (tokens.Count != 0)
                                {
                                    if (service.dateSubstract(DateTime.Now, DateTime.Parse(tokens[0]["date_kill"].AsString)) < 24 * 60) // время жизни токена
                                    {
                                        mongodb.GetCollection<BsonDocument>("users").InsertOne(tokens[0]["params"].AsBsonDocument);
                                        mongodb.GetCollection<BsonDocument>("tokens").DeleteOne(new BsonDocument { { "_id", tokens[0]["_id"].AsObjectId } });
                                        text.Text = "Ваш аккаунт был успешно создан! Подробная информация была отправлена на почту. Вы будете автоматически перемещены через 5 секунд..";
                                        Title = "Регистрация произведена успешно";

                                        string pattern = service.LoadPattern(@"~/App_Data/signup-confirmed.html", this, new List<string> {
                                            Request.Url.Host + (Request.Url.Host == "localhost" ? ":" + Request.Url.Port.ToString() : ""),
                                            tokens[0]["params"]["username"].AsString,
                                            tokens[0]["params"]["email"].AsString,
                                            tokens[0]["params"]["password"].AsString
                                        });

                                        Service.MailSender mail = new Service.MailSender("robot@streamerspace.ru", "ILoveTyumen72", "smtp.jino.ru", 25, false);
                                        mail.sendMessage("robot@streamerspace.ru", null, tokens[0]["params"]["email"].AsString.ToLower(), "Успешная регистрация на streamerspace.ru", pattern);
                                        text.CssClass = "engine success";

                                        Session["user"] = tokens[0]["params"].AsBsonDocument;
                                        service.delayedRedir("/users/" + tokens[0]["params"]["username"].AsString + "/bio", 5000, this);
                                    }
                                    else
                                    {
                                        text.Text = "Этот токен более не действителен.";
                                        text.CssClass = "engine error";
                                    }
                                }
                                else
                                {
                                    text.Text = "Этот токен не был найден.";
                                    text.CssClass = "engine error";
                                }
                            }
                        }
                        break;
                    default:
                        Response.Redirect("/", true);
                        break;
                }
            }
            else
                Response.Redirect("/", true);
        }
    }
}