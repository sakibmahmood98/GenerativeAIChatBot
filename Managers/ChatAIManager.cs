#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

using GenerativeAIChatBot.Models.Entities;
using GenerativeAIChatBot.Persistence.MemoryPluginRegister;
using GenerativeAIChatBot.Persistence.Repositories;
using Humanizer;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace test_generative_ai.Managers
{
    public class ChatAIManager : IChatAIManager
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly Kernel _kernel;
        private readonly OpenAIPromptExecutionSettings _executionSettings;
        private readonly IMemoryPlugin _memoryPlugin;

        public ChatAIManager(IHistoryRepository historyRepository, Kernel kernel, OpenAIPromptExecutionSettings executionSettings, IMemoryPlugin memoryPlugin)
        {
            _historyRepository = historyRepository;
            _kernel = kernel;
            _executionSettings = executionSettings;
            _memoryPlugin = memoryPlugin;
        }

        public async Task<string> GetAnswerAsync(string userInput, string email)
        {
            var result = await SearchMemory("brain-station-23", userInput);

            const string skPrompt = @"
            You are an AI chat assistant that helps people solve problems based on relevant information.
            you can give explicit instructions or say 'I don't know' if it does not have an answer.

            Relevant Info:
            {{ $relevantInfo }}
            {{$history}}
            User: {{$input}}
            ChatBot:";

            var history = _historyRepository.GetHistoriesTextByEmail(email) ?? "";

            var arguments = new KernelArguments()
            {
                ["relevantInfo"] = result,
                ["history"] = history,
                [TextMemoryPlugin.CollectionParam] = "brain-station-23",
                [TextMemoryPlugin.LimitParam] = "2",
                [TextMemoryPlugin.RelevanceParam] = "0.79",
                ["input"] = userInput,
            };

            var chatFunction = _kernel.CreateFunctionFromPrompt(skPrompt, _executionSettings);
            var answer = await chatFunction.InvokeAsync(_kernel, arguments);

            var text = $"\nUser: {userInput}\nMelody: {answer}\n";

            var createHistory = new History() ;
            createHistory.Email = email;
            createHistory.Text = text;

            _historyRepository.CreateHistory(createHistory);

            return answer.ToString();
        }

        private async Task<string> SearchMemory(string MemoryCollectionName, string findMemory)
        {
            Console.WriteLine("Searching for memories relevant to:" + findMemory);

            var result = await _kernel.InvokeAsync(_kernel.Plugins["TextMemoryPlugin"]["Recall"], new("Relevant info for user prompt:" + findMemory)
            {
                [TextMemoryPlugin.CollectionParam] = MemoryCollectionName,
                [TextMemoryPlugin.LimitParam] = "2",
                [TextMemoryPlugin.RelevanceParam] = "0.75",
            });
            string data = result.GetValue<string>();

            if (string.IsNullOrEmpty(data))
            {
                throw new NoMatchFoundException("No relevant memory found");
            }
/*            int start = data.IndexOf(":") + 1;
            int end = data.IndexOf("\\r\\n", start);
            string caseStudy = data.Substring(start, end - start);*/

            Console.WriteLine($"Relevant memory found = Case Study:{data}");

            return data;
        }
    }
}


#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.