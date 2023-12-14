#pragma warning disable SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace GenerativeAIChatBot.Persistence.MemoryPluginRegister
{
    public class MemoryPlugin: IMemoryPlugin
    {
        private readonly Kernel _kernel;
        private readonly ISemanticTextMemory _semanticTextMemory;
        public MemoryPlugin(Kernel kernel, ISemanticTextMemory semanticTextMemory) {
            _kernel = kernel;
            _semanticTextMemory = semanticTextMemory;
        }

        public void RegisterMemoryPlugin()
        {
            _kernel.ImportPluginFromObject(new TextMemoryPlugin(_semanticTextMemory));
        }
    }
}

#pragma warning restore SKEXP0003 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0052 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
