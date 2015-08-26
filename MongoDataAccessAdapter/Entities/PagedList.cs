using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDataAccessAdapter
{
    [Serializable]
    public class PagedList<T> : List<T>
    {
        public long TotalCount { get; set; }
    }
}
