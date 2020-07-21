using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Common
{
    public class PagedResult<T>:PagedResultBase
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
