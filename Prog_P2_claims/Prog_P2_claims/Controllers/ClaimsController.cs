using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog_P2_claims.Areas.Identity.Data;
using Prog_P2_claims.Models;

namespace Prog_P2_claims.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClaimsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Claims()
        {
            return View();
        }


        //working one 
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var claims = await _context.Claims.ToListAsync();
            return View("List", claims);  // add "List " before claim of issue is found 
            // Ensure this matches the name of the view file
        }




        //public async Task<IActionResult> List()
        //{
        //    var claims = await _context.Claims.ToListAsync();
        //    return View("List", claims);  // Ensure the view name matches exactly
        //}



        //[HttpPost]
        //public async Task<IActionResult> Claims(Claims claims)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Save supporting documents


        //        if (claims.SupportingDocuments != null && claims.SupportingDocuments.Any())
        //        {
        //            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,
        //                "uploads");
        //            if (!Directory.Exists(uploadPath))
        //            {
        //                Directory.CreateDirectory(uploadPath);
        //            }

        //            foreach (var file in claims.SupportingDocuments)
        //            {
        //                if (file.Length > 0)
        //                {
        //                    string fileName = Guid.NewGuid().ToString()
        //                        + Path.GetExtension(file.FileName);
        //                    string filePath = Path.Combine(uploadPath, fileName);
        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await file.CopyToAsync(stream);
        //                    }
        //                }
        //            }
        //        }

        //        // Calculate total hours
        //        claims.TotalHours = claims.HoursWorked * claims.RateHour;
        //        _context.Add(claims);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(ClaimSubmitted));
        //    }
        //    return View(claims);
        //}

        [HttpPost]
        public async Task<IActionResult> Claims(Claims claims)
        {
            if (ModelState.IsValid)
            {

                claims.TotalHours = claims.HoursWorked * claims.RateHour;


                _context.Add(claims);
                await _context.SaveChangesAsync();


                if (claims.SupportingDocuments != null && claims.SupportingDocuments.Any())
                {
                    var permittedExtensions = new[] { ".jpg", ".png", ".gif", ".pdf" };
                    var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "application/pdf" };

                    foreach (var file in claims.SupportingDocuments)
                    {
                        if (file.Length > 0)
                        {
                            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                            {
                                ModelState.AddModelError("", "Invalid file type.");
                                return View(claims);
                            }

                            var mimeType = file.ContentType;
                            if (!permittedMimeTypes.Contains(mimeType))
                            {
                                ModelState.AddModelError("", "Invalid MIME type.");
                                return View(claims);
                            }


                            byte[] fileData;
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                fileData = memoryStream.ToArray();
                            }

                            var fileModel = new FileModel
                            {
                                FileName = file.FileName,
                                Length = file.Length,
                                ContentType = mimeType,
                                Data = fileData

                            };

                            _context.Files.Add(fileModel);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(ClaimSubmitted), new { id = claims.Id });
                //return RedirectToAction(nameof(ClaimSubmitted));
            }
            return View(claims);
        }

        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                TempData["Message"] = "Claim not found";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            claim.Status = "Approved";
            _context.SaveChanges();
            TempData["Message"] = "Your claim has been approved by HR";
            TempData["MessageType"] = "success";
            return RedirectToAction("List");
        }
        //reject method
        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                TempData["Message"] = "Claim not found";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            claim.Status = "Rejected";
            _context.SaveChanges();
            TempData["Message"] = "Your claim has been rejected by HR";
            TempData["MessageType"] = "error";
            return RedirectToAction("List");
        }

        //public IActionResult ClaimSubmitted()
        //{
        //    return View();
        //}

        public async Task<IActionResult> ClaimSubmitted(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            // Retrieve the file associated with the claim (if any)
            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

            // Pass the file via ViewBag (since you prefer not to modify models)
            ViewBag.File = file;

            return View(claim);
        }
    }


}