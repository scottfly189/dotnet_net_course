using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/[controller]/[action]")]
public class SseServiceController : ControllerBase
{
    private readonly SseChannelService _sseChannelManager;

    public SseServiceController(SseChannelService sseChannelManager)
    {
        _sseChannelManager = sseChannelManager;
    }


    /// <summary>
    /// 前端测试SSE
    /// </summary>
    /// <param name="token"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{token}")]
    public async Task TestClientStream(
    [FromRoute(Name = "token")] string token,
    [FromQuery(Name = "userId")] long userId,
    CancellationToken cancellationToken)
    {
        Response.ContentType = "text/event-stream";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");
        try
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                // Simulate sending an event every second
                await Task.Delay(1000, cancellationToken);

                // Send a simple event
                await Response.WriteAsync($"data: send data from sse server\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);

                await Task.Delay(500, cancellationToken);
                await Response.WriteAsync($"event:heatbeat\n", cancellationToken);
                await Response.WriteAsync($"data: \n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation gracefully
            Response.StatusCode = 499; // Client Closed Request
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Response.StatusCode = 500; // Internal Server Error
            await Response.WriteAsync($"data: Error occurred: {ex.Message}\n\n", cancellationToken);
        }
    }

    /// <summary>
    /// 演示后端
    /// </summary>
    /// <param name="token"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{token}")]
    public async Task TestServerStream(string token, long userId, CancellationToken cancellationToken)
    {
        CheckAuthentication(token);  //JWT验证或其他认证方式
        //SSE要求返回的Content-Type为text/event-stream
        Response.Headers.Append("Content-Type", "text/event-stream");  //重要
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");
        Response.Headers.Append("X-Accel-Buffering", "no"); // Nginx重要

        var channelReader = _sseChannelManager.Register(userId);
        try
        {
            //1.心跳
            _ = Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Response.WriteAsync($"event:heatbeat\n");
                    await Response.WriteAsync($"data: \n\n", cancellationToken);
                    await Response.Body.FlushAsync(cancellationToken);
                    await Task.Delay(5000, cancellationToken);
                }
            });
            await foreach (var message in channelReader.ReadAllAsync(cancellationToken))
            {
                //如果浏览器刷新或者关闭了连接，弹出异常
                cancellationToken.ThrowIfCancellationRequested();
                //2.发送自定义消息
                await Response.WriteAsync($"event:chat\n", cancellationToken);
                await Response.WriteAsync($"data: {message}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation gracefully
            Response.StatusCode = 499; // Client Closed Request
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Response.StatusCode = 500; // Internal Server Error
            await Response.WriteAsync($"data: Error occurred: {ex.Message}\n\n", cancellationToken);
        }

    }

    /// <summary>
    /// 测试后端发送消息
    /// 通过SseChannelManager向指定用户发送消息
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    public async void TestServerSend(long userId, string message, CancellationToken cancellationToken)
    {
        //向指定用户发送消息
        await _sseChannelManager.SendMessageAsync(userId, message, cancellationToken);
    }

    private void CheckAuthentication(string token)
    {
        if (token == "111222")
        {
            return;
        }
        throw new UnauthorizedAccessException("Invalid token");
    }
}