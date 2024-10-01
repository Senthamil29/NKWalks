using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NKWalks.API.CustomActionFilters;
using NKWalks.API.Data;
using NKWalks.API.Models.Domain;
using NKWalks.API.Models.DTO;
using NKWalks.API.Repository;

namespace NKWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        // Create Walk --> POST
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksDTO addWalksDTO)
        {
            // Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalksDTO);

            await walkRepository.CreateWalkAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery , [FromQuery] string? sortBy, 
            [FromQuery] bool? isAsc, [FromQuery] int PageNumber = 1, [FromQuery] int pageSize =1000)
        {
            var walkDomainModel = await walkRepository.GetWalksAsync(filterOn,filterQuery,sortBy,isAsc ?? true,PageNumber,pageSize);

            // Create a Custom exception

            throw new Exception("This is a new custom exception for testing.");

            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkDTO updateWalkDTO)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDTO);

            walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteWalkAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

    }
}
