using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.netCoreMvcCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_dot_net_core_mvc_crud_master.Controllers
{
    public class ApiController : Controller
    {
        private readonly KendaraanContext _context;

        public ApiController(KendaraanContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Kendaraan.ToListAsync();
            return Json(new{status = 200, data = data});
        }

        [HttpGet, ActionName("getById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            //List<Kendaraan> k = new List<Kendaraan>();
            var data = await _context.Kendaraan.FindAsync(id);
            if (data == null){
                return NotFound("data dengan id "+id+" tidak di temukan");
            }
                return Json(new { status = 200, data = data });
            
        }

        [HttpPost, ActionName("insert")]
        public async Task<IActionResult> Insert([FromBody] Kendaraan k)
        {
           
                _context.Add(k);
                var data = await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = k.id }, k);
        }

        [HttpPut, ActionName("update")]
        public async Task<IActionResult> Update(int id, [FromBody] Kendaraan k)
        {
            if (id != k.id)
            {
                return BadRequest();
            }

            _context.Entry(k).State = EntityState.Modified;

           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = k.id }, k);
        }

        [HttpDelete, ActionName("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Kendaraan.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            _context.Kendaraan.Remove(data);
            await _context.SaveChangesAsync();

            return NotFound("data dengan id "+id+" Berhasil di hapus");
        }
    }
}