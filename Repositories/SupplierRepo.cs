using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace E_CommerceSystem.Repositories
{
    public class SupplierRepo : ISupplierRepo
    {
        public ApplicationDbContext _context;
        public SupplierRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            try
            {
                return _context.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public Supplier GetSupplierById(int supplierId)
        {
            try
            {
                return _context.Suppliers
                    .Include(s => s.Products)
                    .FirstOrDefault(p => p.SupplierId == supplierId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public Supplier GetSupplierByName(string supplierName)
        {
            try
            {
                return _context.Suppliers.FirstOrDefault(p => p.SupplierName == supplierName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Update(supplier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            try
            {
                var supplier = GetSupplierById(supplierId);
                if (supplier != null)
                {
                    _context.Suppliers.Remove(supplier);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }
    }
}
