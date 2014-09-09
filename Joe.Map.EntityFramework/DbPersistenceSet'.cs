using Joe.MapBack;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joe.Map.EntityFramework
{

    public class DbPersistenceSet<TEntity> : IPersistenceSet<TEntity>
        where TEntity : class
    {
        public DbSet<TEntity> Set { get; private set; }
        public IQueryable<TEntity> Queryable
        {
            get
            {
                return (IQueryable<TEntity>)Set;
            }
        }

        public DbPersistenceSet(DbSet<TEntity> set)
        {
            Set = set;
        }

        public TEntity Add(TEntity entity)
        {
            return Set.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            return Set.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return Set.Create<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return Set.Create();
        }

        public TEntity Find(params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        public TEntity Remove(TEntity entity)
        {
            return Set.Remove(entity);
        }

        public Type ElementType
        {
            get { return Queryable.ElementType; }
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

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }



        public IList<TEntity> Local
        {
            get
            {
               return this.Set.Local;
            }
        }
    }

}
