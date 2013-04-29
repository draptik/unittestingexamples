using App.Domain;
using App.Repository.Contract;
using App.Service.Contract;

namespace App.Service
{
    public class SupplierCreator : ICreator
    {
        private readonly ISupplierRepository repository;

        public SupplierCreator(ISupplierRepository supplierRepository)
        {
            repository = supplierRepository;
        }

        public bool IsSatisfiedBy(Supplier supplier)
        {
            return !repository.Contains(supplier);
        }
    }
}