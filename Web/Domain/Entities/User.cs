namespace Web.Domain.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string? Fullname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Phone { get; set; }

    public List<UserBook> Books { get; set; } = [];
}