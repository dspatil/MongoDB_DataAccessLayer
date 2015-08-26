using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccessAdapter
{
    public interface IMongoCSharpDriver
    {
        void Insert<T>(T t);
        T Find<T>(long id);
        void Update<T>(T t);
        void Delete<T>(long id);
        PagedList<T> FindAll<T>(List<long> inputIds, List<FilterParameter> filters, List<string> includeFields, SortingInfo sortingInfo, PagingInfo pagingInfo);
    }
}
