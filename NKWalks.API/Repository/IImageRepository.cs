using NKWalks.API.Models.Domain;

namespace NKWalks.API.Repository
{
    public interface IImageRepository
    {
       Task<Image> UploadImageAsync(Image image);
    }
}
