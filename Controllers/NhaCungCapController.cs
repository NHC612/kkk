
using System;
using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Testthi.Models;
using Testthi.Models.Process;

namespace Testthi.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly LTQLDb _context;
        StringProcess NCC = new StringProcess();

        public NhaCungCapController(LTQLDb context)
        {
            _context = context;
        }

        // GET: NhaCungCap
        public async Task<IActionResult> Index()
        {
              return _context.NhaCungCap != null ? 
                          View(await _context.NhaCungCap.ToListAsync()) :
                          Problem("Entity set 'LTQLDb.NhaCungCap'  is null.");
        }

        // GET: NhaCungCap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap
                .FirstOrDefaultAsync(m => m.MaNhaCungCap == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Create
        public IActionResult Create()
        {
             // Sinh ma tu dong
            var newID = "";
            if(_context.NhaCungCap.Count() == 0){
                newID = "NCC01";
            }
            else{
                var CC = _context.NhaCungCap.OrderByDescending(x=>x.MaNhaCungCap).First().MaNhaCungCap;
                newID = NCC.AutoGenerateKey((CC.ToString()));
            }
            ViewBag.MaNhaCungCap= newID;
            // end
            return View();
        }

        // POST: NhaCungCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhaCungCap,TenNhaCungCap")] NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaCungCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("MaNhaCungCap,TenNhaCungCap")] NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.MaNhaCungCap)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaCungCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaCungCapExists(nhaCungCap.MaNhaCungCap))
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
            return View(nhaCungCap);
        }

        // GET: NhaCungCap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhaCungCap == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _context.NhaCungCap
                .FirstOrDefaultAsync(m => m.MaNhaCungCap == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        // POST: NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.NhaCungCap == null)
            {
                return Problem("Entity set 'LTQLDb.NhaCungCap'  is null.");
            }
            var nhaCungCap = await _context.NhaCungCap.FindAsync(id);
            if (nhaCungCap != null)
            {
                _context.NhaCungCap.Remove(nhaCungCap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaCungCapExists(int? id)
        {
          return (_context.NhaCungCap?.Any(e => e.MaNhaCungCap == id)).GetValueOrDefault();
        }
    }
}
