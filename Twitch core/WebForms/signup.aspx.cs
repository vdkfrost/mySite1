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
using Twitch_core.WebForms;

namespace Twitch_core.WebForms
{
    public partial class signup_beta : System.Web.UI.Page
    {
        [WebMethod]
        public static string checkParam(string param, string paramName, string regex)
        {
            Regex check = new Regex(regex);
            if (check.IsMatch(param))
            {
                if (paramName != "pass")
                {
                    // Проверка среди существующих пользователей
                    List<BsonDocument> users = mongodb.GetCollection<BsonDocument>("users").Find(new BsonDocument { { paramName, param.ToLower() } }).ToList();
                    if (users.Count != 0)
                        return "taken";

                    BsonDocument filter = new BsonDocument {
                        { "action", "create_user" },
                        { "params." + paramName, param.ToLower() }
                    };
                    // Проверка среди ждущих подтверждения
                    List<BsonDocument> tokens = mongodb.GetCollection<BsonDocument>("tokens").Find(filter).ToList();
                    switch (paramName)
                    {
                        case "email":
                            foreach (BsonDocument token in tokens)
                            {
                                if (service.dateSubstract(DateTime.Now, DateTime.Parse(token["date_kill"].AsString)) < 24 * 60) // время жизни токена
                                    return "taken";
                                else
                                    return "update needed";
                            }
                            return "ok";
                        case "username":
                            if (tokens.Count != 0)
                                return "taken";
                            else
                                return "ok";
                        default:
                            return "error";
                    }
                }
                else
                    return "ok";
            }
            else return "incorrect";
        }

        public static Service service = new Service();
        public static IMongoDatabase mongodb = new Service.Mongo(System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString).db;
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink link = new HyperLink();
            link.CssClass = "button with-image home";
            link.Style.Add("margin-top", "35px");
            link.NavigateUrl = "/";
            link.Text = "На главную";
            page_top.FindControl("Content").Controls.Add(link);
        }

        protected void sign_beta_btn_Click(object sender, EventArgs e)
        {
            string emailRes = checkParam(email_txt.Text, "email", @"^([a-zA-Z0-9]+(?:[._-][a-zA-Z0-9]+)*)@([a-zA-Z0-9]+(?:[.-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,})$");
            string userRes = checkParam(login_txt.Text, "username", @"^(?=.*[a-zA-Z]{1,})(?=.*[\d]{0,})[a-zA-Z0-9]{3,15}$");
            string passRes = checkParam(_password.Text, "pass", @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,32}$");
            if (userRes == "ok" && passRes == "ok")
            {
                if (emailRes == "ok")
                {
                    string pass = _password.Text;
                    string windowKey = service.generateToken(32);
                    string token = service.generateToken(64);

                    BsonDocument t = new BsonDocument {
                        { "value", token },
                        { "action", "create_user" },
                        { "params", new BsonDocument {
                            { "display_name", login_txt.Text },
                            { "username", login_txt.Text.ToLower() },
                            { "about", BsonNull.Create(null) },
                            { "email",  email_txt.Text.ToLower() },
                            { "password", pass },
                            { "avatar", "/images/default/user-image.svg" },
                            { "don_window_key", windowKey },
                            { "exp", BsonInt32.Create(0) },
                            { "balance", new BsonDocument {
                                { "current", BsonDouble.Create(0) },
                                { "in_process", BsonDouble.Create(0) } } },
                            { "creation_date", service.adaptDate(DateTime.Now) },
                            { "is_premium", false },
                            { "type", BsonInt32.Create(0) } } },
                        { "date_kill", service.adaptDate(DateTime.Now.AddDays(1)) }
                    };
                    mongodb.GetCollection<BsonDocument>("tokens").InsertOne(t);

                    string pattern = service.LoadPattern(@"~/App_Data/signup.html", this, new List<string>() { Request.Url.Host + (Request.Url.Host == "localhost" ? ":" + Request.Url.Port.ToString() : ""), token });
                    Service.MailSender mail = new Service.MailSender("robot@streamerspace.ru", "ILoveTyumen72", "smtp.jino.ru", 25, false);
                    mail.sendMessage("robot@streamerspace.ru", null, email_txt.Text.ToLower(), "Регистрация на streamerspace.ru", pattern);

                    _regUpd.Update();
                    success_text.Text = string.Format("На почту <font style=\"font-family: 'PT Sans Bold'\">{0}</font> было отправлено письмо с дальнейшими инструкциями для регистрации. Введенные Вами логин и пароль зарезервированы на 1 день. Учтите, что новый токен на создание аккаунта с этими данным можно будет получить лишь ровно через такое же время.", email_txt.Text);
                    registration.Visible = false;
                    success.Visible = true;
                }
                else if (emailRes == "update needed")
                {
                    BsonDocument filter = new BsonDocument {
                        { "action", "create_user" },
                        { "params.email", email_txt.Text }
                    };
                    List<BsonDocument> tokens = mongodb.GetCollection<BsonDocument>("tokens").Find(filter).ToList();
                    if (tokens.Count != 0)
                    {
                        string token = service.generateToken(64);
                        mongodb.GetCollection<BsonDocument>("tokens").UpdateOne(new BsonDocument {
                        { "_id", tokens[0]["_id"] } }, new BsonDocument {
                            { "$set", new BsonDocument {
                                { "value", token },
                                { "date_kill", service.adaptDate(DateTime.Now.AddDays(1)) } } }
                        });

                        string pattern = service.LoadPattern(@"~/App_Data/signup.html", this, new List<string>() { Request.Url.Host + (Request.Url.Host == "localhost" ? ":" + Request.Url.Port.ToString() : ""), token });
                        Service.MailSender mail = new Service.MailSender("robot@streamerspace.ru", "ILoveTyumen72", "smtp.jino.ru", 587, false);
                        mail.sendMessage("robot@streamerspace.ru", null, email_txt.Text.ToLower(), "Регистрация на streamerspace.ru", pattern);

                        _regUpd.Update();
                        success_text.Text = string.Format("На почту <font style=\"font-family: 'PT Sans Bold'\">{0}</font> было отправлено письмо с дальнейшими инструкциями для регистрации. Введенные Вами логин и пароль зарезервированы на 1 день. Учтите, что новый токен на создание этого аккаунта Вы сможете получите ровно через тако же время.", email_txt.Text);
                        registration.Visible = false;
                        success.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "check", "checkEmail(document.getElementById('email_txt'))", true);
                }
            }
            else
            {
                string action = "checkLogin(document.getElementById('login_txt'));";
                action += " checkEmail(document.getElementById('email_txt'));";
                action += " checkPass();";

                ScriptManager.RegisterStartupScript(this, GetType(), "checkData", action, true);
            }
        }
    }
}