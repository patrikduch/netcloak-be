using Microsoft.EntityFrameworkCore;
using NetCloak.Persistence.Contexts;

namespace StyleCop.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public OrderController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _dbContext.Orders.FirstOrDefaultAsync();

        return Ok(result);
    }
}
