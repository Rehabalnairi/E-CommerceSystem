using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;

namespace E_CommerceSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepo _supplierRepo;

        public SupplierService(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public IEnumerable<Supplier> GetAllSuppliers(int pageNumber, int pageSize, string? name = null)
        {
            var query = _supplierRepo.GetAllSuppliers();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.SupplierName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            var pagedSuppliers = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pagedSuppliers;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            var supplier = _supplierRepo.GetSupplierById(supplierId);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier with ID {supplierId} not found.");
            return supplier;
        }

        public Supplier GetSupplierByName(string supplierName)
        {
            var supplier = _supplierRepo.GetSupplierByName(supplierName);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier with Name '{supplierName}' not found.");
            return supplier;
        }

        public void AddSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                throw new ArgumentException("Supplier name is required.");

            var existing = _supplierRepo.GetSupplierByName(supplier.SupplierName);
            if (existing != null)
                throw new InvalidOperationException($"Supplier '{supplier.SupplierName}' already exists.");

            _supplierRepo.AddSupplier(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            var existing = _supplierRepo.GetSupplierById(supplier.SupplierId);
            if (existing == null)
                throw new KeyNotFoundException($"Supplier with ID {supplier.SupplierId} not found.");

            _supplierRepo.UpdateSupplier(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            var existing = _supplierRepo.GetSupplierById(supplierId);
            if (existing == null)
                throw new KeyNotFoundException($"Supplier with ID {supplierId} not found.");

            _supplierRepo.DeleteSupplier(supplierId);
        }

        public IEnumerable<SupplierDTO> GetAllSuppliersDTO(int pageNumber, int pageSize, string? name = null)
        {
            var query = _supplierRepo.GetAllSuppliers();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(s => s.SupplierName.Contains(name, StringComparison.OrdinalIgnoreCase));

            var pagedSuppliers = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SupplierDTO
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    ContactEmail = s.ContactEmail,
                    Phone = s.Phone,
                    ProductCount = s.Products?.Count ?? 0
                })
                .ToList();

            return pagedSuppliers;
        }

    }
}
