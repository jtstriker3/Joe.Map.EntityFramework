using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joe.MapBack;
using System.Data.Entity;

namespace Joe.Map.EntityFramework
{
    public class DbPersistenceSet : IPersistenceSet
    {
        public DbSet Set { get; private set; }
        public IQueryable Queryable
        {
            get
            {
                return (IQueryable)Set;
            }
        }

        public DbPersistenceSet(DbSet set)
        {
            Set = set;
        }

        public object Add(object entity)
        {
            return Set.Add(entity);
        }

        public object Attach(object entity)
        {
            return Set.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class
        {
            return (TDerivedEntity)Set.Create(typeof(TDerivedEntity));
        }

        public object Create()
        {
            return Set.Create();
        }

        public object Find(params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        public object Remove(object entity)
        {
            return Set.Remove(entity);
        }

        public Type ElementType
        {
            get { return Set.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return Queryable.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Queryable.Provider; }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }
    }
}
