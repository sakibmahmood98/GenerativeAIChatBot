using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace test_generative_ai.Managers
{
    [Serializable]
    public class OpenAIPromptManager: IPromptManager
    {
        private readonly Kernel _kernel;
#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        private readonly SemanticTextMemory _semanticTextMemory;
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        private readonly OpenAIPromptExecutionSettings _executionSettings;

#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public OpenAIPromptManager(Kernel kernel, OpenAIPromptExecutionSettings executionSettings, SemanticTextMemory semanticTextMemory)
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        {
            _kernel = kernel;
            _semanticTextMemory = semanticTextMemory;
            _executionSettings = executionSettings;
        }

        public async Task<string> ChatResultFunctionAsync(KernelArguments argument)
        {
#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
       //     IMemoryStore store = await SqliteMemoryStore.ConnectAsync("memories.sqlite");
#pragma warning restore SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

           // await _semanticTextMemory.SaveInformationAsync("brain-station-23", id: "info1", text: "My name is Andrea");

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            //var memoryPlugin = _kernel.ImportPluginFromObject(new TextMemoryPlugin(_semanticTextMemory));
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore IDE0059 // Unnecessary assignment of a value

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
            var memoryPlugin = _kernel.ImportPluginFromObject(new TextMemoryPlugin(_semanticTextMemory));
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

           // var data = await _kernel.InvokeAsync(memoryPlugin["Recall"], argument);

            var chatFunction = _kernel.CreateFunctionFromPrompt(skPrompt, _executionSettings);

            //var textPromptResult = await _kernel.InvokeAsync(chatFunction);
           // _kernel.Plugins.Add(memoryPlugin);
           // var data = await memoryPlugin["Recall"].InvokeAsync(argument);

               var data = await chatFunction.InvokeAsync(_kernel, argument);

            return data.ToString();
        }
    }
}
