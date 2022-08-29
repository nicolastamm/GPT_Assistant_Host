namespace GPTAssistant
{
    using BlingFire;
    public class BlingTokenizer
    {
        public string _tokenizerBinPath, _tokenizerI2WPath;
        public ulong _tokenizerBINHandle, _tokenizerI2WHandle;

        public BlingTokenizer(string tokenizerBinPath, string tokenizerI2WPath)
        {
            _tokenizerBinPath = tokenizerBinPath;
            _tokenizerI2WPath = tokenizerI2WPath;
            _tokenizerBINHandle = 0;
            _tokenizerI2WHandle = 0;
            LoadTokenizer();
        }

        public void LoadTokenizer()
        {
            System.Console.WriteLine($"Loading tokenizer .bin file from {_tokenizerBinPath}");

            if (File.Exists(_tokenizerBinPath))
            {
                try
                {
                    _tokenizerBINHandle = BlingFireUtils.LoadModel(_tokenizerBinPath);
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("{0} Exception caught.", e);
                }
            }
            else
            {
                System.Console.WriteLine("Tokenizer .bin file not found");
            }

            System.Console.WriteLine($"Loading tokenizer .i2w file from {_tokenizerI2WPath}");

            if (File.Exists(_tokenizerI2WPath))
            {
                try
                {
                    _tokenizerI2WHandle = BlingFireUtils.LoadModel(_tokenizerI2WPath);
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("{0} Exception caught.", e);
                }
            }
            else
            {
                System.Console.WriteLine("Tokenizer .i2w file not found");
            }
        }

        public int[] Tokenize(string input_str)
        {
            byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(input_str);
            int[] ids = new int[128];
            int outputCount = BlingFireUtils.TextToIds(_tokenizerBINHandle, inBytes, inBytes.Length, ids, 8, -100);
            ids = ids.Take(outputCount).ToArray();
            return ids;
        }

        public string DeTokenize(int[] input_tokens)
        {
            return BlingFireUtils.IdsToText(_tokenizerI2WHandle,input_tokens);
        }
    }
}
