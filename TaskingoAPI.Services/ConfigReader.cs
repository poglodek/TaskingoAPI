using System.IO;
using Newtonsoft.Json;

namespace TaskingoAPI.Services
{
    public class ConfigReader
    {
        public string ServiceMail { get; set; }
        public string ServiceMailPassword { get; set; }
        public string ServiceMailServer { get; set; }
        public int ServiceMailServerPort { get; set; }

        public ConfigReader()
        {
            if (!File.Exists("Config.json")) SetDefault();
            var json = File.ReadAllText("Config.json");
            var settings = JsonConvert.DeserializeObject<ConfigReader>(json);
            ServiceMail =  settings.ServiceMail;
            ServiceMailPassword =  settings.ServiceMailPassword;
            ServiceMailServer =  settings.ServiceMailServer;
            ServiceMailServerPort =  settings.ServiceMailServerPort;
        }
        public void SetDefault()
        {
            ServiceMail = "YourEmail@email.com";
            ServiceMailPassword = "Pa$$w0rd1234.";
            ServiceMailServer = "smtp.poczta.onet.pl";
            ServiceMailServerPort = 465;

            var json = JsonConvert.SerializeObject(this);

            File.WriteAllText("Config.json", json);

        }
    }
}
