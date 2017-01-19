using System;
using Microsoft.Extensions.DependencyInjection;
using Vendaloo.Contracts;
using Vendaloo.Data;
using Vendaloo.Services;

namespace Vendaloo
{
    public static class Startup
    {
        public static IServiceProvider GetProvider()
        {
            return Configure();
        }
        static IServiceProvider Configure()
        {
            return new ServiceCollection()
            .AddTransient<IVendingMachine, VendingMachine>()
            .AddTransient<IManageProducts, ManageProducts>()
            .AddTransient<IProductsStore, ProductsStore>()
            .AddTransient<IManageMoney, ManageMoney>()
            .BuildServiceProvider();
        }
    }
}