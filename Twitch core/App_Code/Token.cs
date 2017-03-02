﻿using MongoDB.Bson;
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
    public class Token
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId _id;
        [BsonElement("value")]
        public string value;
        [BsonElement("action")]
        public string action;
        [BsonElement("params")]
        public string parameteres;
        [BsonElement("date_kill")]
        public string date_kill;
    }
}