using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ifa.Configuration;
using Ifa.Model;

namespace Ifa.Helpers
{
    public static class QueryableExtensions
    {
        public static PagedResultViewModel<TModel> Page<TModel>(this IQueryable<TModel> queryable, int pageNumber = 1)
        {
            var configuration = ConfigurationHelper.Get();
            return new PagedResultViewModel<TModel>(configuration.ItemsPerPage,
                                                    pageNumber,
                                                    queryable.Count(),
                                                    queryable
                                                        .Skip((pageNumber - 1)*configuration.ItemsPerPage)
                                                        .Take(configuration.ItemsPerPage))
                       {
                           Left = configuration.Left,
                           Right = configuration.Right,
                           Window = configuration.Window
                       };
        }
    }
}
