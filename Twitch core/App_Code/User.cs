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
    public class User
    {
        public int user_id, exp, vk_id;
        public string display_name, username, email, password, avatar, about, twitch_username, youtube_channel;
        public Balance balance = new Balance(0, 0);
        public DateTime creation_date, premium_expire;
        public short type;
        public class Balance
        {
            public double current, in_process;
            public Balance(double current, double in_process)
            {
                this.current = current;
                this.in_process = in_process;
            }
        }
        public User(int user_id, string display_name, string username, string email, string password, string avatar, 
            int exp, double balance_current, double balance_in_process, DateTime creation_date, DateTime premium_expire, 
            short type, string about, string twitch_username, int vk_id, string youtube_channel)
        {
            this.user_id = user_id;
            this.display_name = display_name;
            this.username = username;
            this.email = email;
            this.password = password;
            this.avatar = avatar;
            this.exp = exp;
            balance.current = balance_current;
            balance.in_process = balance_in_process;
            this.creation_date = creation_date;
            this.premium_expire = premium_expire;
            this.type = type;
            this.about = about;
            this.twitch_username = twitch_username;
            this.vk_id = vk_id;
            this.youtube_channel = youtube_channel;
        }
        /*Service service = new Service();
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId id;
        [BsonElement("username")]
        public string username;
        [BsonElement("email")]
        public string email;
        [BsonElement("password")]
        public string password;
        [BsonElement("twitch_account")]
        public BsonDocument twitch_get {
            get { return twitch_bson; }
            set {
                twitch = new TwitchAccount(value);
            }
        }
        public TwitchAccount twitch;
        [BsonElement("hitbox_account")]
        public BsonDocument hitbox_bson
        {
            get { return hitbox_bson; }
            set
            {
                hitbox_bson = value;
                hitbox = new HitboxAccount();
                foreach (BsonElement elem in value.Elements.ToList())
                {
                    switch (elem.Name)
                    {
                        case "id":
                            hitbox.id = elem.Value.AsInt32;
                            break;
                    }
                }
            }
        }
        public HitboxAccount hitbox;
        [BsonElement("donation_window_key")]
        public string donation_window_key;*/
    }
}