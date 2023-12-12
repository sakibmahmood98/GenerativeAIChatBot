using Microsoft.SemanticKernel;

namespace test_generative_ai.Managers
{
    public class ChatAIManager : IChatAIManager
    {
        private readonly IPromptManager _promptManager;

        public ChatAIManager(IPromptManager promptManager)
        {
            _promptManager = promptManager;
        }

        public async Task<string> GetAnswerAsync(string userInput, string email)
        {
            //retrieve history by email address.

            //table design : email , text

            //history = getByEmail(email)

            var history = "";
            var arguments = new KernelArguments()
            {
                ["history"] = history
            };

            arguments["userInput"] = userInput;

            var answer = await _promptManager.ChatResultFunctionAsync(arguments);

            history = $"\nUser: {userInput}\nMelody: {answer}\n";

            //save history in db with email address and answer

            return answer;
        }
    }
}
