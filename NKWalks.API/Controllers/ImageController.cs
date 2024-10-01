using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NKWalks.API.Models.Domain;
using NKWalks.API.Models.DTO;
using NKWalks.API.Repository;

namespace NKWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid) 
            {
                // Convert DTO to Domain Model

                var ImageDomainModel = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length
                };

                // User repository to upload image

                await imageRepository.UploadImageAsync(ImageDomainModel);

                return Ok(ImageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size more than 10MB, Please upload smaller size file.");
            }

        }
    }
}
