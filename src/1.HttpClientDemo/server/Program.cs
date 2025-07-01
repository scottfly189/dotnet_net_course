using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "a test webapi demo",
        Version = "v1",
        Description = "a test webapi demo"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "a test webapi demo");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

#region Map方式接收请求

// //1. query
// app.MapGet("/user", (long id,string name) =>
// {
//     var userList = UserDemo.UserList();
//     var user = userList.FirstOrDefault(u => u.Id == id && u.Name == name);
//     return JsonSerializer.Serialize(user ?? throw new ArgumentException($"User with id {id} not found."));
// });
// //2. parameter
// app.MapGet("/user/{id}", (long id) =>
// {
//     var userList = UserDemo.UserList();
//     var user = userList.FirstOrDefault(u => u.Id == id);
//     return JsonSerializer.Serialize(user ?? throw new ArgumentException($"User with id {id} not found."));
// });
// //3. post 增加
// app.MapPost("/user", (UserDemo user) =>
// {
//     var userList = UserDemo.UserList();
//     if (userList.Any(u => u.Id == user.Id))
//     {
//         throw new ArgumentException($"User with id {user.Id} already exists.");
//     }
//     userList.Add(user);
//     return HttpStatusCode.Created;
// });
// //4. put 全量更新
// app.MapPut("/user/{id}", (long id, UserDemo user) =>
// {
//     var userList = UserDemo.UserList();
//     var existingUser = userList.FirstOrDefault(u => u.Id == id);
//     if (existingUser == null)
//     {
//         throw new ArgumentException($"User with id {id} not found.");
//     }
//     existingUser.Name = user.Name;
//     existingUser.Email = user.Email;
//     return HttpStatusCode.OK;
// });
// //5. patch 部分更新
// app.MapPatch("/user/{id}", (long id, UserDemo user) =>
// {
//     var userList = UserDemo.UserList();
//     var existingUser = userList.FirstOrDefault(u => u.Id == id);
//     if (existingUser == null)
//     {
//         throw new ArgumentException($"User with id {id} not found.");
//     }
//     if (!string.IsNullOrEmpty(user.Name))
//     {
//         existingUser.Name = user.Name;
//     }
//     if (!string.IsNullOrEmpty(user.Email))
//     {
//         existingUser.Email = user.Email;
//     }
//     return HttpStatusCode.OK;
// });
// //6.delete
// app.MapDelete("/user/{id}", (long id) =>
// {
//     var userList = UserDemo.UserList();
//     var user = userList.FirstOrDefault(u => u.Id == id);
//     if (user == null)
//     {
//         throw new ArgumentException($"User with id {id} not found.");
//     }
//     userList.Remove(user);
//     return HttpStatusCode.NoContent;
// });
#endregion


app.Run();
