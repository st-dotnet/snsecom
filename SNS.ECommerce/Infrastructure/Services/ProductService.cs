using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace SNS.ECommerce.Infrastructure.Services
{
    public class ProductService : IProductServices
    {
        private readonly ApplicationContext _dbContext;
        public ProductService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
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

        /// <summary>
        /// Upload XLSX File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool UploadCSVFile(IFormFile file)
        {
            try
            {
                List<ProductModel> productsList = new();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using ExcelPackage package = new(stream);
                    ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        productsList.Add(new ProductModel
                        {
                            SKU = workSheet.Cells[i, 1].Value.ToString(),
                            Price = workSheet.Cells[i, 2].Value.ToString(),
                            Quantity = Convert.ToInt32(workSheet.Cells[i, 3].Value),
                            ShowAvailability = Convert.ToBoolean(workSheet.Cells[i, 4].Value),
                            Description = workSheet.Cells[i, 5].Value.ToString()
                        });
                    }
                    _dbContext.Products.AddRange(productsList);
                    _dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public bool DeleteProducts(int Id)
        {
            try
            {
                var data = _dbContext.Products.Where(x => x.ProductId == Id).FirstOrDefault();
                if (data == null)
                    throw new NullReferenceException();
                _dbContext.Products.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Add Products
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public bool AddProduct(ProductModel productModel)
        {
            try
            {
                _dbContext.Products.Add(productModel);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        #region Private methods

        #endregion
    }
}
