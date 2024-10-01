using System.ComponentModel.DataAnnotations;

namespace NKWalks.API.Models.DTO
{
    public class AddRegionDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 character")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 character")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 character")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
