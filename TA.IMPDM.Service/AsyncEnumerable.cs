using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    class AsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IAsyncEnumerator<T> enumerator;

        public AsyncEnumerable(IAsyncEnumerator<T> enumerator)
        {
            this.enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }
        public IAsyncEnumerator<T> GetEnumerator()
        {
            return enumerator;
        }
    }
}
