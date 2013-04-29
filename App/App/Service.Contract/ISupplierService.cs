using App.Domain;

namespace App.Service.Contract
{
    public interface ISupplierService
    {
        BaseResponse AddSupplier(Supplier supplier); 
    }
}