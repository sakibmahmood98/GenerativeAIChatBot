using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using test_generative_ai.Managers;
using GenerativeAIChatBot.Persistence.Contexts;
using GenerativeAIChatBot.Persistence.Repositories;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Connectors.Memory.Sqlite;
using Microsoft.Extensions.Configuration;

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
                kernelBuilder.AddOpenAIChatCompletion(model, apiKey, orgId)
                            .AddOpenAITextEmbeddingGeneration(model, apiKey, orgId);

                return kernelBuilder.Build();
            });


#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            services.AddSingleton<SemanticTextMemory>(provider =>
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                .Build();

                var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");


#pragma warning disable SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                IMemoryStore store = SqliteMemoryStore.ConnectAsync("C:\\BS23\\Personal\\generative-ai-chatbot\\Database\\generativeai.db").GetAwaiter().GetResult();
#pragma warning restore SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

                var model = "text-embedding-ada-002";
                var apiKey = "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";

                var embeddingGenerator = new OpenAITextEmbeddingGeneration(model, apiKey);

                SemanticTextMemory textMemory = new(store, embeddingGenerator);

                return textMemory;
            });
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.


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
