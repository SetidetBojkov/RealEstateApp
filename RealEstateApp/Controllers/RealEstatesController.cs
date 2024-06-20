using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RealEstatesController : Controller
{

    private readonly ApplicationDbContext _context;

    public RealEstatesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RealEstates
    public async Task<IActionResult> Index()
    {
        return View(await _context.RealEstates.ToListAsync());
    }

    // GET: RealEstates/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var realEstate = await _context.RealEstates
            .FirstOrDefaultAsync(m => m.Id == id);
        if (realEstate == null)
        {
            return NotFound();
        }

        return View(realEstate);
    }

    // GET: RealEstates/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: RealEstates/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,Address,Price,ImageUrl")] RealEstate realEstate)
    {
        if (ModelState.IsValid)
        {
            _context.Add(realEstate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(realEstate);
    }

    // GET: RealEstates/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var realEstate = await _context.RealEstates.FindAsync(id);
        if (realEstate == null)
        {
            return NotFound();
        }
        return View(realEstate);
    }

    // POST: RealEstates/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Address,Price,ImageUrl")] RealEstate realEstate)
    {
        if (id != realEstate.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(realEstate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateExists(realEstate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(realEstate);
    }

    // GET: RealEstates/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var realEstate = await _context.RealEstates
            .FirstOrDefaultAsync(m => m.Id == id);
        if (realEstate == null)
        {
            return NotFound();
        }

        return View(realEstate);
    }

    // POST: RealEstates/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var realEstate = await _context.RealEstates.FindAsync(id);
        _context.RealEstates.Remove(realEstate);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RealEstateExists(int id)
    {
        return _context.RealEstates.Any(e => e.Id == id);
    }

}
