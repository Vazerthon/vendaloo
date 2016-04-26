using NUnit.Framework;
using Vendaloo.Services;

namespace Vendaloo.Tests
{
    [TestFixture]
    public class when_using_a_product_service
    {
        [Test]
        public void it_should_list_products()
        {
            var productService = new ManageProducts();
        }
    }
}
