using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Joe.Initialize;

namespace Joe.Map.EntityFramework
{
    [Init]
    public class Initialize
    {
        private static MethodInfo _stringConvertMethod;
        private static MethodInfo _includeMethod;

        public static void Init()
        {
            Joe.Map.ExpressionHelpers.GetIncludeMethod = (Type entityType) =>
            {
                var iQueryableType = typeof(IQueryable<>).MakeGenericType(entityType);
                _includeMethod = _includeMethod ?? typeof(System.Data.Entity.QueryableExtensions)
                    .GetMethod("Include", new Type[] { iQueryableType, typeof(String) });
                return _includeMethod;
            };

            Joe.Map.ExpressionHelpers.StringConvertMethod = (Type entityType) =>
            {
                _stringConvertMethod = _stringConvertMethod ?? typeof(System.Data.Entity.SqlServer.SqlFunctions).GetMethod("StringConvert", new[] { typeof(Decimal?) });
                return _stringConvertMethod;
            };
        }
    }
}
