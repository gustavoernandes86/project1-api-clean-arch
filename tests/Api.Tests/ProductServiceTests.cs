using System.Linq;
using System.Threading.Tasks;
using Project1.Application.Services;
using Project1.Infrastructure.Repositories;
using Xunit;

namespace Project1.Api.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Create_Should_Add_Product()
        {
            var repo = new InMemoryProductRepository();
            var service = new ProductService(repo);

            var dto = await service.CreateAsync("Teste", 10m, "desc");
            var all = (await service.GetAllAsync()).ToList();

            Assert.NotNull(dto);
            Assert.Contains(all, p => p.Name == "Teste");
        }
    }
}
