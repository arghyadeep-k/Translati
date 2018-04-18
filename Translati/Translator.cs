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

        public string TranslateText(string req)
        {            
            request = req;
            key = ConfigurationManager.AppSettings["API_KEY"];
            
            Translate();

            return response_;
        }
        
        private static void Translate()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
            
            string uri = host + path + "?to=" + "fr-fr" + "&text=" + System.Net.WebUtility.UrlEncode(request);            
            HttpResponseMessage response = client.GetAsync(uri).Result;
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                response_ = XElement.Parse(content).Value.ToString();
            }                
        }
    }
}