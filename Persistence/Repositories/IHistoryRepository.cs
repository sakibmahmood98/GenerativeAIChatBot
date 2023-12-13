using GenerativeAIChatBot.Models.Entities;

namespace GenerativeAIChatBot.Persistence.Repositories
{
    public interface IHistoryRepository
    {
        History CreateHistory(History history);
        List<History> GetHistoriesByEmail(string email);
        string GetHistoriesTextByEmail(string email);
    }
}
