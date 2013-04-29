using App.Domain;

namespace App.Repository.Contract
{
    public interface ISupplierRepository
    {
        bool Contains(Supplier supplier); 
    }
}