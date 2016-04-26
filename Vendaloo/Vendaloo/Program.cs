using System;
using Ninject;
using Vendaloo.AppStart;
using Vendaloo.Contracts;

namespace Vendaloo
{
    class Program
    {
        static readonly IKernel Kernel = NinjectConfig.GetKernel();
        static readonly IVendingMachine VendingMachine = Kernel.Get<IVendingMachine>();

        static void Main()
        {
            var vend = 'y';
            while (vend == 'y')
            {
                PrintProductList();
                vend = PrintExitMessage();
            }
        }

        static char PrintExitMessage()
        {
            Console.WriteLine("Continue? (y/n)");
            return Console.ReadKey().KeyChar;
        }

        static void PrintProductList()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ #\t║\tProduct\t\t║\tPrice\t║\tStock\t║");
            Console.WriteLine("║                                                               ║");
            foreach (var product in VendingMachine.ListProducts())
            {
                Console.WriteLine($"║ {product.Id}\t║\t{product.Name.PadRight(12, ' ')}\t║\t{product.Price.ToString("C")}\t║\t{product.Stock}\t║");
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }
    }
}
