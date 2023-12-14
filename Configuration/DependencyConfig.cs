#pragma warning disable SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using test_generative_ai.Managers;
using GenerativeAIChatBot.Persistence.Contexts;
using GenerativeAIChatBot.Persistence.Repositories;
using Microsoft.SemanticKernel.Connectors.Memory.Sqlite;
using Microsoft.SemanticKernel.Plugins.Memory;
using Microsoft.SemanticKernel.Memory;
using GenerativeAIChatBot.Persistence.MemoryPluginRegister;

namespace test_generative_ai.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {

            services.AddDbContext<StoreContext>();

            services.AddTransient<Kernel>(provider =>
            {
                var kernelBuilder = new KernelBuilder();
                var model = "gpt-3.5-turbo";
                var apiKey = "";
                var orgId = "";
                kernelBuilder.AddOpenAIChatCompletion(model, apiKey, orgId)
                            .AddOpenAITextEmbeddingGeneration(model, apiKey, orgId);
                return kernelBuilder.Build();
            });

            services.AddTransient<ISemanticTextMemory>(provider =>
            {
                var model = "text-embedding-ada-002";
                var apiKey = "";
                var memoryBuilder = new MemoryBuilder();

                memoryBuilder.WithOpenAITextEmbeddingGeneration("text-embedding-ada-002", apiKey);

                IMemoryStore store = SqliteMemoryStore.ConnectAsync("").GetAwaiter().GetResult();
                memoryBuilder.WithMemoryStore(store);

                var memory = memoryBuilder.Build();

                return memory;

            });

            services.AddSingleton(new OpenAIPromptExecutionSettings
            {
                MaxTokens = 2000,
                Temperature = 0.7,
                TopP = 0.5
            });

            services.AddScoped<IHistoryRepository, HistoryRepository>();

            services.AddTransient<IMemoryPlugin, MemoryPlugin>();
            services.AddTransient<IChatAIManager, ChatAIManager>();

            return services;
        }
    }
}


#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning restore SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.