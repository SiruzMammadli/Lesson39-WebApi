namespace Web.Domain.Entities;

public sealed class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public List<UserBook> Users { get; set; } = [];
}