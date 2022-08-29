namespace GPTAssistant
{
    using Microsoft.ML.OnnxRuntime;
    public class GPTAssistantSettings
    {
 
        public const string tokenizerBinPath = "./gpt2.bin";
        public const string tokenizerI2WPath = "./gpt2.i2w";
        public const string gptonnxPath = "./gpt2-lm-head-bs-12.onnx"; // Changing Model requires overloading OnnxRuntimeHandler.Predict()

        const int vocabSize = 50257;

        private BlingTokenizer tokenizer;
        private InferenceSession session;
        private OnnxRuntimeHandler onnxRuntime;

        public GPTAssistantSettings()
        {
            tokenizer = new BlingTokenizer(tokenizerBinPath, tokenizerI2WPath);
            session = new InferenceSession(gptonnxPath);
            onnxRuntime = new OnnxRuntimeHandler(gptonnxPath, vocabSize);
        }

        public BlingTokenizer Tokenizer
        {
            get { return tokenizer; }
        }

        public InferenceSession Session
        {
            get { return session; }
        }

        public OnnxRuntimeHandler OnnxRuntime
        {
            get { return onnxRuntime; }
        }
    }
}
