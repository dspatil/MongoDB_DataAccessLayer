using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDataAccessAdapter
{
    [Serializable]
    public class SortingInfo
    {
        public OrderBy OrderBy { get; set; }

        public bool SortDescending { get; set; }
    }

    public enum OrderBy
    {
        CreationDate,
        Id,
        Name,
        Status,
        NotValid
    }
}
