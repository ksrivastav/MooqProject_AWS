using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Responces
{
    public class ProductResponce
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string ModelNumber { get; set; }
        public string ProductDesc { get; set; }

        public string ProductTitle { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Manufacturer { get; set; }
        public double ProductPrice { get; set; }


        public long SellerId { get; set; }

        //[ForeignKey("SellerId")]
        //public User Seller { get; set; }

        public double NetWieght { get; set; }

        public long Quantity { get; set; }

        public int NetQuantity { get; set; }

        public string DimensionLWH { get; set; }
        public string Tag { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }


        public string? Image { get; set; }

        public long ProductSubCategoryId { get; set; }

        [ForeignKey("ProductSubCategoryId")]
        public virtual ProductSubCategoryResponce ProductSubCategory { get; set; }


    }
}
