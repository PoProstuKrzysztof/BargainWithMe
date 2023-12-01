using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.DTO;
using BargainWithMe.Core.Exceptions;
using BargainWithMe.Core.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BargainWithMe.Controllers;

[Route("/catalog")]
[ApiController]
public class CatalogController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private Mapper _mapper = new Mapper();

    public CatalogController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCatalogs()
    {
        try
        {
            var catalogs = await _repository.Catalog.GetAll();
            return Ok(catalogs);
        }
        catch (Exception)
        {
            throw new NoRecordsException("There are no records in the database.");
        }
    }

    [HttpGet("{id}", Name = "GetCatalogById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatalogById(Guid id)
    {
        try
        {
            var catalog = await _repository.Catalog.GetByGuid(id);

            return catalog == null ? NotFound() : Ok(_mapper.MapCatalogToCatalogDTO(catalog));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateCatalog([FromBody] CatalogDTO catalog)
    {
        try
        {
            if (catalog is null)
            {
                return BadRequest("Catalog object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            _repository.Catalog.CreateCatalog(_mapper.MapCatalogDtoToCatalog(catalog));
            _repository.Save();

            return CreatedAtRoute("CatalogById", new { id = catalog.Id }, catalog);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCatalog([FromBody] CatalogDTO catalog)
    {
        try
        {
            if (catalog is null)
            {
                return BadRequest("Catalog object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var catalogEntity = await _repository.Catalog.GetByGuid(catalog.Id);
            if (catalogEntity is null)
            {
                return NotFound("Catalog doesn't exist");
            }

            _repository.Catalog.UpdateCatalog(catalogEntity);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCatalog(Guid id)
    {
        try
        {
            var catalog = await _repository.Catalog.GetByGuid(id);
            if (catalog is null)
            {
                return NotFound("Catalog doesn't exist");
            }

            _repository.Catalog.DeleteCatalog(catalog);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}