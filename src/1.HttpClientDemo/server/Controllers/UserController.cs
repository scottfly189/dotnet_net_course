using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUser(long id,string name)
    {
        var userList = UserDemo.UserList();
        var user = userList.FirstOrDefault(u => u.Id == id && u.Name == name);
        return user != null ? Ok(JsonSerializer.Serialize(user)) : NotFound($"User with id {id} not found.");
    }

    [HttpGet]
    public IActionResult GetUserById(long id)
    {
        var userList = UserDemo.UserList();
        var user = userList.FirstOrDefault(u => u.Id == id);
        return user != null ? Ok(JsonSerializer.Serialize(user)) : NotFound($"User with id {id} not found.");
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDemo user)
    {
        var userList = UserDemo.UserList();
        if (userList.Any(u => u.Id == user.Id))
        {
            return BadRequest($"User with id {user.Id} already exists.");
        }
        userList.Add(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, JsonSerializer.Serialize(user));
    }

    [HttpPut]
    public IActionResult UpdateUser(long id, [FromBody] UserDemo user)
    {
        var userList = UserDemo.UserList();
        var existingUser = userList.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound($"User with id {id} not found.");
        }
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        return Ok();
    }

    // Add other methods as needed...
}