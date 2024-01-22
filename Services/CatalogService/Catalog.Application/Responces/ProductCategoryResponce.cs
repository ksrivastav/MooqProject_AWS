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
    public class ProductCategoryResponce : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public string tags { get; set; }

    }
}
