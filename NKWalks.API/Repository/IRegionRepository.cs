using NKWalks.API.Models.Domain;

namespace NKWalks.API.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();

        Task<Region?> GetRegionByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id,Region region);

        Task<Region?> DeleteAsync(Guid id);
    } 
}
