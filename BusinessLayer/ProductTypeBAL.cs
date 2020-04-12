using BusinessLayer.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class ProductTypeBAL 
    {
        public List<ProductTypeModel> GetProductType()
        {
            List<ProductTypeModel> models = new List<ProductTypeModel>();
            using (var context=new BhasadEntities())
            {
                models = context.ProductTypes.Where(m=>m.IsActive==true).Select(m => new ProductTypeModel
                {
                    ProductTypeId = m.ProductTypeId,
                    ProductType = m.ProductTypeName,
                    IsActive=m.IsActive
                }).ToList();
            }
            return models;
        }

        public int AddProductType(ProductTypeModel model)
        {
            int result = 0;
            try
            {
                using (var context = new BhasadEntities())
                {
                    context.ProductTypes.Add(new ProductType
                    {
                        ProductTypeName = model.ProductType,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        IsActive = true,

                    });
                    result = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
                return result;
           
           
        }

        public ProductTypeModel GetProductTypeByProductTypeId(int productTypeId)
        {
            using (var context=new BhasadEntities())
            {
                var productType = context.ProductTypes.Where(m => m.ProductTypeId == productTypeId)
                    .Select(m => new ProductTypeModel
                    {
                        ProductTypeId=m.ProductTypeId,
                        ProductType=m.ProductTypeName
                    }).FirstOrDefault();
                return productType;
            }
        }

        public int DeleteProductType(int productTypeId)
        {
            int result = 0;
            try
            {
                using (var context = new BhasadEntities())
                {
                    var productType = context.ProductTypes.SingleOrDefault(m => m.ProductTypeId == productTypeId);
                    if (productType != null)
                    {
                        productType.IsActive = false;
                        productType.ModifiedBy = 1;
                        productType.ModifiedDate = DateTime.Now;

                    }
                    result = context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public int UpdateProductType(ProductTypeModel productTypeModel)
        {
            int result = 0;
            try
            {
                using (var context = new BhasadEntities())
                {
                    var productType = context.ProductTypes.SingleOrDefault(m => m.ProductTypeId == productTypeModel.ProductTypeId);
                    if (productType != null)
                    {
                        productType.ProductTypeName = productTypeModel.ProductType;
                        productType.ModifiedBy = 1;
                        productType.ModifiedDate = DateTime.Now;

                    }
                    result = context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
           
        }
    }
}
