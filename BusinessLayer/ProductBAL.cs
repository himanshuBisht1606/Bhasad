using BusinessLayer.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProduct = BusinessLayer.Models.TestProduct;

namespace BusinessLayer
{
    public class TestProductBAL
    {
        public int SaveProduct(TestProduct product)
        {
            DataAccessLayer.Product newProduct = null;
            using (var context = new BhasadEntities())
            {
                var oldProduct = (from prod in context.Products
                                  where prod.ProductId == product.ProductId
                                  select new TestProduct
                                  {
                                      ProductName = prod.ProductName,
                                      ProductId = prod.ProductId
                                  }).FirstOrDefault();

                var ProductBySP = context.GetProductProductTypeData(product.ProductId).FirstOrDefault();
                    //.Select(m=>new Product
                    //{
                    //    ProductName=m.ProductName,
                    //    ProductId=m.ProductId,
                    //    ProductTypeId=m.ProductTypeId
                    //}).FirstOrDefault();
                
                //Group By Example for grouping on one column

                //var oldProductQuery = (from prod in context.Products
                //                  where prod.ProductId == product.ProductId
                //                  group prod by prod.ProductName into prodGroup
                //                  select new Product
                //                  {
                //                      ProductName = prodGroup.Key,
                //                      ProductId = prodGroup.Select(m => m.ProductId).FirstOrDefault()
                //                  });
                //var query = context.Products.GroupBy(g =>new { g.ProductTypeId, g.ProductName });
                //var data = query.ToList();
                //var oldProduct = oldProductQuery.FirstOrDefault();

                //order by in lambda 

               // var orderByQuery = context.Products.OrderByDescending(o => o.ProductName).ThenBy(o => o.ProductTypeId).ToList();

                //left outer join

                //lambda expression

                //var joinExample = context.Products.Include("ProductType,ProductVariants,ProductVariants.Items").ToList();
                //from query

                //var joinFromExample = from prod in context.Products
                //                      join prodType in context.ProductTypes on prod.ProductTypeId equals prodType.ProductTypeId
                //                      into pt from tempProdType in pt.DefaultIfEmpty()
                //                      select new Product
                //                      {
                //                          ProductName=prod.ProductName,
                //                          ProductTypeId=tempProdType.ProductTypeId
                //                      };

                //var sqlQuery = context.Products.SqlQuery("select * from product where productTypeId=@producTypeId",
                //    new SqlParameter("@producTypeId", product.ProductTypeId)).Select(m => new Product
                //{

                //}).ToList();

                //context.Database.SqlQuery<Product>("select * from product where productTypeId=@producTypeId", new SqlParameter("@producTypeId", product.ProductTypeId))


                //Group by example for grouping on multiple column

                //var oldProduct = (from prod in context.Products
                //                  where prod.ProductId == product.ProductId
                //                  group prod by new { prod.ProductName, prod.ProductTypeId } into prodGroup
                //                  select new Product
                //                  {
                //                      ProductName = prodGroup.Key.ProductName,
                //                      ProductTypeId=prodGroup.Key.ProductTypeId,
                //                      ProductId = prodGroup.Select(m => m.ProductId).FirstOrDefault()
                //                  }).FirstOrDefault();

                //var oldProduct = context.Products.Where(m => m.ProductId == product.ProductId).OrderBy(o=>o.ProductName).FirstOrDefault();
                
                if (oldProduct != null)
                {
                    //context.Products.Remove(oldProduct);
                    oldProduct.ProductName = product.ProductName;
                    oldProduct.ModifiedBy = 1;
                    oldProduct.ModifiedDate = DateTime.Now;
                    context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    newProduct = context.Products.Add(new DataAccessLayer.Product
                    {
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        ProductName = product.ProductName,
                        ProductTypeId = product.ProductTypeId
                    });
                    var info = context.Entry(newProduct);
                    context.Entry(newProduct).State = System.Data.Entity.EntityState.Added;
                }
                
               int result= context.SaveChanges();

                //insert using sp

                var insertedOutput = context.sp_InsertProduct(product.ProductTypeId, product.ProductName);
                return result;

            }
        }

        public object GetEntryInfo(TestProduct product)
        {
            using (var context=new BhasadEntities())
            {
                var productData = context.Products.Find(2);
                var entryInfo = context.Entry(productData);
                var entityName = entryInfo.Entity.GetType().FullName;
                var state = entryInfo.State;
                var properties = entryInfo.CurrentValues.PropertyNames;
                foreach (var property in properties)
                {
                    var propertyValue = entryInfo.OriginalValues[property];
                }
                return entryInfo;

            }
        }
    }
}
