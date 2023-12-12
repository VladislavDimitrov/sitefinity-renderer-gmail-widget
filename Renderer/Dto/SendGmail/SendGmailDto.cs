using Newtonsoft.Json;

namespace Renderer.Dto.SendGmail
{
    public class SendGmailDto
    {
        [JsonProperty("recipients")]
        public RecipientInfo[] Recipients { get; set; }

        [JsonProperty("emailSubject")]
        public string EmailSubject { get; set; }

        [JsonProperty("emailBody")]
        public string EmailBody { get; set; }

        public class RecipientInfo 
        {
            [JsonProperty("emailAddress")]
            public string EmailAddress { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class SenderInfo
        {
            [JsonProperty("emailAddress")]
            public string EmailAddress { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
