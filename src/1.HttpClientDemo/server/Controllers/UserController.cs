using System.Text.Json;
using Microsoft.AspNetCore.Mvc;


[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUser(long id, string name)
    {
        var userList = UserDemo.UserList();
        var user = userList.FirstOrDefault(u => u.Id == id && u.Name == name);
        return user != null ? Ok(JsonSerializer.Serialize(user)) : NotFound($"User with id {id} not found.");
    }

    [HttpGet("{id}")]
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

    [HttpPut("{id}")]
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
    [HttpPatch("{id}")]
    public IActionResult PatchUser(long id, [FromBody] UserDemo user)
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

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        var userList = UserDemo.UserList();
        var user = userList.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound($"User with id {id} not found.");
        }
        userList.Remove(user);
        return NoContent();
    }

    [HttpHead]
    public IActionResult HeadUser()
    {
        return Ok();
    }

    [HttpOptions]
    public IActionResult OptionsUser()
    {
        Response.Headers.Append("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
        return Ok();
    }
    


    // Add other methods as needed...
}