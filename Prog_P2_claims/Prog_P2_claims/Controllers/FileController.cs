using Microsoft.AspNetCore.Mvc;
using Prog_P2_claims.Areas.Identity.Data;
using Prog_P2_claims.Models;

namespace Prog_P2_claims.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public FileController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)

        {

            _context = context;

            _webHostEnvironment = webHostEnvironment;

        }



        public IActionResult Index()

        {

            // Retrieve the files from the database

            var files = _context.Files.ToList();

            return View(files); // Pass the file list to the view

        }



        [HttpPost]

        [RequestSizeLimit(10000000)] // Limit to 10 MB

        public async Task<IActionResult> SingleFileUpload(IFormFile SingleFile)

        {

            if (SingleFile == null || SingleFile.Length == 0)

            {

                ModelState.AddModelError("", "File not selected");

                return View("Index");

            }



            var permittedExtensions = new[] { ".jpg", ".png", ".gif" }; // add pdf here if you want 

            var extension = Path.GetExtension(SingleFile.FileName).ToLowerInvariant();



            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))

            {

                ModelState.AddModelError("", "Invalid file type.");

                return View("Index");

            }


            var mimeType = SingleFile.ContentType;

            var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };// file/pdf

            if (!permittedMimeTypes.Contains(mimeType))

            {

                ModelState.AddModelError("", "Invalid MIME type.");

                return View("Index");

            }



            if (ModelState.IsValid)

            {

                // Ensure the uploads directory exists

                var uploadsFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolderPath))

                {

                    Directory.CreateDirectory(uploadsFolderPath); // Create the directory if it doesn't exist

                }



                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(SingleFile.FileName);

                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);



                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))

                {

                    await SingleFile.CopyToAsync(stream);

                }



                var fileModel = new FileModel

                {

                    FileName = uniqueFileName,

                    Length = SingleFile.Length,

                    ContentType = mimeType,

                    Data = ConvertToByteArray(filePath)

                };



                _context.Files.Add(fileModel);

                await _context.SaveChangesAsync();



                return RedirectToAction("Index");

            }



            return View("Index");

        }



        // Method to handle file download

        public IActionResult DownloadFile(int id)

        {

            var file = _context.Files.FirstOrDefault(f => f.Id == id);

            if (file == null)

            {

                return NotFound();

            }



            return File(file.Data, file.ContentType, file.FileName);

        }



        // This method will convert the uploaded file into a byte array

        private byte[] ConvertToByteArray(string filePath)

        {

            byte[] fileData;



            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))

            {

                using (BinaryReader reader = new BinaryReader(fs))

                {

                    fileData = reader.ReadBytes((int)fs.Length);

                }

            }



            return fileData;

        }
    }
}
