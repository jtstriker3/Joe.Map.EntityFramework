using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joe.Map;
using System.Reflection;
using System.Data.Entity.Core.Objects;
using Joe.MapBack;

namespace Joe.Map.EntityFramework
{
    public abstract class MapBackObjectContext : ObjectContext, IDBViewContext
    {
        public MapBackObjectContext(String connectionString)
            : base(connectionString)
        {

        }

        public MapBackObjectContext(String connectionString, String defaultContainerName)
            : base(connectionString, defaultContainerName)
        {

        }
        public MapBackObjectContext(System.Data.Entity.Core.EntityClient.EntityConnection entityConnections)
            : base(entityConnections)
        {

        }

        public MapBackObjectContext(System.Data.Entity.Core.EntityClient.EntityConnection entityConnections, String defaultContainerName)
            : base(entityConnections, defaultContainerName)
        {

        }

        public IPersistenceSet<TEntity> GetIPersistenceSet<TEntity>() where TEntity : class
        {
            return new ObjectSetWrapper<TEntity>(this.GetObjectSet<TEntity>());
        }

        public IPersistenceSet GetIPersistenceSet(Type TModel)
        {
            foreach (PropertyInfo info in this.GetType().GetProperties())
            {
                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(ObjectSet<>))
                    if (info.PropertyType.GetGenericArguments().Single() == TModel)
                        return new ObjectSetWrapper((IQueryable)info.GetValue(this, null), this);
            }
            return null;
        }

        private ObjectSet<TEntity> GetObjectSet<TEntity>() where TEntity : class
        {
            foreach (PropertyInfo info in this.GetType().GetProperties())
            {
                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(ObjectSet<>))
                    if (info.PropertyType.GetGenericArguments().Single() == typeof(TEntity))
                        return (ObjectSet<TEntity>)info.GetValue(this, null);
            }

            return null;
        }

        public ObjectContext ObjectContext
        {
            get { return this; }
        }
    }
}
