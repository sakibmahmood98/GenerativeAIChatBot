using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using test_generative_ai.Managers;

namespace test_generative_ai.Configuration
{
    public static class DependencyConfig
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<Kernel>(provider =>
            {
                var kernelBuilder = new KernelBuilder();
                var model = "gpt-3.5-turbo";
                var apiKey = "";
                var orgId = "";
                kernelBuilder.AddOpenAIChatCompletion(model, apiKey, orgId);
                return kernelBuilder.Build();
            });

            services.AddSingleton(new OpenAIPromptExecutionSettings
            {
                MaxTokens = 2000,
                Temperature = 0.7,
                TopP = 0.5
            });

            services.AddTransient<IPromptManager, OpenAIPromptManager>();
            services.AddTransient<IChatAIManager, ChatAIManager>();
        }
    }
}
