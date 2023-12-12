using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;

namespace test_generative_ai.Managers
{
    [Serializable]
    public class OpenAIPromptManager: IPromptManager
    {
        private readonly Kernel _kernel;
        private readonly OpenAIPromptExecutionSettings _executionSettings;

        public OpenAIPromptManager(Kernel kernel, OpenAIPromptExecutionSettings executionSettings)
        {
            _kernel = kernel;
            _executionSettings = executionSettings;
        }

        public async Task<string> ChatResultFunctionAsync(KernelArguments argument)
        {
            const string skPrompt = @"
            ChatBot can have a conversation with you about any topic.
            It can give explicit instructions or say 'I don't know' if it does not have an answer.

            {{$history}}
            User: {{$userInput}}
            ChatBot:";

            var chatFunction = _kernel.CreateFunctionFromPrompt(skPrompt, _executionSettings);
            var answer = await chatFunction.InvokeAsync(_kernel, argument);
            return answer.ToString();
        }
    }
}
