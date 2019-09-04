namespace ControlPanel.Domain
{
    public class AppSettings
    {
        public string ApiUrl { get; set; }
        public string BasicAuthUsername { get; set; }
        public string BasicAuthPassword { get; set; }
        public string EncryptionKey { get; set; }
    }
}
