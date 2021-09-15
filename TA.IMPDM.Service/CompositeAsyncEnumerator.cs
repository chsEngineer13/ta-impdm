using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TA.IMPDM.Service
{
    class CompositeAsyncEnumerator<T> : IAsyncEnumerator<T> where T : class
    {
        private T _current = null;
        private readonly IEnumerator<IAsyncEnumerator<T>> _enumerator;

        public T Current => _current;

        public CompositeAsyncEnumerator(IEnumerable<IAsyncEnumerator<T>> enumerators)
        {
            if (enumerators == null)
                throw new ArgumentNullException(nameof(enumerators));

            _enumerator = enumerators.GetEnumerator();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            if (_current == null && _enumerator.MoveNext() == false)
                return false;

            do
            {
                bool canMoveNext = await _enumerator.Current.MoveNext(cancellationToken).ConfigureAwait(false);
                if (canMoveNext)
                {
                    _current = _enumerator.Current.Current;
                    return true;
                }
                else
                {
                    _current = null;
                }
            } while (_enumerator.MoveNext());

            return false;
        }
    }
}
