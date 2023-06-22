using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Testthi.Models;
using Super_Book_Store.Models.Process;
namespace Testthi.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly LTQLDb _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public SanPhamController(LTQLDb context)
        {
            _context = context;
        }

        // GET: SanPham
        public async Task<IActionResult> Index()
        {
            var lTQLDb = _context.SanPham.Include(s => s.NhaCungCap);
            return View(await lTQLDb.ToListAsync());
        }

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPham/Create
        public IActionResult Create()
        {
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCap, "MaNhaCungCap", "MaNhaCungCap");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,TenSanPham,MaNhaCungCap")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCap, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCap, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSanPham,TenSanPham,MaNhaCungCap")] SanPham sanPham)
        {
            if (id != sanPham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSanPham))
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
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCap, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.NhaCungCap)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SanPham == null)
            {
                return Problem("Entity set 'LTQLDb.SanPham'  is null.");
            }
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPham.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(string id)
        {
          return (_context.SanPham?.Any(e => e.MaSanPham == id)).GetValueOrDefault();
        }
          public async Task<IActionResult>Upload()
        {
            return View();
        }
        [HttpPost]
          [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file){

            if(file != null){
                string fileExtension = Path.GetExtension(file.FileName);
                if(fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("","Please choose excel file to upload!");
                }
                else{
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for(int i = 0; i< dt.Rows.Count; i++)
                        {
                            var emp = new SanPham();

                            emp.MaSanPham = dt.Rows[i][0].ToString();
                            emp.TenSanPham = dt.Rows[i][1].ToString();
                            emp.MaNhaCungCap = int.Parse(dt.Rows[i][2].ToString());
                          


                            _context.SanPham.Add(emp);
                        } 

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}

