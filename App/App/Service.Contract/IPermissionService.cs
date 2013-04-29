using App.Domain;

namespace App.Service.Contract
{
    public interface IPermissionService
    {
        bool HasRight(User user, Right right); 
    }
}