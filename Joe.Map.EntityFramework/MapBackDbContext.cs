using Joe.MapBack;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joe.Map.EntityFramework
{
    public abstract class MapBackDbContext : DbContext, IDBViewContext
    {

        protected MapBackDbContext() { }

        protected MapBackDbContext(DbCompiledModel model) : base(model) { }

        public MapBackDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public MapBackDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }

        public MapBackDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext) { }

        public MapBackDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }

        public MapBackDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        public IPersistenceSet<TModel> GetIPersistenceSet<TModel>() where TModel : class
        {
            return new DbPersistenceSet<TModel>(Set<TModel>());
        }

        public IPersistenceSet GetIPersistenceSet(Type TModel)
        {
            return new DbPersistenceSet(Set(TModel));
        }

        public void Detach(Object obj)
        {
            var entry = this.Entry(obj);

            if (entry != null)
                entry.State = EntityState.Detached;
        }
    }
}
