using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace MongoDataAccessAdapter
{
    public static class MongoQueryBuilder
    {

        private static string GetSearchString(FilterParameter filter)
        {
            var searchText = string.Empty;
            if (filter is GenericFilterParameter)
                searchText = (filter as GenericFilterParameter).SearchOn;
            return searchText;
        }


        internal static QueryDocument BuildQuery(List<long> inputIds, List<FilterParameter> filters)
        {
            var queryDocument = new QueryDocument();

            var ids = new BsonArray();

            if (inputIds != null && inputIds.Count > 0)
            {
                ids.AddRange(inputIds);
                queryDocument.AddRange(new BsonDocument(Query.In("_id", ids).ToBsonDocument()));
            }

            if (filters != null && filters.Count > 0)
            {
                foreach (var filter in filters)
                {
                    dynamic value = null;
                    if (filter.SearchValue is TextValue)
                        value = (filter.SearchValue as TextValue).Value;
                    else if (filter.SearchValue is NumericValue)
                        value = (filter.SearchValue as NumericValue).Value;
                    else if (filter.SearchValue is DateValue)
                        value = (filter.SearchValue as DateValue).Value;
                    else
                        value = string.Empty;
                    var searchString = GetSearchString(filter);
                    var queryDoc = new QueryDocument(new BsonDocument(searchString, value));
                    queryDocument.AddRange(queryDoc);
                }
            }
            return queryDocument;
        }
    }
}

