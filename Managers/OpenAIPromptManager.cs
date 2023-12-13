using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Plugins.Memory;
using System;

namespace test_generative_ai.Managers
{
    [Serializable]
    public class OpenAIPromptManager: IPromptManager
    {
        private readonly Kernel _kernel;
#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        private readonly ISemanticTextMemory _semanticTextMemory;
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        private readonly OpenAIPromptExecutionSettings _executionSettings;

#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public OpenAIPromptManager(Kernel kernel, OpenAIPromptExecutionSettings executionSettings, ISemanticTextMemory semanticTextMemory)
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        {
            _kernel = kernel;
            _semanticTextMemory = semanticTextMemory;
            _executionSettings = executionSettings;
        }

        public async Task<string> ChatResultFunctionAsync(KernelArguments argument)
        {

#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            MemoryQueryResult? lookup = await _semanticTextMemory.GetAsync("brain-station-23", "info1");
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            Console.WriteLine("Memory with key 'info1':" + lookup?.Metadata.Text ?? "ERROR: memory not found");

            const string skPrompt = @"
            ChatBot can have a conversation with you about any topic.
            It can give explicit instructions or say 'I don't know' if it does not have an answer.

            {{$history}}
            User: {{$input}}
            ChatBot:";

#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            _kernel.ImportPluginFromObject(new TextMemoryPlugin(_semanticTextMemory));
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            var chatFunction = _kernel.CreateFunctionFromPrompt(skPrompt, _executionSettings);

            //var textPromptResult = await _kernel.InvokeAsync(chatFunction);
           // _kernel.Plugins.Add(memoryPlugin);
           // var data = await memoryPlugin["Recall"].InvokeAsync(argument);

               var data = await chatFunction.InvokeAsync(_kernel, argument);

            return data.ToString();
        }
    }
}
