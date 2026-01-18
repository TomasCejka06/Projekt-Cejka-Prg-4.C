using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CejkaTomas_UFC_APP.Models;
using CejkaTomas_UFC_APP.Data;


[Authorize]
public class FightsController : Controller
{
    private readonly AppDbContext _context;

    public FightsController(AppDbContext context)
    {
        _context = context;
    }

    // VEŘEJNÝ SEZNAM ZÁPASŮ
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var fights = _context.Fights
            .Include(f => f.FighterRed)
            .Include(f => f.FighterBlue)
            .Include(f => f.Winner);

        return View(await fights.ToListAsync());
    }

    // CREATE
    public IActionResult Create()
    {
        ViewData["FighterRedId"] = new SelectList(_context.Fighters, "Id", "Name");
        ViewData["FighterBlueId"] = new SelectList(_context.Fighters, "Id", "Name");
        ViewData["WinnerId"] = new SelectList(_context.Fighters, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Fights fight)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["FighterRedId"] = new SelectList(_context.Fighters, "Id", "Name", fight.FighterRedId);
        ViewData["FighterBlueId"] = new SelectList(_context.Fighters, "Id", "Name", fight.FighterBlueId);
        ViewData["WinnerId"] = new SelectList(_context.Fighters, "Id", "Name", fight.WinnerId);

        return View(fight);
    }

    // DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var fight = await _context.Fights
            .Include(f => f.FighterRed)
            .Include(f => f.FighterBlue)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fight == null) return NotFound();

        return View(fight);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var fight = await _context.Fights.FindAsync(id);
        _context.Fights.Remove(fight);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
