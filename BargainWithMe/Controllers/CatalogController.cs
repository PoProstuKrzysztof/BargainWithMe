using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.DTO;
using BargainWithMe.Core.Exceptions;
using BargainWithMe.Core.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BargainWithMe.Controllers;

[Route("/catalog")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CatalogController : Controller
{
    private readonly IRepositoryWrapper _repository;
    private readonly Mapper _mapper = new Mapper();

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
            var catalogs = await _repository.Catalog.GetAllCatalogsAsync();
            return Ok(catalogs.Select(x => _mapper.MapCatalogToCatalogDTO(x)));
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
            var catalog = await _repository.Catalog.GetProductByGuidAsync(id);

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
    public async Task<IActionResult> CreateCatalogAsync([FromBody] CatalogDTO catalog)
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
            await _repository.Save();

            return CreatedAtRoute("GetCatalogById", new { Id = catalog.Id }, catalog);
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

            var catalogEntity = await _repository.Catalog.GetProductByGuidAsync(catalog.Id);
            if (catalogEntity is null)
            {
                return NotFound("Catalog doesn't exist");
            }

            _repository.Catalog.UpdateCatalog(_mapper.MapCatalogDtoToCatalog(catalog));
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
            var catalog = await _repository.Catalog.GetProductByGuidAsync(id);
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

    [HttpGet("/AllCatalogsWithProdutcs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCatalogsWithProducts()
    {
        try
        {
            // We're obtaining catalogs and products in them.
            var catalogs = await _repository.Catalog.GetAllCatalogsAsync();
            var catalogsWithProducts = new List<CatalogWithProductsDTO>();

            foreach (var catalog in catalogs)
            {
                var productsInCatalog = await _repository.Product.GetAllProductsByCatalogAsync(catalog.Id);
                var productsInCatalogDTO = productsInCatalog.Select(product => _mapper.MapProductToProductDTO(product)).ToList();

                catalogsWithProducts.Add(new CatalogWithProductsDTO
                {
                    Catalog = _mapper.MapCatalogToCatalogDTO(catalog),
                    Products = productsInCatalogDTO
                });
            }

            return Ok(catalogsWithProducts);
        }
        catch (Exception)
        {
            throw new NoRecordsException("There are no records in the database.");
        }
    }

    [HttpGet("/CatalogWithProdutcs/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCatalogWithProdutcs(Guid id)
    {
        try
        {
            // We're obtaining catalog and products in it.
            var specificCatalog = await _repository.Catalog.GetProductByGuidAsync(id);
            var catalogWithProducts = new List<CatalogWithProductsDTO>();

            specificCatalog.Products = await _repository.Product.GetAllProductsByCatalogAsync(id);

            foreach (var catalog in specificCatalog.Products)
            {
                // We look for products in catalog and map it to DTO
                var productsInCatalogDTO = specificCatalog.Products
                    .Select(product => _mapper.MapProductToProductDTO(product))
                    .ToList();

                catalogWithProducts.Add(new CatalogWithProductsDTO
                {
                    Catalog = _mapper.MapCatalogToCatalogDTO(specificCatalog),
                    Products = productsInCatalogDTO
                });
            }

            return Ok(catalogWithProducts);
        }
        catch (Exception)
        {
            throw new NoRecordsException("There are no records in the database.");
        }
    }
}