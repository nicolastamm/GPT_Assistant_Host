using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GPTAssistant;

namespace GPT_Assistant_Host.Controllers
{
    public class GPTAssistantController : Controller
    {
        private readonly GPTAssistantSettings _settings;
        public GPTAssistantController(IOptions<GPTAssistantSettings> settingsOptions)
        {
            _settings = settingsOptions.Value;
        }

        //
        // GET: /GPTAssistant/PredictOutput?input='your input'&newtokens='amount of new tokens wanted'"
        public string PredictOutput(string input, int newtokens = 15)
        {
            if (input == null)
                return "Usage: .../GPTAssistant/PredictOutput?input='your input'&newtokens='amount of new tokens wanted'";

            int[] inputIds = _settings.Tokenizer.Tokenize(input);
            int[] outputIds = _settings.OnnxRuntime.Predict(inputIds, newtokens); // The Predict function is ATM highly model specific!!!
            string newTestString = _settings.Tokenizer.DeTokenize(outputIds);

            return newTestString;
        }

        // 
        // GET: /GPTAssistant/
        public string Index()
        {
            return "Usage: .../GPTAssistant/PredictOutput?input='your input'&newtokens='amount of new tokens wanted'";
        }
    }
}
