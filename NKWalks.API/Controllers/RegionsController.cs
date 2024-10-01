using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NKWalks.API.CustomActionFilters;
using NKWalks.API.Data;
using NKWalks.API.Models.Domain;
using NKWalks.API.Models.DTO;
using NKWalks.API.Repository;
using System.Text.Json;

namespace NKWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NKWalkDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NKWalkDBContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            #region Before using Mapping
            // Get data from databases --  Domain Model

            // Map Domain Models to DTO
            //var regionDTO = new List<RegionDTO>();

            //foreach (var region in regionDomain) 
            //{
            //    regionDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            // Map Domain Models to DTO using AutoMapper
            #endregion

            //throw new Exception("This is a Custom Expection");

            logger.LogInformation("Get All Region Data Action Method is invoked");

            var regionDomain = await regionRepository.GetAllRegionAsync();

            logger.LogInformation($"Finished  Get All Region request with data : {JsonSerializer.Serialize(regionDomain)}");

            return Ok(mapper.Map<List<RegionDTO>>(regionDomain));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            // var region = dbContext.Regions.Find(id); --> Find() method used for primary key only u can't used for name or dept

            var regionDomain = await regionRepository.GetRegionByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map Domain Models to DTO
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegions([FromBody] AddRegionDTO addRegionDTO)
        {
            //var regionDomainModel = new Region
            //{
            //    Name = addRegionDTO.Name,
            //    Code = addRegionDTO.Code,
            //    RegionImageUrl = addRegionDTO.RegionImageUrl
            //};

            //if (ModelState.IsValid) // to check validation in custom validation
            //{

            var regionDomainModel = mapper.Map<Region>(addRegionDTO);

            await regionRepository.CreateAsync(regionDomainModel);

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
            //}
            //else
            //{
            //    return BadRequest();
            //}
            // nameof() --> nameof is a C# operator that returns the name of the method as a string.
            //In Response Header - it generate URL location (it is action method)


            //new { id = regionDomainModel.Id } --> the route value needeed to generate of URL

            // regionDTO --> return response boby 

        }

        // Update 
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            // Map DTO to Domaim Model
            var regionDomainModel = mapper.Map<Region>(updateRegionDTO);

            //Check if region exist
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);

        }
    }
}
