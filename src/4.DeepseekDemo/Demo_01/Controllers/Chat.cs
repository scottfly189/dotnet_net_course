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
    [HttpGet("v1/normalchat")]
    public async Task<string> Chat(string message)
    {
        var request = new DeepSeekRequest();
        request.Model = "deepseek-chat";
        request.Stream = false;
        request.Messages.Add(new Message { Role = "system", Content = "你是一个AI助手，请回答用户的问题。" });
        request.Messages.Add(new Message { Role = "user", Content = message });
        return await _deepSeekHttpService.GetDeepSeekResponse(request);
    }

    /// <summary>
    /// 流式聊天
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("v1/streamchat")]
    public async Task<string> Stream(string message)
    {
        var request = new DeepSeekRequest();
        request.Model = "deepseek-chat";
        request.Stream = true;
        request.Messages.Add(new Message { Role = "system", Content = "你是一个AI助手，请回答用户的问题。" });
        request.Messages.Add(new Message { Role = "user", Content = message });

        return await _deepSeekHttpService.GetDeepSeekResponseStream(request);
    }
}

