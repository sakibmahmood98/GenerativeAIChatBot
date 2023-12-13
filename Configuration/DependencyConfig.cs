using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using test_generative_ai.Managers;
using GenerativeAIChatBot.Persistence.Contexts;
using GenerativeAIChatBot.Persistence.Repositories;

namespace test_generative_ai.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<StoreContext>();

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

            services.AddScoped<IHistoryRepository, HistoryRepository>();

            services.AddTransient<IPromptManager, OpenAIPromptManager>();
            services.AddTransient<IChatAIManager, ChatAIManager>();

            return services;
        }
    }
}
