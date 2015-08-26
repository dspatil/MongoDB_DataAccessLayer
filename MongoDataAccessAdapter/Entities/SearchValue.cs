using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccessAdapter
{
    [Serializable]
    public abstract class SearchValue
    {

    }
    [Serializable]
    public class TextValue : SearchValue
    {
        public string Value { get; set; }
    }
    [Serializable]
    public class NumericValue : SearchValue
    {
        public double Value { get; set; }
    }
    [Serializable]
    public class DateValue : SearchValue
    {
        public DateTime Value { get; set; }
    }
    [Serializable]
    public class PatternValue : SearchValue
    {
        public string Pattern { get; set; }

        public bool ExcludePattern { get; set; }
    }

}
