using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using MongoDB.Driver;
using System.Security.Authentication;
using System.Threading;
using Npgsql;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Twitch_core
{
    public class Service
    {
        public class MailSender
        {
            public SmtpClient smtpClient;
            public MailSender(string login, string password, string smtpServerName, int smtpServerPort, bool enableSsl = true)
            {
                smtpClient = new SmtpClient(smtpServerName, smtpServerPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(login, password);
                smtpClient.Timeout = 0;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = enableSsl;
            }
            public void sendMessage(string sender_address, string sender_display_name, string receiver, string subject, string text)
            {
                MailMessage mail = new MailMessage();
                if (sender_display_name != null)
                    mail.From = new System.Net.Mail.MailAddress(sender_address, sender_display_name);
                else
                    mail.From = new System.Net.Mail.MailAddress(sender_address);
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Subject = subject;
                mail.Body = text;
                mail.BodyEncoding = Encoding.UTF8;
                mail.To.Add(receiver);
                smtpClient.Send(mail);
            }
        }
        public class Mongo
        {
            public IMongoDatabase db;
            public Mongo(string host, int port, string username, string password, string dbName, bool useSsl = false)
            {
                MongoClientSettings settings = new MongoClientSettings();
                settings.Server = new MongoServerAddress(host, port);
                if (useSsl)
                {
                    settings.UseSsl = true;
                    settings.SslSettings = new SslSettings();
                    settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
                }
                else
                    settings.UseSsl = false;

                MongoIdentity identity = new MongoInternalIdentity(dbName, username);
                MongoIdentityEvidence evidence = new PasswordEvidence(password);
                settings.Credentials = new List<MongoCredential>() { new MongoCredential("SCRAM-SHA-1", identity, evidence) };

                db = new MongoClient(settings).GetDatabase(dbName);
            }
            public Mongo(string connectionString, bool useSsl = false)
            {
                string[] parameteres = connectionString.Split(new char[] { ':', '@', '/' });
                string username = parameteres[0];
                string password = parameteres[1];
                string host = parameteres[2];
                int port = Convert.ToInt32(parameteres[3]);
                string dbName = parameteres[4];

                MongoClientSettings settings = new MongoClientSettings();
                settings.Server = new MongoServerAddress(host, port);
                if (useSsl)
                {
                    settings.UseSsl = true;
                    settings.SslSettings = new SslSettings();
                    settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
                }
                else
                    settings.UseSsl = false;

                MongoIdentity identity = new MongoInternalIdentity(dbName, username);
                MongoIdentityEvidence evidence = new PasswordEvidence(password);
                settings.Credentials = new List<MongoCredential>() { new MongoCredential("SCRAM-SHA-1", identity, evidence) };

                db = new MongoClient(settings).GetDatabase(dbName);
            }
        }
        public class Postgre
        {
            public NpgsqlConnection connection = new NpgsqlConnection();
            public Postgre(string connection)
            {
                this.connection.ConnectionString = connection;
            }
            public async Task<List<List<object>>> ReadAsync(string query, List<KeyValuePair<string, string>> parameteres = null, byte readType = 0)
            {
                List<List<object>> data = new List<List<object>>();
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                if (parameteres != null)
                    foreach (var pair in parameteres)
                        command.Parameters.AddWithValue(pair.Key, pair.Value);
                await connection.OpenAsync();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    switch (readType)
                    {
                        case 0: // all
                            while (await reader.ReadAsync())
                            {
                                List<object> found = new List<object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (!reader.IsDBNull(i))
                                        found.Add(reader.GetValue(i));
                                    else
                                        found.Add(null);
                                }
                                data.Add(found);
                            }
                            break;
                        case 1: // 1
                            if (await reader.ReadAsync())
                            {
                                List<object> found = new List<object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (!reader.IsDBNull(i))
                                        found.Add(reader.GetValue(i));
                                    else
                                        found.Add(null);
                                }
                                data.Add(found);
                            }
                            break;
                    }

                }
                connection.Close();
                return data;
            }
            public object Read(string query, List<KeyValuePair<string, string>> parameteres = null, byte readType = 0)
            {
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                if (parameteres != null)
                    foreach (var pair in parameteres)
                        command.Parameters.AddWithValue(pair.Key, pair.Value);

                BsonDocument found = new BsonDocument();
                connection.Open();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    switch (readType)
                    {
                        case 0: // all
                            List<BsonDocument> data = new List<BsonDocument>();
                            while (reader.Read())
                            {
                                found = new BsonDocument();
                                for (int i = 0; i < reader.FieldCount; i++)
                                    if (!reader.IsDBNull(i))
                                        found.Add(new BsonElement(reader.GetName(i), reader.GetValue(i).ToBson()));
                                //found = ReworkBson(found);
                                data.Add(found);
                            }
                            connection.Close();
                            return data;
                        case 1: // 1
                            found = new BsonDocument();
                            if (reader.Read())
                                for (int i = 0; i < reader.FieldCount; i++)
                                    if (!reader.IsDBNull(i))
                                        found.Add(new BsonElement(reader.GetName(i), BsonValue.Create(reader.GetValue(i))));
                            //found = ReworkBson(found);
                            connection.Close();
                            return found;
                    }

                }
                return null;
            }
        }
        public static BsonDocument ReworkBson(BsonDocument doc)
        {
            BsonDocument result = new BsonDocument();
            for (int i = 0; i < doc.ElementCount; i++)
            {
                int iter = 0;
                string[] parts = doc.GetElement(i).Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    BsonDocument p = new BsonDocument(parts[parts.Length - 1], doc.GetElement(i).Value);
                    for (int k = parts.Length - 2; k > 0; k--)
                        p = new BsonDocument(parts[k], p);
                    BsonElement temp = new BsonElement(parts[0], p);
                    
                }
                else
                    result.Add(doc.GetElement(i));
                
            }

            return result;
        }
        public List<KeyValuePair<string, string>> resolve_token(string token, SqlConnection connection)
        {
            List<KeyValuePair<string, string>> actionResult = new List<KeyValuePair<string, string>>();
            SqlCommand getToken = new SqlCommand("SELECT * FROM tokens.main WHERE value = @value DELETE FROM tokens.main WHERE value = @value", connection);
            getToken.Parameters.AddWithValue("@value", token.ToLower());
            int type = -1;
            string parameteres = "";
            connection.Open();
            using (SqlDataReader reader = getToken.ExecuteReader())
                if (reader.Read())
                {
                    type = reader.GetInt32(2);
                    parameteres = reader.GetString(3);
                }
            connection.Close();

            switch (type)
            {
                // Подтверждение регистрации пользователя
                case 0:
                    string[] info = parameteres.Split(new string[] { "◘" }, StringSplitOptions.RemoveEmptyEntries);
                    string email = "";
                    string f_name = "", s_name = "";
                    SqlCommand createUser = new SqlCommand("", connection);
                    string command = "insert into users.main (";
                    for (int i = 0; i < info.Length; i++)
                    {
                        string[] param = info[i].Split('•');
                        command += "[" + param[0] + "], ";
                        createUser.Parameters.AddWithValue("@" + param[0], param[1]);
                        switch (param[0])
                        {
                            case "f_name":
                                f_name = param[1];
                                break;
                            case "s_name":
                                s_name = param[1];
                                break;
                            case "login":
                                actionResult.Add(new KeyValuePair<string, string>("user_login", param[1]));
                                break;
                            case "email":
                                break;
                            case "sex":
                                break;
                            case "type":
                                actionResult.Add(new KeyValuePair<string, string>("user_type", param[1]));
                                break;
                            case "avatar":
                                actionResult.Add(new KeyValuePair<string, string>("user_image", param[1]));
                                break;
                        }
                    }
                    command = command.Remove(command.Length - 2) + ") VALUES (";
                    for (int i = 0; i < info.Length; i++)
                        command += "@" + info[i].Split('•')[0] + ", ";
                    command = command.Remove(command.Length - 2) + ")";
                    createUser.CommandText = command;
                    connection.Open();
                    createUser.ExecuteNonQuery();

                    int id = 0;
                    SqlCommand getCreated = new SqlCommand("SELECT [user_id] FROM [users].main WHERE email = @email", connection);
                    getCreated.Parameters.AddWithValue("@email", email);
                    using (SqlDataReader reader = getCreated.ExecuteReader())
                        if (reader.Read())
                            id = reader.GetInt32(0);
                    connection.Close();
                    actionResult.Add(new KeyValuePair<string, string>("user_id", id.ToString()));
                    actionResult.Add(new KeyValuePair<string, string>("user_name", s_name + " " + f_name));
                    actionResult.Add(new KeyValuePair<string, string>("message", f_name + ", Вы были успешно зарегистрированы, добро пожаловать!"));
                    break;
                default:
                    actionResult.Add(new KeyValuePair<string, string>("error", "Введенный токен не был найден."));
                    break;
            }
            return actionResult;
        }
        public void load_default_data(Page target)
        {
            target.Session["user_id"] = 1;
            target.Session["user_name"] = "Владислав Кибенко";
            target.Session["user_image"] = "Content/Images/Avatars/unknown_user.svg";
            target.Session["user_login"] = "vdkfrost";
            target.Session["user_type"] = 0;
        }
        public string generateToken(int length)
        {
            Random rand = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
                result += Convert.ToString(rand.Next(0, 16), 16);
            Thread.Sleep(50);
            return result;
        }
        public void delayedRedir(string target, int delay, Page page)
        {
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('" + target + "') }, " + delay.ToString() + ");", true);
        }
        public string LoadPattern(string patternPath, Page target, List<string> values)
        {
            return strFormat(System.IO.File.ReadAllText(target.Server.MapPath(patternPath)), values.ToArray());
        }
        public string adaptDate(DateTime date, bool withTime = true)
        {
            string result = string.Format("{0:00}.{1:00}.{2:0000}", date.Day, date.Month, date.Year);
            if (withTime)
                result += string.Format(" {0:00}:{1:00}", date.Hour, date.Minute);
            return result;
        }
        public string strFormat(string text, string[] values)
        {
            for (int i = 0; i < values.Length; i++)
                text = text.Replace("{" + i.ToString() + "}", values[i]);
            return text;
        }
        /// <summary>
        /// Привод дату к определенному строковому виду
        /// </summary>
        /// <param name="input">Дата</param>
        /// <param name="cut">Обрезание месяца до 3х символов</param>
        /// <param name="case_type">1 - верхний регистр, 2 - нижний регистр</param>
        /// <returns></returns>
        public string formatDate(DateTime input, int case_type = 0, bool deleteZeros = false, bool cutYear = false, bool cutMonth = false, bool cutTime = false)
        {
            string result = (deleteZeros ? Convert.ToInt32(input.Day).ToString() : input.Day.ToString()) + " ";
            string month = "";
            switch (input.Month)
            {
                case 1:
                    month = "Января";
                    break;
                case 2:
                    month = "Февраля";
                    break;
                case 3:
                    month = "Марта";
                    break;
                case 4:
                    month = "Апреля";
                    break;
                case 5:
                    month = "Мая";
                    break;
                case 6:
                    month = "Июня";
                    break;
                case 7:
                    month = "Июля";
                    break;
                case 8:
                    month = "Августа";
                    break;
                case 9:
                    month = "Сентября";
                    break;
                case 10:
                    month = "Октября";
                    break;
                case 11:
                    month = "Ноября";
                    break;
                default:
                    month = "Декабря";
                    break;
            }
            switch (case_type)
            {
                case 1:
                    month = month.ToUpper();
                    break;
                case 2:
                    month = month.ToLower();
                    break;
            }
            if (cutMonth)
                month = month.Substring(0, 3);
            result += month + (cutYear ? "" : " " + input.Year.ToString()) + (cutTime ? "" : " " + input.Hour + ":" + input.Minute);
            return result;
        }
        /// <summary>
        /// Привод дату к определенному строковому виду
        /// </summary>
        /// <param name="input">Дата</param>
        /// <param name="cut">Обрезание месяца до 3х символов</param>
        /// <param name="case_type">1 - верхний регистр, 2 - нижний регистр</param>
        /// <returns></returns>
        public string formatDate(string input, int case_type = 0, bool deleteZeros = false, bool cutYear = false, bool cutMonth = false, bool cutTime = false)
        {
            string[] parts = new string[5];
            int i = 0;
            foreach (string p in input.Split(new char[] { '.', ' ', ':' }))
            {
                parts[i] = p;
                i++;
            }
            string result = (deleteZeros ? Convert.ToInt32(parts[0]).ToString() : parts[0]) + " ";
            string month = "";
            switch (Convert.ToInt32(parts[1]))
            {
                case 1:
                    month = "Января";
                    break;
                case 2:
                    month = "Февраля";
                    break;
                case 3:
                    month = "Марта";
                    break;
                case 4:
                    month = "Апреля";
                    break;
                case 5:
                    month = "Мая";
                    break;
                case 6:
                    month = "Июня";
                    break;
                case 7:
                    month = "Июля";
                    break;
                case 8:
                    month = "Августа";
                    break;
                case 9:
                    month = "Сентября";
                    break;
                case 10:
                    month = "Октября";
                    break;
                case 11:
                    month = "Ноября";
                    break;
                default:
                    month = "Декабря";
                    break;
            }
            switch (case_type)
            {
                case 1:
                    month = month.ToUpper();
                    break;
                case 2:
                    month = month.ToLower();
                    break;
            }
            if (cutMonth)
                month = month.Substring(0, 3);
            result += month + (cutYear ? "" : " " + parts[2]) + (cutTime ? "" : " " + parts[3] + ":" + parts[4]);
            return result;
        }
        public int dateSubstract(DateTime first, DateTime second, byte returnType = 0)
        {
            long val1 = first.Year * 365 * 24 * 60 + first.Month * 30 * 24 * 60 + first.Day * 24 * 60 + first.Hour * 60 + first.Minute;
            long val2 = second.Year * 365 * 24 * 60 + second.Month * 30 * 24 * 60 + second.Day * 24 * 60 + second.Hour * 60 + second.Minute;
            return (int)(val1 - val2);
        }
    }
}
