using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public WalletsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Wallet>> GetWallet(int id)
    {
        var wallet = await _appDbContext.wallet.FindAsync(id);
        if (wallet == null) return NotFound();
        return Ok(wallet);
    }

    [HttpPost]
    public async Task<ActionResult<Wallet>> CreateWallet(Wallet wallet)
    {
        wallet.LastUpdated = DateTime.UtcNow;

        _appDbContext.wallet.Add(wallet);
        await _appDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWallet), new { id = wallet.Id }, wallet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWallet(int id, Wallet updated)
    {
        var wallet = await _appDbContext.wallet.FindAsync(id);
        if (wallet == null) return NotFound();

        // Only metadata is editable — Balance changes only via Expense create/update/delete
        wallet.Currency = updated.Currency;
        wallet.LastUpdated = DateTime.UtcNow;

        await _appDbContext.SaveChangesAsync();
        return NoContent();
    }
}