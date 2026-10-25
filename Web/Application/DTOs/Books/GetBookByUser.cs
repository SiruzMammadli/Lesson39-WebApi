namespace Web.Application.DTOs.Books;

public sealed class GetBookByUser
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Email { get; set; }
}