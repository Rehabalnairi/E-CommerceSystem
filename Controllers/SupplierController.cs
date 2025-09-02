using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class SupplierController : ControllerBase
{
    private readonly SupplierService _supplierService;

    public SupplierController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll(int pageNumber = 1, int pageSize = 10, string? name = null)
    {
        try
        {
            var suppliers = _supplierService.GetAllSuppliers(pageNumber, pageSize, name);
            return Ok(suppliers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving suppliers. {ex.Message}");
        }
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var supplier = _supplierService.GetSupplierById(id);
            return Ok(supplier);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the supplier. {ex.Message}");
        }
    }

    [HttpGet("GetByName")]
    public IActionResult GetByName(string name)
    {
        try
        {
            var supplier = _supplierService.GetSupplierByName(name);
            return Ok(supplier);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the supplier. {ex.Message}");
        }
    }
    [HttpPost("Add")]
    public IActionResult Add([FromBody] Supplier supplier)
    {
        try
        {
            if (supplier == null)
                return BadRequest("Supplier data is required");

            _supplierService.AddSupplier(supplier);
            return Ok(supplier);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the supplier. {ex.Message}");
        }
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Supplier supplier)
    {
        try
        {
            if (supplier == null)
                return BadRequest("Supplier data is required");

            _supplierService.UpdateSupplier(supplier);
            return Ok(supplier);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the supplier. {ex.Message}");
        }
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _supplierService.DeleteSupplier(id);
            return Ok($"Supplier with ID {id} deleted successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the supplier. {ex.Message}");
        }
    }
}
