﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Translati
{
    [JsonObject]
    public class AlexaRequest
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("session")]
        public SessionAttributes Session { get; set; }

        [JsonProperty("request")]
        public RequestAttributes Request { get; set; }

        [JsonObject("attributes")]
        public class SessionCustomAttributes
        {
            [JsonProperty("memberId")]
            public int MemberId { get; set; }
        }

        [JsonObject("session")]
        public class SessionAttributes
        {
            [JsonProperty("sessionId")]
            public string SessionId { get; set; }

            [JsonProperty("application")]
            public ApplicationAttributes Application { get; set; }

            [JsonProperty("attributes")]
            public SessionCustomAttributes Attributes { get; set; }

            [JsonProperty("user")]
            public UserAttributes User { get; set; }

            [JsonProperty("new")]
            public bool New { get; set; }

            [JsonObject("application")]
            public class ApplicationAttributes
            {
                [JsonProperty("applicationId")]
                public string ApplicationId { get; set; }
            }

            [JsonObject("user")]
            public class UserAttributes
            {
                [JsonProperty("userId")]
                public string UserId { get; set; }

                [JsonProperty("accessToken")]
                public string AccessToken { get; set; }
            }
        }

        [JsonObject("request")]
        public class RequestAttributes
        {
            private string _timestampEpoch;
            private double _timestamp;

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("requestId")]
            public string RequestId { get; set; }

            [JsonProperty("timestamp")]
            public string TimestampEpoch
            {
                get
                {
                    return _timestampEpoch;
                }
                set
                {
                    _timestampEpoch = value;

                    if (Double.TryParse(value, out _timestamp) && _timestamp > 0)
                        Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(_timestamp);
                    else
                    {
                        var timeStamp = DateTime.MinValue;
                        if (DateTime.TryParse(_timestampEpoch, out timeStamp))
                            Timestamp = timeStamp.ToUniversalTime();
                    }
                }
            }

            [JsonIgnore]
            public DateTime Timestamp { get; set; }

            [JsonProperty("intent")]
            public IntentAttributes Intent { get; set; }

            [JsonProperty("reason")]
            public string Reason { get; set; }

            public RequestAttributes()
            {
                Intent = new IntentAttributes();
            }

            [JsonObject("intent")]
            public class IntentAttributes
            {
                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("slots")]
                public SlotAttributes Slots { get; set; }                                
            }

            [JsonObject("slots")]
            public class SlotAttributes
            {
                [JsonProperty("phrase")]
                public PhraseAttributes Phrase { get; set; }

                [JsonProperty("language")]
                public LangAttributes Language { get; set; }

            }

            [JsonObject("phrase")]
            public class PhraseAttributes
            {
                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("value")]
                public string Value { get; set; }
            }

            [JsonObject("language")]
            public class LangAttributes
            {
                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("value")]
                public string Value { get; set; }
            }
        }
    }
}