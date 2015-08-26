using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;

namespace MongoDataAccessAdapter
{
    public class MongoCSharpDriver : IMongoCSharpDriver
    {
        public void Insert<T>(T t)
        {
            var collection = MongoConfig.GetCollection<T>();
            collection.Insert(t);
        }

        public T Find<T>(long id)
        {
            var query = Query.EQ("_id", id);
            var entityCollection = MongoConfig.GetCollection<T>();
            var entity = entityCollection.FindOne(query);
            return entity;
        }

        public void Update<T>(T t)
        {
            dynamic x = t;
            var id = x.Id;
            var query = Query.EQ("_id", id);
            var entityCollection = MongoConfig.GetCollection<T>();
            entityCollection.Update(query, new UpdateWrapper(typeof(T), t));
        }

        public void Delete<T>(long id)
        {
            var query = Query.EQ("_id", id);
            var entityCollection = MongoConfig.GetCollection<T>();
            entityCollection.Remove(query);
        }

        public PagedList<T> FindAll<T>(List<long> inputIds, List<FilterParameter> filters, List<string> includeFields, SortingInfo sortingInfo, PagingInfo pagingInfo)
        {
            var query = MongoQueryBuilder.BuildQuery(inputIds, filters);
            return GetMongoEntities<T>(query, includeFields, sortingInfo, pagingInfo);
        }

        private PagedList<T> GetMongoEntities<T>(QueryDocument query, List<string> includeFields, SortingInfo sortingInfo, PagingInfo pagingInfo)
        {
            var collection = MongoConfig.GetCollection<T>();
            MongoCursor<T> mongoCursor = null;
            var requiredFields = new FieldsBuilder();

            int pageSize = 0;
            int pageNumber = 1;
            if (pagingInfo != null)
            {
                pageNumber = pagingInfo.PageNumber;
                pageSize = pagingInfo.PageSize;
            }
            int limit = pageSize;
            int skip = (pageNumber - 1) * pageSize;

            if (includeFields != null && includeFields.Count > 0)
                requiredFields.Include(includeFields.ToArray());

            var orderBy = OrderBy.CreationDate.ToString();
            var sortDescending = false;

            if (sortingInfo != null)
            {
                orderBy = sortingInfo.OrderBy.ToString();
                sortDescending = sortingInfo.SortDescending;
                if (sortingInfo.OrderBy == OrderBy.Id)
                    orderBy = "_id";
            }
            var sortBy = sortDescending ? SortBy.Descending(new[] { orderBy }) : SortBy.Ascending(new[] { orderBy });

            mongoCursor = collection.Find(query).SetSortOrder(sortBy).SetFields(requiredFields).SetSkip(skip).SetLimit(limit);
            var totalCount = mongoCursor.Count();

            var entities = new PagedList<T>();
            entities.TotalCount = totalCount;
            foreach (var entity in mongoCursor)
            {
                entities.Add(entity);
            }
            return entities;
        }
    }
}
