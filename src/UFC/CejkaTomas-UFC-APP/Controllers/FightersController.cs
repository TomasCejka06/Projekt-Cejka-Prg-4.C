using CejkaTomas_UFC_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CejkaTomas_UFC_APP.Data;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class FightersController : Controller
{
    private readonly AppDbContext _context;

    public FightersController(AppDbContext context)
    {
        _context = context;
    }

    // VEŘEJNÝ SEZNAM FIGHTERŮ
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var fights = await _context.Fights
        .Include(f => f.FighterRed)
        .Include(f => f.FighterBlue)
        .Include(f => f.Winner)   
        .ToListAsync();

        return View(await _context.Fighters.ToListAsync());
    }

    // DETAIL
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var fighter = await _context.Fighters
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fighter == null) return NotFound();

        return View(fighter);
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Fighter fighter)
    {
        if (ModelState.IsValid)
        {
            _context.Add(fighter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fighter);
    }

    // EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var fighter = await _context.Fighters.FindAsync(id);
        if (fighter == null) return NotFound();

        return View(fighter);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Fighter fighter)
    {
        if (id != fighter.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(fighter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fighter);
    }

    // DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var fighter = await _context.Fighters.FirstOrDefaultAsync(f => f.Id == id);
        if (fighter == null) return NotFound();

        return View(fighter);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var fighter = await _context.Fighters.FindAsync(id);
        _context.Fighters.Remove(fighter);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
