namespace GPTAssistant
{
    using Microsoft.ML.OnnxRuntime;
    using Microsoft.ML.OnnxRuntime.Tensors;

    public class OnnxRuntimeHandler
    {
        public InferenceSession inferencesession;
        int vocab_size = 0;
        public OnnxRuntimeHandler(string modelPath, int vocab_size)
        {
            inferencesession = new InferenceSession(modelPath);
            this.vocab_size = vocab_size;
        }

        public int[] Predict(int[] input_ids, int newtokens)
        {
            // Setup tensors from input arrays.
            Tensor<Int64> input_tensor = new DenseTensor<Int64>(new[] {1,input_ids.Length});
            Tensor<float> attention_mask = new DenseTensor<float>(new[] { 1, input_ids.Length });

            for (int i = 0; i < input_ids.Length; i++)
            {
                input_tensor[0, i] = i < input_ids.Length ? input_ids[i] : 0;
                attention_mask[0, i] = 1; //REWORK THIS TO ALLOW FOR BATCHES.
            }

            //Tensor determining the length of created text.
            Tensor<Int64> out_token_num = new DenseTensor<Int64>(new[] { 1 });
            out_token_num[0] = newtokens;

            // Setup inputs for OnnxRuntime from tensors.
            var model_inputs = new List<NamedOnnxValue>()
            {
                NamedOnnxValue.CreateFromTensor("input_ids", input_tensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", attention_mask),
                NamedOnnxValue.CreateFromTensor("out_token_num", out_token_num)
            };

            // Running OnnxRuntime.
            var model_outputs = inferencesession.Run(model_inputs);

            // Output post-processing.
            int[] model_outputs_int_array = Array.ConvertAll(model_outputs.ToArray()[0].AsTensor<Int64>().ToArray<Int64>(), item => (int)item);

            int last_i = input_ids.Length;
            int j = last_i + input_ids.Length;
            for (; j < model_outputs_int_array.Length; last_i++, j++)
            {
                int[] sub_array = model_outputs_int_array[last_i..j];
                if (input_ids.SequenceEqual(sub_array))
                    break;
            }
            return model_outputs_int_array[..last_i];
        }
    }
}
