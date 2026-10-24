using Microsoft.AspNetCore.Mvc;
using Web.Domain;
using Web.Domain.Entities;

namespace Web.Controllers;

[Route("/books")]
[ApiController]
public sealed class BookController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public List<Book> GetBooks()
    {
        var books = context.Books.ToList();
        return books;
    }

    [HttpPost]
    public Book AddBook([FromBody] Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
        return book;
    }
}