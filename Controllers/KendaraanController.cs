using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.netCoreMvcCrud.Models;
using System.IO;
using System;
using System.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using OfficeOpenXml.Style;
using System.Drawing;
using ExcelDataReader;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.netCoreMvcCrud.Controllers
{
    public class KendaraanController : Controller
    {
        private readonly KendaraanContext _context;

        public KendaraanController(KendaraanContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        // GET:
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kendaraan.ToListAsync());
        }

        // GET: /Create
        public IActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new Kendaraan());
            else
                return View(_context.Kendaraan.Find(id));
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("id,nama,model,merek,transmisi,tahun,harga")] Kendaraan k)
        {
            if (ModelState.IsValid)
            {
                if (k.id == 0)
                    _context.Add(k);
                else
                    _context.Update(k);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(k);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var kendaraan = await _context.Kendaraan.FindAsync(id);
            _context.Kendaraan.Remove(kendaraan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public FileContentResult Download()
        {

            var fileDownloadName = String.Format("file.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            // Pass your ef data to method
            ExcelPackage package = GenerateExcelFile(_context.Kendaraan.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFile(IEnumerable<Kendaraan> datasource)
        {

            ExcelPackage pck = new ExcelPackage();

            //Create the worksheet 
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");

            // Sets Headers
            ws.Cells[1, 1].Value = "ID";
            ws.Cells[1, 2].Value = "Nama";
            ws.Cells[1, 3].Value = "Model";
            ws.Cells[1, 4].Value = "Merek";
            ws.Cells[1, 5].Value = "Transmisi";
            ws.Cells[1, 6].Value = "Tahun";
            ws.Cells[1, 7].Value = "Harga";

            // Inserts Data
            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).id;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).nama;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).model;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).merek;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).transmisi;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).tahun;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).harga;
            }

            // Format Header of Table
            using (ExcelRange rng = ws.Cells["A1:G1"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                rng.Style.Fill.BackgroundColor.SetColor(Color.Gold); //Set color to DarkGray 
                rng.Style.Font.Color.SetColor(Color.Black);
            }
            return pck;
        }

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<Kendaraan>> Import(IFormFile importFile)
        {
            List<Kendaraan> list = new List<Kendaraan>();
            var file =importFile.OpenReadStream();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
 
            using(var reader = ExcelReaderFactory.CreateReader(file)) {

                while (reader.Read()) //Each row of the file
                {
                    if(reader.Depth > 0){
                        list.Add(new Kendaraan(){
                            //id = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0).ToString()),
                            nama = reader.IsDBNull(1) ? null : reader.GetValue(1).ToString(),
                            model = reader.IsDBNull(2) ? null : reader.GetValue(2).ToString(),
                            merek = reader.IsDBNull(3) ? null : reader.GetValue(3).ToString(),
                            transmisi = reader.IsDBNull(4) ? null : reader.GetValue(4).ToString(),
                            tahun = reader.IsDBNull(5) ? 0 : Convert.ToInt32(reader.GetValue(5).ToString()),
                            harga = reader.IsDBNull(6) ? 0 : Convert.ToInt32(reader.GetValue(6).ToString()),
                        });
                    }
                }
            }
            foreach(var i in list){
                _context.Add(i);
                await _context.SaveChangesAsync();
            }
            Console.WriteLine(JsonSerializer.Serialize(list));
            return list;
        }
    }
}
