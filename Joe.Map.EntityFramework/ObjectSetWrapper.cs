using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joe.MapBack;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;

namespace Joe.Map.EntityFramework
{
    public class ObjectSetWrapper : IPersistenceSet
    {
        public IQueryable Set { get; private set; }
        public ObjectContext Context { get; private set; }

        public ObjectSetWrapper(IQueryable set, ObjectContext context)
        {
            Set = set;
            Context = context;
        }

        public object Add(object entity)
        {
            if (entity != null)
            {
                if (entity.GetType() == Set.ElementType)
                {
                    var entitySet = Context.GetEntitySet(entity.GetType());
                    Context.AddObject(entitySet.Name, entity);

                    return entity;
                }
                else
                    throw new ArgumentException("Entity must be of the Querable Type");
            }
            else
                throw new ArgumentNullException("Entity cannot be null");
        }

        public object Attach(object entity)
        {
            if (entity != null)
            {
                if (entity.GetType() == Set.ElementType)
                {
                    var entitySet = Context.GetEntitySet(entity.GetType());
                    Context.AttachTo(entitySet.Name, entity);

                    return entity;
                }
                else
                    throw new ArgumentException("Entity must be of the Querable Type");
            }
            else
                throw new ArgumentNullException("Entity cannot be null");
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class
        {
            return Context.CreateObject<TDerivedEntity>();
        }

        public object Create()
        {
            return typeof(ObjectContext).GetMethod("CreateObject").MakeGenericMethod(Set.ElementType).Invoke(Context, null);
        }

        public object Find(params object[] keyValues)
        {
            var eneitySet = Context.GetEntitySet(Set.ElementType);
            var keys = eneitySet.ElementType.KeyMembers;

            var parameterExpression = Expression.Parameter(Set.ElementType, Set.ElementType.Name);

            Expression compare = null;
            var count = 0;
            foreach (var key in keys)
            {

                Expression compareSingle = Expression.Property(parameterExpression, key.Name);
                compareSingle = Expression.Equal(compareSingle, Expression.Constant(keyValues[count]));
                if (count > 0)
                    compare = Expression.And(compare, compareSingle);
                else
                    compare = compareSingle;
                count++;
            }

            var lambda = Expression.Lambda(compare, new ParameterExpression[] { parameterExpression });
            var castSet = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(Set.ElementType).Invoke(null, new object[] { Set });

            return typeof(Enumerable).GetMethod("SingleOrDefault").MakeGenericMethod(Set.ElementType).Invoke(null, new object[] { castSet, lambda });
        }

        public object Remove(object entity)
        {
            Context.DeleteObject(entity);
            return entity;
        }

        public Type ElementType
        {
            get { return Set.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return Set.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Set.Provider; }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return Set.GetEnumerator();
        }
    }
}
