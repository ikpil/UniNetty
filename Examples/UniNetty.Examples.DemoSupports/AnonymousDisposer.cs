using System;

namespace UniNetty.Examples.DemoSupports
{
    internal class AnonymousDisposer : IDisposable
    {
        private Action _action;
        private bool _disposed;
        private readonly object _lock = new object();

        public static IDisposable Create(Action action)
        {
            return new AnonymousDisposer(action);
        }

        private AnonymousDisposer(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        ~AnonymousDisposer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            bool needToDispose = false;
            lock (_lock)
            {
                if (!_disposed)
                {
                    needToDispose = true;
                    _disposed = true;
                }
            }

            if (needToDispose)
            {
                var temp = _action;
                _action = null;
                temp.Invoke();
            }
        }
    }
}