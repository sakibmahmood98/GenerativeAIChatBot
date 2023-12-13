using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel;
using test_generative_ai.Managers;
using GenerativeAIChatBot.Persistence.Contexts;
using GenerativeAIChatBot.Persistence.Repositories;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Connectors.Memory.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Plugins.Memory;
using Microsoft.SemanticKernel.Memory;

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
#pragma warning disable SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                kernelBuilder.AddOpenAIChatCompletion(model, apiKey, orgId)
                            .AddOpenAITextEmbeddingGeneration(model, apiKey, orgId);
#pragma warning restore SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                return kernelBuilder.Build();
            });


#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
/*            services.AddSingleton<SemanticTextMemory>(provider =>
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                .Build();

                var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");


#pragma warning disable SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                IMemoryStore store = SqliteMemoryStore.ConnectAsync("").GetAwaiter().GetResult();
#pragma warning restore SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

                var model = "text-embedding-ada-002";
                var apiKey = "";

#pragma warning disable SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                var embeddingGenerator = new OpenAITextEmbeddingGeneration(model, apiKey);
#pragma warning restore SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

                SemanticTextMemory textMemory = new(store, embeddingGenerator);

                return textMemory;
            });*/
#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.



#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            services.AddSingleton<ISemanticTextMemory>(provider =>
            {
#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                var model = "text-embedding-ada-002";
                var apiKey = "";
                var memoryBuilder = new MemoryBuilder();

#pragma warning disable SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                memoryBuilder.WithOpenAITextEmbeddingGeneration("text-embedding-ada-002", apiKey);
#pragma warning restore SKEXP0011 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning disable SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                IMemoryStore store = SqliteMemoryStore.ConnectAsync("").GetAwaiter().GetResult();
                memoryBuilder.WithMemoryStore(store);
#pragma warning restore SKEXP0028 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

                var memory = memoryBuilder.Build();

                return memory;

#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
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
