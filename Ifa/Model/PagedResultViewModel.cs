using System;
using System.Collections.Generic;

namespace Ifa.Model
{
    public abstract class PagedResultBase
    {
        public virtual int Total { get; protected set; }
        public virtual int ItemsPerPage { get; protected set; }
        public virtual int PageNumber { get; protected set; }
        public virtual int Window { get; protected set; }

        public virtual int Pages
        {
            get { return (int)Math.Ceiling((decimal)Total / ItemsPerPage); }
        }

        protected PagedResultBase()
        { }

        protected PagedResultBase(int itemsPerPage, int pageNumber, int total)
        {
            ItemsPerPage = itemsPerPage;
            PageNumber = pageNumber;
            Total = total;
            Window = 10;
        }
    }

    public class PagedResultViewModel<TResult> : PagedResultBase
    {
        public IEnumerable<TResult> Result { get; private set; }

        public PagedResultViewModel(int itemsPerPage,
            int pageNumber, 
            int total, 
            IEnumerable<TResult> result)
            : base(itemsPerPage, pageNumber, total)
        {
            Result = result;
        }
    }
}
