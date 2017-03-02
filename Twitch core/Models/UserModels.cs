using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitch_core.Models
{
    public struct Twitch
    {
        public BsonDocument channel;
        public BsonDocument user;
        public BsonDocument stream;
    }
    public class UserViewModel
    {
        public bool exist;
        public Twitch twitch;
    }
}