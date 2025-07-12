
using System.Collections.Concurrent;
using System.Threading.Channels;

public class SseChannelService
{
    /// <summary>
    /// 创建一个多线程安全的字典来存储用户的Channels
    /// 键为用户ID
    /// </summary>
    private readonly ConcurrentDictionary<long, Channel<string>> _userChanners = new();

    /// <summary>
    /// 注册用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ChannelReader<string> Register(long userId)
    {
        if (_userChanners.TryGetValue(userId, out var oldChannel))
        {
            // 如果用户已经有了Channel，直接返回它的Reader
            return oldChannel.Reader;
        }
        var channel = Channel.CreateBounded<string>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        });
        _userChanners[userId] = channel;
        return channel.Reader;
    }
    /// <summary>
    /// 反注册用户
    /// 结束Channel的读端
    /// </summary>
    /// <param name="userId"></param>
    public void Unregister(long userId)
    {
        if (_userChanners.TryRemove(userId, out var channel))
        {
            channel.Writer.TryComplete();  //结束读端
        }
    }

    public async Task SendMessageAsync(long userId, string message, CancellationToken cancellationToken)
    {
        // 获取用户的Channel    
        if (_userChanners.TryGetValue(userId, out var channel))
        {
            // 向Channel写入消息
            await channel.Writer.WriteAsync(message,cancellationToken);
        }
    }
}
