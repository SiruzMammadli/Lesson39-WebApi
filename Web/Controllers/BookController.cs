using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Application.DTOs.Books;
using Web.Domain;
using Web.Domain.Entities;

namespace Web.Controllers;

[Route("/books")] //  {{host}}/books?userId=1
[ApiController]
public sealed class BookController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks([FromQuery] string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            var books = context.Books.ToList();
            return Ok(books);
        }
        else
        {
            if (!int.TryParse(userId, out int parsedUserId))
                return BadRequest("Doğru user id göndərilməyib!");

            var booksByUser = context.Books
            .Include(b => b.Users.Where(ub => ub.UserId == parsedUserId))
            .Select(e => new GetBookByUser
            {
                Id = e.Id,
                Title = e.Title,
                Price = e.Price,
                Email = e.Users.FirstOrDefault(u => u.UserId == parsedUserId).User.Email
            })
            .ToList();

            return Ok(booksByUser);
        }
    }

    [HttpPost]
    public Book AddBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
        return book;
    }
}