using AssignmentOfNimap.Data;
using AssignmentOfNimap.Models;

namespace AssignmentOfNimap.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;
        public ProductRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddProduct(Product prod)
        {
            int result = 0;
            db.Products.Add(prod);
            result = db.SaveChanges();
            return result;
        }

        public int DeleteProduct(int id)
        {
            int result = 0;
            var c = db.Products.Where(x => x.ProductId == id).SingleOrDefault();
            if (c != null)
            {
                db.Products.Remove(c);
                result = db.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Product> GetProducts()
        {
            var res = (from p in db.Products
                       join c in db.Categories on p.CategoryId equals c.CategoryId
                       select new Product
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           CategoryId = c.CategoryId,
                           CategoryName=c.CategoryName,
                       }).ToList();
            return res;
        }

        public Product GetProductById(int id)
        {
            var res = (from p in db.Products
                       join c in db.Categories on p.CategoryId equals c.CategoryId
                       where p.ProductId == id
                       select new Product
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           CategoryId = p.CategoryId,
                           CategoryName = c.CategoryName,
                       }).FirstOrDefault();
            return res;
        }

        public int UpdateProduct(Product prod)
        {
            int result = 0;
            var b = db.Products?.Where(x => x.ProductId == prod.ProductId).FirstOrDefault();
            if (b != null)
            {
                db.Entry<Product>(b).CurrentValues.SetValues(prod);
                result = db.SaveChanges();
            }
            return result;
        }
    }
}

