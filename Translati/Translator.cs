using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace Translati
{
    public class Translator
    {
        static string host = "https://api.microsofttranslator.com";
        static string path = "/V2/Http.svc/Translate";
        
        static string key = "";

        static string request="";
        static string response_ = "";       

        public string TranslateText(string req, string language)
        {            
            request = req;
            key = ConfigurationManager.AppSettings["API_KEY"];

            DataAccess d = new DataAccess();
            string abbr = d.Fetch(language);

            Translate(abbr);

            return response_;
        }
        
        private static void Translate(string abbr)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
            
            string uri = host + path + "?to=" + abbr + "&text=" + System.Net.WebUtility.UrlEncode(request);            
            HttpResponseMessage response = client.GetAsync(uri).Result;
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                response_ = XElement.Parse(content).Value.ToString();
            }                
        }
    }
}