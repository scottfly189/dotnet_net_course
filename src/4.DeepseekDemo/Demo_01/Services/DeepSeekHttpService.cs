using Demo_01.Models;
using System.Reflection;

namespace Demo_01.Services;

public class DeepSeekHttpService
{
    private readonly HttpClient _httpClient;
    
    public DeepSeekHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        // 通过反射获取HttpMessageHandler
        var handlerField = typeof(HttpClient).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance);
        var handler = handlerField?.GetValue(_httpClient);
        
        Console.WriteLine($"DeepSeekHttpService instance: {this.GetHashCode()}");
        Console.WriteLine($"HttpClient instance: {_httpClient.GetHashCode()}");
        Console.WriteLine($"HttpMessageHandler instance: {handler?.GetHashCode()}");
        Console.WriteLine($"HttpMessageHandler type: {handler?.GetType().Name}");
        Console.WriteLine("---");
    }

    public async Task<string> GetDeepSeekResponse(DeepSeekRequest request)
    {
        // HttpClient已经在Program.cs中正确配置，无需重复设置
        // 这里应该发送实际的HTTP请求
        // var response = await _httpClient.PostAsJsonAsync("", request);
        // return await response.Content.ReadAsStringAsync();
        
        return await Task.FromResult("Hello");
    }

    public async Task<string> GetDeepSeekResponseStream(DeepSeekRequest request)
    {
        // HttpClient已经在Program.cs中正确配置，无需重复设置
        // 这里应该发送实际的HTTP请求
        // var response = await _httpClient.PostAsJsonAsync("", request);
        // return await response.Content.ReadAsStringAsync();
        
        return await Task.FromResult("Hello");
    }
}

