namespace test_generative_ai.Managers
{
    public interface IChatAIManager
    {
        Task<string> GetAnswerAsync(string userInput, string email);
    }
}
