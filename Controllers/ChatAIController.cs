using Microsoft.AspNetCore.Mvc;
using test_generative_ai.Managers;

namespace test_generative_ai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatAIController: Controller
    {
        private readonly IChatAIManager _aichatServices;

        public ChatAIController(IChatAIManager aichatServices)
        {
            _aichatServices = aichatServices;
        }

        [HttpPost]
        public async Task<IActionResult> GetAnswer(string question, string email)
        {
            var result = await _aichatServices.GetAnswerAsync(question, email);
            return Ok(result);
        }
    }
}
