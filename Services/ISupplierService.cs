using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface ISupplierService
    {
        void AddSupplier(Supplier supplier);
        void DeleteSupplier(int supplierId);
        IEnumerable<Supplier> GetAllSuppliers(int pageNumber, int pageSize, string? name = null);
        Supplier GetSupplierById(int supplierId);
        Supplier GetSupplierByName(string supplierName);
        void UpdateSupplier(Supplier supplier);
    }
}