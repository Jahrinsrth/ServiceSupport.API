using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatQueueService _chatQueueService; 
 
        public ChatController(IChatQueueService chatQueueService)
        {
            _chatQueueService = chatQueueService;
        }

        [HttpPost("create")]
        public IActionResult CreateChatSession(ChatSessionDto chatSessionDto)
        {
            var result = _chatQueueService.CreateChat(chatSessionDto);
            return Ok(result);
        }

        [HttpGet("GetAllSession")]
        public IActionResult GetAllActiveSession()
        {
            var result = _chatQueueService.GetAll();
            return Ok(result);
        }
    }
}
