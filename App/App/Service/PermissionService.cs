using App.Domain;
using App.Service.Contract;

namespace App.Service
{
    public class PermissionService : IPermissionService
    {
        public bool HasRight(User user, Right right)
        {
            return user.Group != null && user.Group.Rights.Contains(right);
        }
    }
}