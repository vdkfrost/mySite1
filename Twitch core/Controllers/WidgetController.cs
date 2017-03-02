using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitch_core.Controllers
{
    public class WidgetController : Controller
    {
        Service service = new Service();
        IMongoDatabase mongodb = new Service.Mongo(System.Configuration.ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString).db;
        public ActionResult Show(string id)
        {
            if (id.Length == 64)
            {
                List<BsonDocument> widget = mongodb.GetCollection<BsonDocument>("widgets").Find(new BsonDocument { { "identity", id.ToLower() } }).Limit(1).ToList();
                if (widget.Count == 0)
                    ViewBag.error = "Виджет не найден";
                else
                    ViewBag.widget = widget;
            }
            else
                ViewBag.error = "Виджет не найден";
            return View("Show_widget");
        }
    }
}
