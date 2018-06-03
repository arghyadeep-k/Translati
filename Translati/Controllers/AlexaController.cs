using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Translati.Controllers
{
    public class AlexaController : ApiController
    {
        [HttpPost, Route("api/translati")]
        public dynamic Translator(AlexaRequest request)
        {
            AlexaResponse response = null;

            switch (request.Request.Type)
            {
                case "LaunchRequest":
                    response = LaunchRequestHandler(request);
                    break;
                case "IntentRequest":
                    response = IntentRequestHandler(request);
                    break;
                case "SessionEndedRequest":
                    response = SessionEndedRequestHandler(request);
                    break;
            }
            return response;                
        }

        private AlexaResponse LaunchRequestHandler(AlexaRequest request)
        {
            var response = new AlexaResponse("Welcome to Translati. Say a word or phrase and I'll translate it for you.");           
            response.Response.Card.Title = "Translati";
            response.Response.Card.Content = "Welcome to Translati. Say a word or phrase and I'll translate it for you.";
            response.Response.Reprompt.OutputSpeech.Text = "Please pick one, Top Courses or New Courses?";
            response.Response.ShouldEndSession = false;
            //Deal with reprompt later
            return response;
        }

        private AlexaResponse IntentRequestHandler(AlexaRequest request)
        {
            AlexaResponse response = null;

            switch (request.Request.Intent.Name.ToString())
            {
                case "Translati":
                case "translati":
                    response = Translate(request.Request.Intent.Slots.Phrase.Value.ToString(), request.Request.Intent.Slots.Language.Value.ToString()); 
                    break;                
                case "AMAZON.CancelIntent":
                case "AMAZON.StopIntent":
                    response = CancelOrStopIntentHandler(request);
                    break;
                case "AMAZON.HelpIntent":
                    response = HelpIntent(request);
                    break;
            }

            return response;
        }

        private AlexaResponse HelpIntent(AlexaRequest request)
        {
            var response = new AlexaResponse("To use Translati, you can say, Alexa, ask Translati to translate Hello World. You can also say, Alexa, stop Translati or Alexa, cancel Translati, at any time to exit from the skill. For now, do you want to translate anything?", false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one, top courses or new courses?";
            return response;
        }

        private AlexaResponse CancelOrStopIntentHandler(AlexaRequest request)
        {
            return new AlexaResponse("Thanks for using Translati. Have a nice day.", true);
        }
        
        private AlexaResponse Translate(string request, string language)
        {            
            string result = null;
            var obj = new Translator();
            result = obj.TranslateText(request, language);
                        
            if (result.Equals("") || result.Equals(null))
                result = "Sorry, Translati failed to translate right now.";
            var response = new AlexaResponse(result, true);

            return response;              
        }

        private AlexaResponse SessionEndedRequestHandler(AlexaRequest request)
        {
            return null;
        }
    }
}
