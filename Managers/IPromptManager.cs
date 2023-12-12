using Microsoft.SemanticKernel;

namespace test_generative_ai.Managers
{
    public interface IPromptManager
    {
        Task<string> ChatResultFunctionAsync(KernelArguments prompt);
    }
}
