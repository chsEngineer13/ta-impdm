using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TA.IMPDM.Service.Tests
{
    class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> inner;

        public T Current => inner.Current;

        public AsyncEnumerator(IEnumerator<T> inner)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public void Dispose()
        {
            inner.Dispose();
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(inner.MoveNext());
        }
    }
}
