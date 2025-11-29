using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolvePoverty.Infrastructure.Data;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var users = await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users");
            return StatusCode(500, "An error occurred while retrieving users");
        }
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user {UserId}", id);
            return StatusCode(500, "An error occurred while retrieving the user");
        }
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
    {
        try
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email && !u.IsDeleted))
            {
                return BadRequest("A user with this email already exists");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = HashPassword(userDto.Password), // You'll need to implement proper password hashing
                UserType = userDto.UserType,
                Address = userDto.Address,
                City = userDto.City,
                State = userDto.State,
                ZipCode = userDto.ZipCode,
                CreatedAt = DateTime.UtcNow,
                IsVerified = false,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, "An error occurred while creating the user");
        }
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            // Update properties
            user.FirstName = userDto.FirstName ?? user.FirstName;
            user.LastName = userDto.LastName ?? user.LastName;
            user.PhoneNumber = userDto.PhoneNumber ?? user.PhoneNumber;
            user.Address = userDto.Address ?? user.Address;
            user.City = userDto.City ?? user.City;
            user.State = userDto.State ?? user.State;
            user.ZipCode = userDto.ZipCode ?? user.ZipCode;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", id);
            return StatusCode(500, "An error occurred while updating the user");
        }
    }

    // DELETE: api/users/5 (soft delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            return StatusCode(500, "An error occurred while deleting the user");
        }
    }

    // Helper method - implement proper password hashing with BCrypt or Identity
    private string HashPassword(string password)
    {
        // TODO: Implement proper password hashing (use BCrypt.Net-Next or ASP.NET Core Identity)
        return password; // TEMPORARY - DO NOT USE IN PRODUCTION
    }
}

// DTOs (Data Transfer Objects)
public record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    UserType UserType,
    string? Address,
    string? City,
    string? State,
    string? ZipCode
);

public record UpdateUserDto(
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Address,
    string? City,
    string? State,
    string? ZipCode
);