using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace SseDemo.Server.Controllers;


[ApiController]
[Route("/api/[controller]/[action]")]
public class SseService : ControllerBase
{
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
            }
        }catch(OperationCanceledException)
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


    [HttpGet("{token}")]
    public async Task TestServerStream(string token, long userId, CancellationToken cancellationToken)
    {
        CheckAuthentication(token);
        Response.ContentType = "text/event-stream";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");

        while (!cancellationToken.IsCancellationRequested)
        {
            // Simulate sending an event every second
            await Task.Delay(1000, cancellationToken);

            // Send a simple event
            await Response.WriteAsync($"data: Event at {DateTime.Now}\n\n", cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }
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