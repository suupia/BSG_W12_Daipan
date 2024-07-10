#nullable enable
using System;
using System.Linq;
using R3;

namespace Daipan.Enemy.Scripts
{
    public class SamePressChecker : IDisposable
    {
        readonly double _allowableSec;
        readonly bool[] _flags;
        readonly IDisposable?[] _disposables;

        public SamePressChecker(double allowableSec, int count)
        {
            _allowableSec = allowableSec;
            _flags = new bool[count];
            _disposables = new IDisposable[count];
        }

        public void IsOn(int index)
        {
            _flags[index] = true;
            _disposables[index]?.Dispose();
            _disposables[index] = Observable.Timer(TimeSpan.FromSeconds(_allowableSec))
                .Subscribe(_ => { _flags[index] = false; });
        }

        public bool IsAllOn()
        {
            return _flags.All(flag => flag);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }

        ~SamePressChecker()
        {
            Dispose();
        }
    }
}