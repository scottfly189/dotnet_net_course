using Demo_01.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// 正确配置HttpClient
builder.Services.AddHttpClient<DeepSeekHttpService>(client =>
{
    var apiKey = Environment.GetEnvironmentVariable("sk_dk_api");
    client.BaseAddress = new Uri("https://api.deepseek.com");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

