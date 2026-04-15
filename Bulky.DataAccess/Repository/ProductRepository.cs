using MAY.DataAccess.Data;
using MAY.DataAccess.Repository.IRepository;
using MAY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAY.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository

    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}

