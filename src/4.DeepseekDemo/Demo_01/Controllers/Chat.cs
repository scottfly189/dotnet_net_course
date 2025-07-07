using Demo_01.Models;
using Demo_01.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo_01.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly ILogger<ChatController> _logger;
    private readonly DeepSeekHttpService _deepSeekHttpService;
    
    public ChatController(ILogger<ChatController> logger, DeepSeekHttpService deepSeekHttpService)
    {
        _logger = logger;
        _deepSeekHttpService = deepSeekHttpService;
    }

    /// <summary>
    /// 非流式聊天
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> Chat([FromBody] DeepSeekRequest request)
    {
        return await _deepSeekHttpService.GetDeepSeekResponse(request);
    }

    /// <summary>
    /// 流式聊天
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("stream")]
    public async Task<string> Stream([FromBody] DeepSeekRequest request)
    {
        return await _deepSeekHttpService.GetDeepSeekResponseStream(request);
    }
}

