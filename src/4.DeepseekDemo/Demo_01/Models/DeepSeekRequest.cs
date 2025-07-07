using System.Text.Json.Serialization;

namespace Demo_01.Models
{
    /// <summary>
    /// DeepSeek API 请求消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 消息角色：system, user, assistant
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }

    /// <summary>
    /// DeepSeek API 请求DTO
    /// </summary>
    public class DeepSeekRequest
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = "deepseek-chat";

        /// <summary>
        /// 消息列表
        /// </summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// 是否流式响应
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; } = false;
    }
}
