using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SNS.ECommerce.Infrastructure.Services
{
    public class ProductService : IProductServices
    {
        private readonly ApplicationContext _dbContext;
        private IHostingEnvironment _Environment;
        public ProductService(ApplicationContext dbContext, IHostingEnvironment Environment)
        {
            _dbContext = dbContext;
            _Environment = Environment;
        }
        #region Public Methods

        /// <summary>
        /// Get products list
        /// </summary>
        /// <returns></returns>
        public List<ProductModel> GetProductList()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UploadCSVFile(IFormFile file)
        {
            List<ProductModel> data = new();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var fileStream = file.OpenReadStream())
                using (var reader = new StreamReader(fileStream))
                {
                    reader.ReadLine();
                    string row;
                    while ((row = reader.ReadLine()) != null)
                    {
                        if (row != "")
                        {
                            string[] rowData = row.Split(',');
                            data.Add(new ProductModel
                            {
                                SKU = rowData[0].ToString(),
                                Price = rowData[1].ToString(),
                                Quantity = Convert.ToInt32(rowData[2]),
                                Description = rowData[4].ToString()
                            });
                        }
                        
                    }
                }
                _dbContext.Products.AddRange(data);
                _dbContext.SaveChanges();
            }
            return true;
        }


        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel GetProductById(int id)
        {
            try
            {
                return _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateProductById(ProductModel model)
        {
            try
            {
                _dbContext.Products.Update(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteProductById(int id)
        {
            try
            {
                var product = _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }

        }

        #endregion

       
    }
}