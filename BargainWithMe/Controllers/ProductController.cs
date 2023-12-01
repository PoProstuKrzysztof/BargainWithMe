using BargainWithMe.Core.Contracts.RepositoryContracts;
using BargainWithMe.Core.DTO;
using BargainWithMe.Core.Exceptions;
using BargainWithMe.Core.Mapper;
using BargainWithMe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BargainWithMe.Controllers;

[Route("/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private Mapper _mapper = new Mapper();

    public ProductController(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _repository.Product.GetAllAsync();
            return Ok(products);
        }
        catch (Exception)
        {
            throw new NoRecordsException("There are no records in database.");
        }
    }

    [HttpGet("{id}", Name = "GetProductByGuid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductByGuid(Guid id)
    {
        try
        {
            var product = await _repository.Product.GetProductByGuidAsync(id);

            return product == null ? NotFound() : Ok(_mapper.MapProductToProductDTO(product));
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    //[HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetProductsByCatalog()
    //{
    //}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProduct([FromBody] ProductDTO product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest("Product object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            _repository.Product.CreateProduct(_mapper.MapProductDtoToProduct(product));
            _repository.Save();

            return CreatedAtRoute("ProductById", new { id = product.Id }, product);
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
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest("Product object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var productEntity = await _repository.Product.GetProductByGuidAsync(product.Id);
            if (productEntity is null)
            {
                return NotFound("Product doesn't exist");
            }

            _repository.Product.UpdateProduct(productEntity);
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
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var product = await _repository.Product.GetProductByGuidAsync(id);
            if (product is null)
            {
                return NotFound("Product doesn't exist");
            }

            _repository.Product.DeleteProduct(product);
            await _repository.Save();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}