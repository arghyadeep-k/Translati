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

        // NOTE: Replace this example key with a valid subscription key.
        static string key = "";

        static string request="Hi";
        static string response_ = "";
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello");
        //    TranslateText();
        //    Console.WriteLine("Hello ended.");
        //    Console.Read();
        //}

        public string TranslateText(string req)
        {
            //var task = Translate(request);

            //string result = "";
            //result = task.Result.ToString();
            request = req;
            key = ConfigurationManager.AppSettings["API_KEY"];
            //RunAsync().RunSynchronously();
            Translate();
            //RunAsync().GetAwaiter().GetResult();
            return response_;
        }

        //private static async Task Translate()
        private static void Translate()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            //List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>() {
            //    new KeyValuePair<string, string> (request, "fr-fr"),
            //    //new KeyValuePair<string, string> ("Salut", "en-us")
            //};

            //foreach (KeyValuePair<string, string> i in list)
            //{
                string uri = host + path + "?to=" + "fr-fr" + "&text=" + System.Net.WebUtility.UrlEncode(request);
            //client.BaseAddress = new System.Uri(uri);
                HttpResponseMessage response = client.GetAsync(uri).Result;
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                response_ = XElement.Parse(content).Value.ToString();
            }
                //var content = await response.Content.ReadAsStringAsync();
                // NOTE: A successful response is returned in XML. You can extract the contents of the XML as follows.
                //string result = XElement.Parse(content).Value.ToString();
                //response_ = XElement.Parse(content).Value.ToString();
            // return request;
            //}
            //return null;
        }
    }
}