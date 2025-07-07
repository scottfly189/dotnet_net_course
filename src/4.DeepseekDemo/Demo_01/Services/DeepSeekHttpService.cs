using Demo_01.Models;
using System.Reflection;
using System.Net.Http.Json;
using System.Text.Json;

namespace Demo_01.Services;

public class DeepSeekHttpService
{
    private readonly HttpClient _httpClient;
    
    public DeepSeekHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }

    public async Task<string> GetDeepSeekResponse(DeepSeekRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync<DeepSeekRequest>("/chat/completions", request);
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<string> GetDeepSeekResponseStream(DeepSeekRequest request)
    {
        var result = "";
        var response = await _httpClient.PostAsJsonAsync<DeepSeekRequest>("/chat/completions", request);
        var stream = await response.Content.ReadAsStreamAsync();
        using (var reader = new StreamReader(stream))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();    
                Console.WriteLine("接收到数据：" + line);
                result += line;
            }
        }
        return result;
    }
}

