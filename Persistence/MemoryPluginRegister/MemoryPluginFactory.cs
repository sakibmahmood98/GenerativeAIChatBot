#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel;

namespace GenerativeAIChatBot.Persistence.MemoryPluginRegister
{
    public class MemoryPluginFactory
    {
        public static IMemoryPlugin CreateMemoryPlugin(IServiceProvider serviceProvider)
        {
            var kernel = serviceProvider.GetRequiredService<Kernel>();
            var semanticTextMemory = serviceProvider.GetRequiredService<ISemanticTextMemory>();

            var memoryPlugin = new MemoryPlugin(kernel, semanticTextMemory);
            memoryPlugin.RegisterMemoryPlugin();

            return memoryPlugin;
        }
    }
}

#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.