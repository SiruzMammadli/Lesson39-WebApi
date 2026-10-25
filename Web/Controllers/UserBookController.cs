using Microsoft.AspNetCore.Mvc;
using Web.Application.DTOs.UserBooks;
using Web.Domain;
using Web.Domain.Entities;

namespace Web.Controllers;

[Route("/userbooks"), ApiController]
public class UserBookController(AppDbContext context) : ControllerBase
{
    [HttpPost]
    public IActionResult AddUserBook(AddUserBookDto dto)
    {
        var newUserBook = new UserBook
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
        };

        try
        {
            context.UserBooks.Add(newUserBook);
            context.SaveChanges();

            return Created();
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}