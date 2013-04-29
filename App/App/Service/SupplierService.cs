using App.Domain;
using App.Service.Contract;

namespace App.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ICreator creator;
        private readonly IPermissionService permissionService;
        private readonly User user;

        public SupplierService(ICreator creator, UserContext myUserContext, IPermissionService permissionSrv)
        {
            this.creator = creator;
            user = myUserContext.User;
            permissionService = permissionSrv;
        }

        public BaseResponse AddSupplier(Supplier supplier)
        {
            var baseResponse = new BaseResponse();

            if (creator.IsSatisfiedBy(supplier) && permissionService.HasRight(user, Right.SupplierManagement))
            {
                baseResponse.Message = "Added supplier.";
            }
            else
            {
                baseResponse.Success = false;
                baseResponse.Message = "Could not add supplier.";
            }

            return baseResponse;
        }
    }
}