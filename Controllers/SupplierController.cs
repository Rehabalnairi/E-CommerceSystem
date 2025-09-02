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
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var supplier = _supplierService.GetSupplierById(id);
            var dto = new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                ProductCount = supplier.Products?.Count ?? 0
            };
            return Ok(dto);
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
            var dto = new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                ProductCount = supplier.Products?.Count ?? 0
            };
            return Ok(dto);
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

            var dto = new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                ProductCount = supplier.Products?.Count ?? 0
            };

            return Ok(dto);
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

            var dto = new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                ProductCount = supplier.Products?.Count ?? 0
            };

            return Ok(dto);
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
