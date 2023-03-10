using EFP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FastExcel;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EFP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _hostingEnvironment;
        private DefaultContext _context;

        public HomeController(ILogger<HomeController> logger, 
            IWebHostEnvironment env, DefaultContext ctx)
        {
            _logger = logger;
            _hostingEnvironment = env;
            _context = ctx;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Customer model)
        {
            IFormFile fl = this.HttpContext.Request.Form.Files[0];
            if (ModelState.IsValid && fl != null)
            {
                
                _context.Customers.Add(model);
                _context.SaveChanges();
                var user = _context.Customers
                    .FirstOrDefault(x => x.FirstName == model.FirstName &&
                    x.LastName == model.LastName && x.DateOfBirth == model.DateOfBirth);
                byte[] fileBytes;
                using ( var ms = new MemoryStream())
                {
                    fl.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                var uploadData = new Upload
                {
                    FileName = fl.FileName,
                    DateUploaded = DateTime.Now,
                    data = fileBytes,
                    UserId = user.UserId
                };
                _context.Uploads.Add(uploadData);
                _context.SaveChanges();
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploads, fl.FileName);
                var viewModel = new CustomerViewModel()
                {
                    Cust = model,
                    Finances = GetData(fl),
                    uploadPath = filePath
                };
                return View("Result", viewModel);
            }
            ModelState.AddModelError("", "Invalid data");
            return View(model);
        }
        protected List<CustomerFinance> GetData(IFormFile f)
        {
            List<CustomerFinance> list = new List<CustomerFinance>();
            string path = "";
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (f.Length > 0)
            {
                string filePath = Path.Combine(uploads, f.FileName);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    f.CopyTo(filestream);
                }
                path = filePath;
            }
            var file = new FileInfo(path);
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(file, true))
            {
                foreach(var worksheet in fastExcel.Worksheets)
                {
                    worksheet.Read();
                    var rows = worksheet.Rows.ToArray();
                    for(int i = 1; i < rows.Length; i++)
                    {
                        list.Add(new CustomerFinance()
                        {
                            Month = Convert.ToString(rows[i].GetCellByColumnName("A").Value),
                            Income = Convert.ToDouble(rows[i].GetCellByColumnName("B").Value),
                            Expense = Convert.ToDouble(rows[i].GetCellByColumnName("C").Value)
                        });
                    }
                }
            }
            return list;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
