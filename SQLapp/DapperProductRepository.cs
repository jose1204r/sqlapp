using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace   Bestbuy
{
    internal class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;


        public DapperProductRepository(IDbConnection connection) 
        
        {
           _connection= connection;
        
        }
        public void CreateProduct(string name, double price, int CategoryID)
        {
            _connection.Execute("INSERT INTO products(Name,Price,CategoryID) VALUES (@productName,@price,@categoryID);",
                new {productname =name, price = price,categoryID= CategoryID});
            
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }


        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        //Delete data
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }







    }
}
