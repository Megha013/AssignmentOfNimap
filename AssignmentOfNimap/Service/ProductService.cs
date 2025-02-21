using AssignmentOfNimap.Models;
using AssignmentOfNimap.Repository;

namespace AssignmentOfNimap.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repo;
        public ProductService(IProductRepository repo)
        {
            this.repo = repo;
        }
        public int AddProduct(Product prod)
        {
            return repo.AddProduct(prod);
        }

        public int DeleteProduct(int id)
        {
            return repo.DeleteProduct(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return repo.GetProducts();
        }

        public Product GetProductById(int id)
        {
            return repo.GetProductById(id);
        }

        public int UpdateProduct(Product prod)
        {
            return repo.UpdateProduct(prod);
        }
    }
}
