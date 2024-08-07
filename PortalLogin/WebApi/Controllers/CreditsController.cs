using Application.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin" )]
public class CreditsController : ControllerBase
{
    private readonly ICreditRepository _creditRepository;

    public CreditsController(ICreditRepository creditRepository)
    {
        _creditRepository = creditRepository;
    }
    
    [HttpGet("get/{userId}")]
    public async Task<ActionResult> GetUserCredits(Guid userId)
    {
        var result = await _creditRepository.GetCreditAsync(userId);
        if (result.UserId == null)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
    
    [HttpPut("increase/{userId}")]
    public async Task<ActionResult> IncreaseCredits(Guid userId, decimal amount)
    {
        if (amount <= 0)
        {
            return BadRequest("Insira um valor maior que 0!");
        }
        var result = await _creditRepository.IncreaseCreditAsync(userId, amount);
        return Ok(result);
    }

    [HttpPut("decrease/{userId}")]
    public async Task<ActionResult> DecreaseCredits(Guid userId, decimal amount)
    {
        if (amount <= 0)
        {
            return BadRequest("Insira um valor maior que 0!");
        }
        var result = await _creditRepository.DecreaseCreditAsync(userId, amount);
        return Ok(result);
    }
    
}