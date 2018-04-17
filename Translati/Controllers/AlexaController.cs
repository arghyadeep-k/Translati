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
        [HttpPost, Route("api/translati/demo")]
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
            //return new
            //{
            //    version = "1.0",
            //    sessionAttributes = new { },
            //    response = new
            //    {
            //        outputSpeech = new
            //        {
            //            type = "PlainText",
            //            text = "Hello World from Alexa!"
            //        },
            //        card = new
            //        {
            //            type = "Simple",
            //            title = "Calci",
            //            content = "Hello World from Alex!"
            //        },
            //        shouldEndSession = true
            //    }
            //};        
        }

        private AlexaResponse LaunchRequestHandler(AlexaRequest request)
        {
            var response = new AlexaResponse("Welcome to Translati. Say a word or phrase and I'll translate it for you.");
            //response.Session.MemberId = request.MemberId;
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
                    response = Translate(request.Request.Intent.Slots.Phrase.Value.ToString()); 
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
            var response = new AlexaResponse("To use Translati, you can say, Alexa, add one and 2 or subtract 3 from 5. You can also say, Alexa, stop or Alexa, cancel, at any time to exit from Calcy skill. For now, do you want to calculate anything?", false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one, top courses or new courses?";
            return response;
        }

        private AlexaResponse CancelOrStopIntentHandler(AlexaRequest request)
        {
            return new AlexaResponse("Thanks for using Translati. Have a nice day.", true);
        }

        //private AlexaResponse Calculate(AlexaRequest request, int code)
        //{
        //    var output = new StringBuilder();
        //    if (code == 1)
        //    {
        //        double result = request.Request.Intent.GetSlot(0) + request.Request.Intent.GetSlot(1);
        //        output.AppendLine("The answer is " + result.ToString());
        //    }
        //    else if (code == 2)
        //    {
        //        double result = request.Request.Intent.GetSlot(0) - request.Request.Intent.GetSlot(1);
        //        output.AppendLine("The answer is " + result.ToString());
        //    }

        //    return new AlexaResponse(output.ToString());
        //}

        private AlexaResponse Translate(string request)
        {
            //var output = new StringBuilder();
            //..output.AppendLine("I translated this text from " + request);

            string result = null;
            var obj = new Translator();
            result = obj.TranslateText(request);
            
            //result = request;
            if (result.Equals(""))
                result = "Sorry, Translati failed to translate right now.";
            var response = new AlexaResponse(result, true);
            return response;
            //return new AlexaResponse(result, false);

        }
        private AlexaResponse SessionEndedRequestHandler(AlexaRequest request)
        {
            return null;
        }
    }
}
