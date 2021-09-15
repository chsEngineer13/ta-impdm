using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TA.IMPDM.Service.Tests
{
    class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);

        public AsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {

        }
        public AsyncEnumerable(Expression expression)
            : base(expression)
        {

        }
        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}
