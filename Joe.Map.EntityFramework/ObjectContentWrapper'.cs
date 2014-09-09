﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity.Infrastructure;
using Joe.MapBack;

namespace Joe.Map.EntityFramework
{
    public class ObjectSetWrapper<TEntity> : IPersistenceSet<TEntity>
        where TEntity : class
    {
        private ObjectSet<TEntity> ObjectSet { get; set; }

        public ObjectSetWrapper(ObjectSet<TEntity> objectSet)
        {
            ObjectSet = objectSet;
        }
        public TEntity Add(TEntity entity)
        {
            ObjectSet.AddObject(entity);
            return entity;
        }

        public TEntity Create()
        {
            return ObjectSet.CreateObject();
        }

        public TEntity Attach(TEntity entity)
        {
            ObjectSet.Attach(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return ObjectSet.CreateObject<TDerivedEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            var keys = ObjectSet.EntitySet.ElementType.KeyMembers;

            var parameterExpression = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

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

            var lambda = (Expression<Func<TEntity, Boolean>>)Expression.Lambda(compare, new ParameterExpression[] { parameterExpression });
            return ObjectSet.SingleOrDefault(lambda);
        }

        public TEntity Remove(TEntity entity)
        {
            ObjectSet.DeleteObject(entity);
            return entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)ObjectSet).GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return ((IQueryable)ObjectSet).ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return ((IQueryable)ObjectSet).Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ((IQueryable)ObjectSet).Provider;
            }
        }

        public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> Local
        {
            get
            {
                return new ObservableCollection<TEntity>(ObjectSet);
            }
        }
    }
}
