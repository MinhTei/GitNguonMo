using System.Security.Cryptography;
using System.Text;
using GitNguonMo.Api.Models;
using GitNguonMo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitNguonMo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataStore _store;

    public AuthController(DataStore store)
    {
        _store = store;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegistration req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest("Email and password required");

        if (_store.GetUserByEmail(req.Email) != null)
            return Conflict("Email already registered");

        var user = new User
        {
            Email = req.Email,
            Name = req.Name ?? string.Empty,
            PasswordHash = Hash(req.Password)
        };

        _store.AddUser(user);
        return Ok(new { user.Id, user.Email, user.Name });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var user = _store.GetUserByEmail(req.Email);
        if (user == null) return Unauthorized();
        if (user.PasswordHash != Hash(req.Password)) return Unauthorized();

        // Simple demo token (not secure) — in production use JWT or proper auth
        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id + ":" + user.Email));
        return Ok(new { token, user = new { user.Id, user.Email, user.Name } });
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var b = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(b);
    }

    public record UserRegistration(string Email, string Password, string? Name);
    public record LoginRequest(string Email, string Password);
}
