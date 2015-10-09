using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Pagination<T>
    {
        public int Page { get; set; }

        public IEnumerable<T> items; 

        public int Count
        {
            get { return (items != null) ? items.Count() : 0; }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
