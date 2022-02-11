﻿namespace Retro_Achievement_Tracker
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    public class UserSummaryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            UserSummary UserSummary = new UserSummary();

            if (existingValue != null)
            {
                UserSummary = (UserSummary)existingValue;
            }

            JToken RecentAchievements = item["RecentAchievements"];
            JToken LastGameID = item["LastGameID"];
            JToken UserPic = item["UserPic"];
            JToken Rank = item["Rank"];
            JToken TotalPoints = item["TotalPoints"];
            JToken TotalTruePoints = item["TotalTruePoints"];

            if (RecentAchievements != null)
            {
                if (RecentAchievements.Type == JTokenType.Object)
                {
                    UserSummary.Achievements = new List<Achievement>();

                    foreach (JToken jobject in RecentAchievements.Children<JToken>())
                    {
                        foreach (JToken jobjectJr in jobject.Children<JToken>())
                        {
                            foreach (JToken jobjectIII in jobjectJr.Children<JToken>())
                            {
                                foreach (JToken jobjectIV in jobjectIII.Children<JToken>())
                                {
                                    UserSummary.Achievements.Add(jobjectIV.ToObject<Achievement>());
                                }
                            }
                        }
                    }
                }
            }

            if (LastGameID != null)
            {
                if (LastGameID.Type == JTokenType.String)
                {
                    UserSummary.LastGameID = Convert.ToInt32(LastGameID);
                }
            }

            if (UserPic != null)
            {
                if (UserPic.Type == JTokenType.String)
                {
                    UserSummary.UserPic = UserPic.ToString();
                }
            }

            if (TotalPoints != null)
            {
                if (TotalPoints.Type == JTokenType.String)
                {
                    UserSummary.TotalPoints = Convert.ToInt32(TotalPoints);
                }
            }

            if (TotalTruePoints != null)
            {
                if (TotalTruePoints.Type == JTokenType.String)
                {
                    UserSummary.TotalTruePoints = Convert.ToInt32(TotalTruePoints);
                }
            }

            if (Rank != null)
            {
                if (Rank.Type == JTokenType.Integer)
                {
                    UserSummary.Rank = Convert.ToInt32(Rank);
                }
            }
            return UserSummary;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}