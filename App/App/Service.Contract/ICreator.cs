using App.Domain;

namespace App.Service.Contract
{
    public interface ICreator
    {
        bool IsSatisfiedBy(Supplier supplier); 
    }
}