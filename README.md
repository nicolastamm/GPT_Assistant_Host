# GPT_Assistant_Host

.Net Core App that handles text generations tasks

## Dependencies:
1. BlingFire: https://github.com/microsoft/BlingFire
2. OnnxRuntime: https://github.com/microsoft/onnxruntime
3. GPT2 Model with Beam Search: https://github.com/onnx/models/tree/main/text/machine_comprehension/gpt2-bs

## Usage:

__GET__ (URL)/GPTAssistant/PredictOutput?input=\_\_\_\_&newtokens=\_\_\_\_
- input ... text to be completed.
- newtokens ... length of maximum added text in tokens. (default: 15)
   
