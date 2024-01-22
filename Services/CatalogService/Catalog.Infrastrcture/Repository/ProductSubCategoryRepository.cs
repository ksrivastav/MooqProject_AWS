using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.Core.Entities;
using Catalog.Core.RepositoryContracts;
using Catalog.Infrastrcture.DBContext;

namespace Catalog.Infrastrcture.Repository
{
    public class ProductSubCategoryRepository : IProductSubCategoryRepository<ProductSubCategory>
    {
        DaffECommerceDbContext _dbContext;
        public ProductSubCategoryRepository(DaffECommerceDbContext dbContext)
        { _dbContext = dbContext; }
        public async Task<List<ProductSubCategory>> getAllItem()
        {
            List<ProductSubCategory> ProductSubCategoryList = new List<ProductSubCategory>();
            ProductSubCategoryList = await _dbContext.ProductSubCategory.ToListAsync();
            //.Products.Include("ProductSubCategory").ToList<Product>();
            return ProductSubCategoryList;
        }
        //T getSingleItem(string a, string b);

        public async Task<ProductSubCategory> getSingleItem(long id)
        {
            ProductSubCategory ProductSubCategory = new ProductSubCategory();
            ProductSubCategory = await _dbContext.ProductSubCategory.FirstAsync(x => x.Id == id);
            // FindAsync<ProductSubCategory>(x=> x. );
            return ProductSubCategory;
        }

        public async Task<long> insertSingleItem(ProductSubCategory ProductSubCategory)
        {
            await _dbContext.AddAsync(ProductSubCategory);
            long id = 0;
            id = await _dbContext.SaveChangesAsync();
            return id;
        }
        public async Task<ProductSubCategory> updateSingleItem(ProductSubCategory ProductSubCategory)
        {
            ProductSubCategory updatedProductSubCategory = null;
            try
            {


                updatedProductSubCategory = await _dbContext.ProductSubCategory.FirstAsync(x => x.Id == ProductSubCategory.Id);

                if (updatedProductSubCategory != null)
                {
                    updatedProductSubCategory.Name = ProductSubCategory.Name;
                    updatedProductSubCategory.Description = ProductSubCategory.Description;
                    updatedProductSubCategory.UpdateDateTime = DateTime.Now;
                    updatedProductSubCategory.Tags = ProductSubCategory.Tags;
                    updatedProductSubCategory.CreateDateTime = ProductSubCategory.CreateDateTime;
                    await _dbContext.SaveChangesAsync();
                    updatedProductSubCategory = await _dbContext.ProductSubCategory.FirstAsync(x => x.Id == ProductSubCategory.Id);
                    return updatedProductSubCategory;
                }
                else
                {
                    return updatedProductSubCategory;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return updatedProductSubCategory;
            }
        }
        public async void deleteSingleItem(long id)
        {
            ProductSubCategory ProductSubCategory = null;
            try
            {

                ProductSubCategory = await _dbContext.ProductSubCategory.FirstAsync(x => x.Id == ProductSubCategory.Id);
                if (ProductSubCategory != null)
                {

                    _dbContext.ProductSubCategory.Remove(ProductSubCategory);
                    _dbContext.SaveChanges();


                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
