using GenerativeAIChatBot.Models.Entities;
using GenerativeAIChatBot.Persistence.Repositories;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace test_generative_ai.Managers
{
    public class ChatAIManager : IChatAIManager
    {
        private readonly IPromptManager _promptManager;
        private readonly IHistoryRepository _historyRepository;

        public ChatAIManager(IPromptManager promptManager, IHistoryRepository historyRepository)
        {
            _promptManager = promptManager;
            _historyRepository = historyRepository;
        }

        public async Task<string> GetAnswerAsync(string userInput, string email)
        {

            var history = _historyRepository.GetHistoriesTextByEmail(email) ?? "";

#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var arguments = new KernelArguments()
            {
                ["history"] = history,
                [TextMemoryPlugin.CollectionParam] = "brain-station-23",
                [TextMemoryPlugin.LimitParam] = "2",
                [TextMemoryPlugin.RelevanceParam] = "0.79",
            };
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

           // arguments["userInput"] = userInput;

            arguments["input"] = userInput;

            var answer = await _promptManager.ChatResultFunctionAsync(arguments);

            var text = $"\nUser: {userInput}\nMelody: {answer}\n";

            var createHistory = new History() ;

            createHistory.Email = email;
            createHistory.Text = text;

            _historyRepository.CreateHistory(createHistory);

            return answer;
        }
    }
}
