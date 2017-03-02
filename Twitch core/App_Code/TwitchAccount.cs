using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Twitch_core
{
    public class TwitchAccount
    {
        [BsonElement("id")]
        public int id;
        public TwitchAccount(BsonDocument rawData)
        {
            foreach (BsonElement elem in rawData.Elements.ToList())
            {
                switch (elem.Name)
                {
                    case "id":
                        id = elem.Value.AsInt32;
                        break;
                }
            }
        }
    }
}