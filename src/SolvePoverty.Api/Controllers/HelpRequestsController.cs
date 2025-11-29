using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolvePoverty.Infrastructure.Data;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelpRequestsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HelpRequestsController> _logger;

    public HelpRequestsController(ApplicationDbContext context, ILogger<HelpRequestsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/helprequests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetHelpRequests(
        [FromQuery] RequestType? requestType = null,
        [FromQuery] RequestStatus? status = null,
        [FromQuery] bool? isUrgent = null)
    {
        try
        {
            var query = _context.HelpRequests
                .Include(hr => hr.User)
                .Where(hr => !hr.IsDeleted);

            if (requestType.HasValue)
                query = query.Where(hr => hr.RequestType == requestType.Value);

            if (status.HasValue)
                query = query.Where(hr => hr.Status == status.Value);

            if (isUrgent.HasValue)
                query = query.Where(hr => hr.IsUrgent == isUrgent.Value);

            var requests = await query
                .OrderByDescending(hr => hr.IsUrgent)
                .ThenByDescending(hr => hr.CreatedAt)
                .Select(hr => new
                {
                    hr.Id,
                    hr.Title,
                    hr.Description,
                    hr.RequestType,
                    hr.IsUrgent,
                    hr.Status,
                    hr.CreatedAt,
                    User = new
                    {
                        hr.User.Id,
                        hr.User.FirstName,
                        hr.User.LastName,
                        hr.User.City,
                        hr.User.State
                    }
                })
                .ToListAsync();

            return Ok(requests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving help requests");
            return StatusCode(500, "An error occurred while retrieving help requests");
        }
    }

    // GET: api/helprequests/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetHelpRequest(int id)
    {
        try
        {
            var request = await _context.HelpRequests
                .Include(hr => hr.User)
                .Include(hr => hr.FulfilledByUser)
                .Where(hr => hr.Id == id && !hr.IsDeleted)
                .Select(hr => new
                {
                    hr.Id,
                    hr.Title,
                    hr.Description,
                    hr.RequestType,
                    hr.IsUrgent,
                    hr.Status,
                    hr.CreatedAt,
                    hr.FulfilledDate,
                    User = new
                    {
                        hr.User.Id,
                        hr.User.FirstName,
                        hr.User.LastName,
                        hr.User.Email,
                        hr.User.PhoneNumber,
                        hr.User.City,
                        hr.User.State
                    },
                    FulfilledBy = hr.FulfilledByUser != null ? new
                    {
                        hr.FulfilledByUser.Id,
                        hr.FulfilledByUser.FirstName,
                        hr.FulfilledByUser.LastName
                    } : null
                })
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return NotFound($"Help request with ID {id} not found");
            }

            return Ok(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving help request {RequestId}", id);
            return StatusCode(500, "An error occurred while retrieving the help request");
        }
    }

    // POST: api/helprequests
    [HttpPost]
    public async Task<ActionResult<HelpRequest>> CreateHelpRequest(CreateHelpRequestDto dto)
    {
        try
        {
            // Verify user exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest("User not found");
            }

            var helpRequest = new HelpRequest
            {
                UserId = dto.UserId,
                RequestType = dto.RequestType,
                Title = dto.Title,
                Description = dto.Description,
                IsUrgent = dto.IsUrgent,
                Status = (RequestStatus)0, // First enum value
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.HelpRequests.Add(helpRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHelpRequest), new { id = helpRequest.Id }, helpRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating help request");
            return StatusCode(500, "An error occurred while creating the help request");
        }
    }

    // PUT: api/helprequests/5/fulfill
    [HttpPut("{id}/fulfill")]
    public async Task<IActionResult> FulfillHelpRequest(int id, int fulfilledByUserId)
    {
        try
        {
            var request = await _context.HelpRequests
                .FirstOrDefaultAsync(hr => hr.Id == id && !hr.IsDeleted);

            if (request == null)
            {
                return NotFound($"Help request with ID {id} not found");
            }

            if (request.Status != (RequestStatus)0)
            {
                return BadRequest("This request has already been processed");
            }

            var userExists = await _context.Users.AnyAsync(u => u.Id == fulfilledByUserId && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest("Fulfilling user not found");
            }

            request.Status = (RequestStatus)1;
            request.FulfilledByUserId = fulfilledByUserId;
            request.FulfilledDate = DateTime.UtcNow;
            request.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fulfilling help request {RequestId}", id);
            return StatusCode(500, "An error occurred while fulfilling the help request");
        }
    }

    // GET: api/helprequests/urgent
    [HttpGet("urgent")]
    public async Task<ActionResult<IEnumerable<object>>> GetUrgentRequests()
    {
        try
        {
            var urgentRequests = await _context.HelpRequests
                .Include(hr => hr.User)
                .Where(hr => hr.IsUrgent && hr.Status == (RequestStatus)0 && !hr.IsDeleted)
                .OrderByDescending(hr => hr.CreatedAt)
                .Select(hr => new
                {
                    hr.Id,
                    hr.Title,
                    hr.Description,
                    hr.RequestType,
                    hr.CreatedAt,
                    User = new
                    {
                        hr.User.FirstName,
                        hr.User.LastName,
                        hr.User.City,
                        hr.User.State
                    }
                })
                .Take(20)
                .ToListAsync();

            return Ok(urgentRequests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving urgent help requests");
            return StatusCode(500, "An error occurred while retrieving urgent requests");
        }
    }

    // GET: api/helprequests/user/5
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetUserHelpRequests(int userId)
    {
        try
        {
            var requests = await _context.HelpRequests
                .Where(hr => hr.UserId == userId && !hr.IsDeleted)
                .OrderByDescending(hr => hr.CreatedAt)
                .Select(hr => new
                {
                    hr.Id,
                    hr.Title,
                    hr.Description,
                    hr.RequestType,
                    hr.IsUrgent,
                    hr.Status,
                    hr.CreatedAt,
                    hr.FulfilledDate
                })
                .ToListAsync();

            return Ok(requests);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving help requests for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving user help requests");
        }
    }
}

// DTOs
public record CreateHelpRequestDto(
    int UserId,
    RequestType RequestType,
    string Title,
    string Description,
    bool IsUrgent
);