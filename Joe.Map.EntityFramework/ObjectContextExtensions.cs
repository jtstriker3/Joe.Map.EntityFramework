using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joe.Map.EntityFramework
{
    static class ObjectContextExtensions
    {
        internal static EntitySetBase GetEntitySet(this ObjectContext context, Type entityType)
        {
            EntityContainer container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

            EntitySetBase entitySet = container.BaseEntitySets.Where(item => item.ElementType.Name.Equals(entityType.Name))
                                                              .FirstOrDefault();

            return entitySet;
        }
    }
}
