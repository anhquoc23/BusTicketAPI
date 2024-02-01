namespace BusApi.Configs
{
    public class CloudinaryConfig
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public static string path = "Bus Ticket/avatar";

        public CloudinaryConfig() { }

        public CloudinaryConfig(string cloudName, string apiKey, string apiSecret)
        {
            CloudName = cloudName;
            this.ApiKey = apiKey;
            this.ApiSecret = apiSecret;
        }

    }
}
