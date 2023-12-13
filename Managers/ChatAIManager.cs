using GenerativeAIChatBot.Models.Entities;
using GenerativeAIChatBot.Persistence.Repositories;
using Microsoft.SemanticKernel;

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

            var arguments = new KernelArguments()
            {
                ["history"] = history
            };

            arguments["userInput"] = userInput;

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
