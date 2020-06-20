using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EfDbContext Context = new EfDbContext();

        public IEnumerable<Product> Products
        {
            get{ return Context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductID == 0 )
            {
                Context.Products.Add(product);
            }
            else
            {
                Product dbEntry = Context.Products.Find(product.ProductID);
                if(dbEntry!=null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            Context.SaveChanges();
        }


        public Product DeleteProduct(int ProductID)
        {
            Product dbEntry = Context.Products.Find(ProductID);
            if(dbEntry != null)
            {
                Context.Products.Remove(dbEntry);
                Context.SaveChanges();
            }
            return dbEntry;

        }

    }
}
