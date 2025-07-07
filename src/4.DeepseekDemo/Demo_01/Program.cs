using Demo_01.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// 正确配置HttpClient
builder.Services.AddHttpClient<DeepSeekHttpService>(client =>
{
    client.BaseAddress = new Uri("https://api.deepseek.com/chat/completions");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer sk-1234567890");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

