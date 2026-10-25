using Microsoft.AspNetCore.Mvc;
using Web.Application;
using Web.Domain;
using Web.Domain.Entities;

namespace Web.Controllers;

[Route("/auth"), ApiController]
public sealed class AuthController(AppDbContext context) : ControllerBase
{
    [Route("login"), HttpPost]
    public IActionResult Login(LoginDto dto)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return NotFound("Belə bir istifadəçi maili ya da şifrəsi təpənqə dəysin");

        return Ok();
    }

    [Route("register"), HttpPost]
    public IActionResult Register(RegisterDto dto)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (user is not null) 
            return NotFound("Belə bir istifadəçi maili artıq var");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, BCrypt.Net.BCrypt.GenerateSalt());

        var newUser = new User {
            Email = dto.Email,
            Password = hashedPassword,
        };

        try
        {
            context.Users.Add(newUser);
            context.SaveChanges();

            return Created();
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}