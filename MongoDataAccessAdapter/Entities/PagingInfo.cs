using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDataAccessAdapter
{
     [Serializable]
    public class PagingInfo
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public bool PagingDisabled { get; set; }

    }
}
