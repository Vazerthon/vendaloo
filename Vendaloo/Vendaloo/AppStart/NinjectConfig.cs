using Ninject;
using Vendaloo.Contracts;
using Vendaloo.Data;
using Vendaloo.Services;

namespace Vendaloo.AppStart
{
    static class NinjectConfig
    {
        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel();
            AddBindings(kernel);
            return kernel;
        }

        static void AddBindings(IKernel kernel)
        {
            kernel.Bind<IVendingMachine>().To <VendingMachine>();
            kernel.Bind<IManageProducts>().To <ManageProducts>();
            kernel.Bind<IProductsStore>().To <ProductsStore>();
            kernel.Bind<IManageMoney>().To <ManageMoney>();
        }
    }
}
