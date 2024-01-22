using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Infrastrcture.DBContext;
using Microsoft.EntityFrameworkCore;
using Catalog.Core.Entities;
using Catalog.Core.RepositoryContracts;
using Catalog.Core.Specs;
using System.Drawing.Printing;

namespace Catalog.Infrastrcture.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        DaffECommerceDbContext _dbContext;
        public ProductRepository(DaffECommerceDbContext dbContext)
        { _dbContext = dbContext; }
        public async Task<List<Product>> getAllItem(ProductSpecs productSpecs)
        {
            List<Product> ProductList = new List<Product>();

            ProductList = await _dbContext.Product
                .Where(x => (productSpecs.searchColumn == null) || ((productSpecs.searchColumn == "Name") && x.Name.Contains(productSpecs.searchValue)))
                .Include(x => x.ProductSubCategory)
                .Include(x => x.ProductSubCategory.ProductCategory)
                .Skip((productSpecs.pageIndex - 1) * productSpecs.pageSize)
                .Take(productSpecs.pageSize)
            .ToListAsync();

            //switch (productSpecs.searchColumn)
            //{
            //    case "Name":
            //        ProductList = await _dbContext.Product.Where(x => x.Name.Contains(productSpecs.searchValue)).Include(x => x.ProductSubCategory).Include(x => x.ProductSubCategory.ProductCategory).ToListAsync();
            //        break;
            //    default:
            //        ProductList = await _dbContext.Product.Include(x => x.ProductSubCategory).Include(x => x.ProductSubCategory.ProductCategory).ToListAsync();
            //        break;

            //}


            switch (productSpecs.orderByColumn)
            {
                case "Name":
                    if (productSpecs.sortOrder.ToLower() == "desc")
                    {
                        return ProductList.OrderByDescending(x => x.Name).ToList();
                    }
                    else
                    {
                        return ProductList = ProductList.OrderBy(x => x.Name).ToList();
                    }

                    break;
                default:
                    return ProductList = ProductList.OrderBy(x => x.ProductSubCategory.ProductCategory.Name).ToList();
                    break;

            }

            return ProductList;
        }
        //T getSingleItem(string a, string b);

        public async Task<int> getTotalCount(ProductSpecs productSpecs)
        {

             var count  = await _dbContext.Product
              .Where(x => (productSpecs.searchColumn == null) || ((productSpecs.searchColumn == "Name") && x.Name.Contains(productSpecs.searchValue)))
              //.Include(x => x.ProductSubCategory)
              //.Include(x => x.ProductSubCategory.ProductCategory)
              ////.Skip((productSpecs.pageIndex - 1) * productSpecs.pageSize)
              ////.Take(productSpecs.pageSize)
              .CountAsync();

            return count;
        }
        public List<Product> getAllItemProd()
        {
            List<Product> ProductList = new List<Product>();
            //ProductList = await _dbContext.Product.Include(x => x.ProductSubCategory).Include(x => x.ProductSubCategory.ProductCategory).ToListAsync();
            //ProductList = await _dbContext.Product.ToListAsync();
            ProductList = _dbContext.Product.ToList();
            //Product rod = new Product();
            //rod.Id = 1;
            //rod.Name = "helo";
            //ProductList.Add(rod);
            return ProductList;
        }

        public Product getSingleItemProd(long id)
        {
            Product Product = new Product();
            Product = _dbContext.Product
                //.Include(x => x.ProductSubCategory).Include(x => x.ProductSubCategory.ProductCategory).
                .First(x => x.Id == id);
            // FindAsync<Product>(x=> x. );
            return Product;
        }


        public async Task<Product> getSingleItem(long id)
        {
            Product Product = new Product();
            Product = await _dbContext.Product.Include(x => x.ProductSubCategory).Include(x => x.ProductSubCategory.ProductCategory).FirstAsync(x => x.Id == id);
            // FindAsync<Product>(x=> x. );
            return Product;
        }

        public async Task<long> insertSingleItem(Product Product)
        {
            await _dbContext.AddAsync(Product);
            long id = 0;
            id = await _dbContext.SaveChangesAsync();
            return id;
        }
        public async Task<Product> updateSingleItem(Product Product)
        {
            Product updatedProduct = null;
            try
            {


                updatedProduct = await _dbContext.Product.FirstAsync(x => x.Id == Product.Id);

                if (updatedProduct != null)
                {
                    updatedProduct.Name = Product.Name;
                    updatedProduct.Price = Product.Price;
                    updatedProduct.ProductPrice = Product.ProductPrice;
                    updatedProduct.NetQuantity = Product.NetQuantity;
                    updatedProduct.Quantity = Product.Quantity;
                    updatedProduct.Color = Product.Color;
                    updatedProduct.Manufacturer = Product.Manufacturer;
                    updatedProduct.CountryOfOrigin = Product.CountryOfOrigin;
                    updatedProduct.DimensionLWH = Product.DimensionLWH;
                    updatedProduct.Image = Product.Image;
                    updatedProduct.ProductDesc = Product.ProductDesc;
                    updatedProduct.CreateDateTime = Product.CreateDateTime;
                    updatedProduct.UpdateDateTime = DateTime.Now;
                    updatedProduct.ModelNumber = Product.ModelNumber;
                    updatedProduct.ProductDesc = Product.ProductDesc;
                    await _dbContext.SaveChangesAsync();
                    updatedProduct = await _dbContext.Product.FirstAsync(x => x.Id == Product.Id);
                    return updatedProduct;
                }
                else
                {
                    return updatedProduct;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return updatedProduct;
            }
        }

        public Product updateSingleItemProduct(Product Product)
        {
            Product updatedProduct = null;
            try
            {


                updatedProduct = _dbContext.Product.First(x => x.Id == Product.Id);

                if (updatedProduct != null)
                {
                    updatedProduct.Name = Product.Name;
                    updatedProduct.Price = Product.Price;
                    updatedProduct.ProductPrice = Product.ProductPrice;
                    updatedProduct.NetQuantity = Product.NetQuantity;
                    updatedProduct.Quantity = Product.Quantity;
                    updatedProduct.Color = Product.Color;
                    updatedProduct.Manufacturer = Product.Manufacturer;
                    updatedProduct.CountryOfOrigin = Product.CountryOfOrigin;
                    updatedProduct.DimensionLWH = Product.DimensionLWH;
                    updatedProduct.Image = Product.Image;
                    updatedProduct.ProductDesc = Product.ProductDesc;
                    updatedProduct.CreateDateTime = Product.CreateDateTime;
                    updatedProduct.UpdateDateTime = DateTime.Now;
                    updatedProduct.ModelNumber = Product.ModelNumber;
                    updatedProduct.ProductDesc = Product.ProductDesc;
                    _dbContext.SaveChanges();
                    updatedProduct = _dbContext.Product.First(x => x.Id == Product.Id);
                    return updatedProduct;
                }
                else
                {
                    return updatedProduct;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return updatedProduct;
            }
        }


        public Product insertSingleItemProduct(Product updatedProduct)
        {

            var Product = _dbContext.Product
                .Include(x => x.ProductSubCategory)
                .Include(x => x.ProductSubCategory.ProductCategory)
                .First(x => x.Id == 1);

            if (Product != null)
            {
                //updatedProduct.Name = Product.Name;
                updatedProduct.Price = Product.Price;
                updatedProduct.ProductPrice = Product.ProductPrice;
                updatedProduct.NetQuantity = Product.NetQuantity;
                updatedProduct.Quantity = Product.Quantity;
                updatedProduct.Color = Product.Color;
                updatedProduct.Manufacturer = Product.Manufacturer;
                updatedProduct.CountryOfOrigin = Product.CountryOfOrigin;
                updatedProduct.DimensionLWH = Product.DimensionLWH;
                updatedProduct.Image = Product.Image;
                updatedProduct.ProductDesc = Product.ProductDesc;
                updatedProduct.CreateDateTime = Product.CreateDateTime;
                updatedProduct.UpdateDateTime = DateTime.Now;
                updatedProduct.ModelNumber = Product.ModelNumber;
                updatedProduct.ProductDesc = Product.ProductDesc;
                updatedProduct.ProductTitle = Product.ProductTitle;
                updatedProduct.ProductSubCategory = Product.ProductSubCategory;
                updatedProduct.Tag = Product.Tag;

                //_dbContext.SaveChanges();
                //updatedProduct = _dbContext.Product.First(x => x.Id == Product.Id);
                //return updatedProduct;
            }




            _dbContext.Add(updatedProduct);
            long id = 0;
            id = _dbContext.SaveChanges();
            var product = getSingleItemProd(id);
            return product;
        }

        public async Task<List<Product>> getAllProductBySeller(long id)
        {
            return await _dbContext.Product
                .Include(x => x.ProductSubCategory)
                .Include(x => x.ProductSubCategory.ProductCategory)
                .Where(x => x.SellerId == id)
                .ToListAsync();

        }
        public async void deleteSingleItem(long id)
        {
            Product Product = null;
            try
            {

                Product = await _dbContext.Product.FirstAsync(x => x.Id == Product.Id);
                if (Product != null)
                {

                    _dbContext.Product.Remove(Product);
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
