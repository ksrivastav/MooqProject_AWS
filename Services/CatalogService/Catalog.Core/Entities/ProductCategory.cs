using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Entities
{
    public class ProductCategory:BaseEntity
    {
      
        public string Name { get; set; }
        public string Description { get; set; }

        public string tags { get; set; }

    }
}
