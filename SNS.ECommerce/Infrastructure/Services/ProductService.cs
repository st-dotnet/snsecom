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
        public bool UploadXLSXFile(IFormFile file)
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

        #endregion

        #region Private methods

        #endregion
    }
}
