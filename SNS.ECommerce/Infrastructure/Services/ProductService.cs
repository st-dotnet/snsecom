using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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

        /// <summary>
        /// Upload XLSX File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //public bool UploadCSVFile(IFormFile file)
        //{
        //    try
        //    {
        //        List<ProductModel> productsList = new();
        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //        using (var stream = new MemoryStream())
        //        {
        //            file.CopyTo(stream);
        //            stream.Position = 0;
        //            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        //            using ExcelPackage package = new(stream);
        //            ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
        //            int totalRows = workSheet.Dimension.Rows;
        //            for (int i = 2; i <= totalRows; i++)
        //            {
        //                productsList.Add(new ProductModel
        //                {
        //                    SKU = workSheet.Cells[i, 1].Value.ToString(),
        //                    Price = workSheet.Cells[i, 2].Value.ToString(),
        //                    Quantity = Convert.ToInt32(workSheet.Cells[i, 3].Value),
        //                    ShowAvailability = Convert.ToBoolean(workSheet.Cells[i, 4].Value),
        //                    Description = workSheet.Cells[i, 5].Value.ToString()
        //                });
        //            }
        //            _dbContext.Products.AddRange(productsList);
        //            _dbContext.SaveChanges();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public bool UploadCSVFile(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                string path = Path.Combine(this._Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string csvData = System.IO.File.ReadAllText(filePath);
                DataTable dt = new DataTable();
                bool firstRow = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (firstRow)
                            {
                                foreach (string cell in row.Split(','))
                                {
                                    dt.Columns.Add(cell.Trim());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (string cell in row.Split(','))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                    i++;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            return false;
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
                if(product != null)
                {
                    _dbContext.Remove(product);
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

        //public bool ImportCsvFile()
        //{
        //    ProductModel product = new ProductModel();

        //    List<object> customers = (from customer in product..ToList().Take(10)
        //                              select new[] { customer.CustomerID.ToString(),
        //                                                    customer.ContactName,
        //                                                    customer.City,
        //                                                    customer.Country
        //                        }).ToList<object>();
        //}


        #endregion

        #region Private methods

        #endregion
    }
}
