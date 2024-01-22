using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.RepositoryContracts
{
    public interface IProductSubCategoryRepository<T>
    {
        public Task<List<T>> getAllItem();
        //T getSingleItem(string a, string b);

        public Task<T> getSingleItem(long id);

        public Task<long> insertSingleItem(T t);
        public Task<T> updateSingleItem(T t);
        public void deleteSingleItem(long id);
    }
}
