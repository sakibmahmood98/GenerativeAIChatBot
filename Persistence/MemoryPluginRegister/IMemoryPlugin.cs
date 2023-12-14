using Microsoft.SemanticKernel;

namespace GenerativeAIChatBot.Persistence.MemoryPluginRegister
{
    public interface IMemoryPlugin
    {
        IKernelPlugin GetTextMemoryPlugin();
    }
}
