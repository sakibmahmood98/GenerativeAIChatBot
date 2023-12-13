using GenerativeAIChatBot.Models.Entities;
using GenerativeAIChatBot.Persistence.Contexts;

namespace GenerativeAIChatBot.Persistence.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly StoreContext _storeContext;

        public HistoryRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public History CreateHistory(History history)
        {
            history.CreatedAt = DateTime.UtcNow;

            _storeContext.History.Add(history);
            _storeContext.SaveChanges();

            return history;
        }

        public List<History> GetHistoriesByEmail(string email)
        {
            List<History> histories = _storeContext.History.Where(h=>h.Email == email).ToList();

            if (histories != null)
            {
                return histories;
            }

            throw new Exception();
        }

        public string GetHistoriesTextByEmail(string email)
        {
            List<History> histories = _storeContext.History.Where(h => h.Email == email).ToList();

            string concatenatedText = string.Join(" ", histories.Select(h => h.Text));

            if (histories != null)
            {
                return concatenatedText;
            }

            throw new Exception();
        }
    }
}
