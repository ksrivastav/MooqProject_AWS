using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Core.Entities;

namespace Catalog.Application.Responces
{
    public class ProductSubCategoryResponce : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public string Tags { get; set; }

        public long ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategoryResponce ProductCategory { get; set; }


    }
}
