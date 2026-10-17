using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ISoft.RichReportDesigner.Core.Fields.Argumentes
{
    /// <summary>
    /// Thread-safe implementation of field argument collection using an index-based dictionary
    /// protected by a ReaderWriterLockSlim for high-concurrency O(1) lookups.
    /// </summary>
    public class RichFieldArgumentThreadSafeCollection : IEnumerable<IRichFieldArgument>, IDisposable
    {
        private readonly Dictionary<int, IRichFieldArgument> _arguments;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private int _nextIndex;

        public RichFieldArgumentThreadSafeCollection()
        {
            _arguments = new Dictionary<int, IRichFieldArgument>();
            _nextIndex = 0;
        }

        public RichFieldArgumentThreadSafeCollection(IEnumerable<string> values)
        {
            var valueList = values?.ToList() ?? new List<string>();
            _arguments = new Dictionary<int, IRichFieldArgument>(valueList.Count);

            foreach (var value in valueList)
            {
                Add(value); // Thread-safe constructor invocation
            }
        }

        public RichFieldArgumentThreadSafeCollection(IEnumerable<IRichFieldArgument> arguments)
        {
            var argList = arguments?.ToList() ?? new List<IRichFieldArgument>();
            _arguments = new Dictionary<int, IRichFieldArgument>(argList.Count);

            foreach (var argument in argList)
            {
                Add(argument); // Thread-safe constructor invocation
            }
        }

        #region IDataCollection Implementation

        public IRichFieldArgument this[int index]
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _arguments.TryGetValue(index, out var arg) ? arg : null;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            set
            {
                _lock.EnterWriteLock();
                try
                {
                    if (value == null)
                    {
                        if (_arguments.Remove(index) && index == _nextIndex - 1) _nextIndex--;
                        return;
                    }

                    value.UpdateIndex(index);
                    _arguments[index] = value;
                    _nextIndex = Math.Max(_nextIndex, index + 1);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public IRichFieldArgument FirstOrDefault()
        {
            _lock.EnterReadLock();
            try
            {
                return _arguments.Count > 0 ? _arguments[_arguments.Keys.First()] : null;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IRichFieldArgument LastOrDefault()
        {
            _lock.EnterReadLock();
            try
            {
                return _arguments.Count > 0 ? _arguments[_arguments.Keys.Last()] : null;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try { return _arguments.Count; }
                finally { _lock.ExitReadLock(); }
            }
        }

        public int MaxIndex
        {
            get
            {
                _lock.EnterReadLock();
                try { return _arguments.Count > 0 ? _arguments.Keys.Max() : -1; }
                finally { _lock.ExitReadLock(); }
            }
        }

        public int MinIndex
        {
            get
            {
                _lock.EnterReadLock();
                try { return _arguments.Count > 0 ? _arguments.Keys.Min() : -1; }
                finally { _lock.ExitReadLock(); }
            }
        }

        public void Add(IRichFieldArgument item)
        {
            if (item == null) return;

            _lock.EnterWriteLock();
            try
            {
                if (item.Index >= 0 && !_arguments.ContainsKey(item.Index))
                {
                    _arguments[item.Index] = item;
                    _nextIndex = Math.Max(_nextIndex, item.Index + 1);
                }
                else
                {
                    item.UpdateIndex(_nextIndex);

                    _arguments[_nextIndex] = item;
                    _nextIndex++;
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Add(string value)
        {
            _lock.EnterWriteLock();
            try
            {
                _arguments[_nextIndex] = new RichFieldArgument(_nextIndex, value);
                _nextIndex++;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddAtIndex(int index, string value)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be negative.");

            _lock.EnterWriteLock();
            try
            {
                if (_arguments.ContainsKey(index)) throw new ArgumentException($"An item with index {index} already exists.", nameof(index));

                _arguments[index] = new RichFieldArgument(index, value);
                _nextIndex = Math.Max(_nextIndex, index + 1);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddOrUpdate(int index, string value)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be negative.");

            _lock.EnterWriteLock();
            try
            {
                _arguments[index] = new RichFieldArgument(index, value);
                _nextIndex = Math.Max(_nextIndex, index + 1);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Insert(int index, string value)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be negative.");

            _lock.EnterWriteLock();
            try
            {
                if (!_arguments.ContainsKey(index))
                {
                    _arguments[index] = new RichFieldArgument(index, value);
                    _nextIndex = Math.Max(_nextIndex, index + 1);
                    return;
                }

                var keysToShift = _arguments.Keys.Where(k => k >= index).OrderByDescending(k => k).ToList();

                foreach (var key in keysToShift)
                {
                    var item = _arguments[key];
                    _arguments.Remove(key);

                    item.UpdateIndex(key + 1);
                    _arguments[key + 1] = item;
                }

                _arguments[index] = new RichFieldArgument(index, value);
                _nextIndex = Math.Max(_nextIndex, (_arguments.Keys.Count > 0 ? _arguments.Keys.Max() + 1 : 0));
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _arguments.Clear();
                _nextIndex = 0;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool ContainsAt(int index)
        {
            if (index < 0) return false;

            _lock.EnterReadLock();
            try
            {
                return _arguments.ContainsKey(index);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Contains(IRichFieldArgument item)
        {
            if (item == null) return false;

            _lock.EnterReadLock();
            try
            {
                return _arguments.TryGetValue(item.Index, out var existing) && existing == item;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public bool Remove(IRichFieldArgument item)
            => item != null && RemoveAt(item.Index);

        public bool RemoveAt(int index)
        {
            _lock.EnterWriteLock();
            try
            {
                if (!_arguments.Remove(index))
                    return false;

                if (index == _nextIndex - 1)
                {
                    _nextIndex--;
                }

                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion

        #region IEnumerator Implementation

        /// <summary>
        /// Returns a snapshot copy of the arguments list to guarantee thread safety during iteration.
        /// </summary>
        public IEnumerator<IRichFieldArgument> GetEnumerator()
        {
            List<IRichFieldArgument> snapshot;
            _lock.EnterReadLock();
            try
            {
                snapshot = _arguments.Values.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return snapshot.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        public void CopyTo(IRichFieldArgument[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                _arguments.Values.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IEnumerable<IRichFieldArgument> GetOrderedArguments()
        {
            _lock.EnterReadLock();
            try
            {
                // Materialize the list (ToList) inside the lock to ensure background modifications don't break iteration
                return _arguments.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Dispose()
        {
            _lock.Dispose();
        }
    }


}
