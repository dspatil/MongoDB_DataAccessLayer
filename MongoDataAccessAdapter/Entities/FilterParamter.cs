using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDataAccessAdapter
{
    [Serializable]
    public class FilterParameter
    {
        public SearchOperator SearchOperator { get; set; }

        public SearchValue SearchValue { get; set; }
    }

    [Serializable]
    public class GenericFilterParameter : FilterParameter
    {
        public string SearchOn { get; set; }
    }

    public enum SearchOperator
    {
        EqualTo,
        LessThan,
        GreaterThan,
        Like,
        NotEqualTo
    }
}
