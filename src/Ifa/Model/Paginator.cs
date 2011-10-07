using System.Linq;
using System.Text;

namespace Ifa.Model
{
    public class Paginator
    {
        public void Construct(PaginationBuilder builder)
        {
            builder.BuildFirstPageLink();
            builder.BuildPreviousPageLink();
            builder.BuildPageLinks();
            builder.BuildNextPageLink();
            builder.BuildLastPageLink();
        }
    }
}
