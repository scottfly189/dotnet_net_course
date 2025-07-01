public class UserDemo(long id, string name, string email)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;

    private static List<UserDemo>? _userList = null;

    public static List<UserDemo> UserList()
    {
        _userList = _userList ?? new List<UserDemo>()
        {
            new UserDemo(1, "Alice", "Alice@gmail.com"),
            new UserDemo(2, "Bob","Bob@gmail.com"),
            new UserDemo(3, "Charlie", "Charlie@gmail.com"),
        };
        return _userList;
    }
}