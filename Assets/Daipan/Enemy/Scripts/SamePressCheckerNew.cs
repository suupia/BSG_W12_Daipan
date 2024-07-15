#nullable enable
using System;
using System.Linq;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class SamePressCheckerNew : IDisposable
    {
        readonly double _allowableSec;
        readonly bool[] _flags;
        readonly Action _onSuccess;
        readonly Action _onFailure;
        readonly IDisposable?[] _disposables;

        public SamePressCheckerNew(
            double allowableSec
            , int buttonCount
            , Action onSuccess
            , Action onFailure
            )
        {
            _allowableSec = allowableSec;
            _flags = new bool[buttonCount];
            _disposables = new IDisposable[buttonCount];
            _onSuccess = onSuccess;
            _onFailure = onFailure;
        }
        
        public void SetOn(int index)
        {
            _flags[index] = true;
            _disposables[index]?.Dispose();
            _disposables[index] = Observable.Timer(TimeSpan.FromSeconds(_allowableSec))
                .Subscribe(_ => { _flags[index] = false; });

            if (IsAllOn(_flags))
            {
                _onSuccess();
                Debug.Log($"All on");
                Dispose();
            }
        }
        
        static bool IsAllOn(bool[] flags)
        {
            return flags.All(flag => flag);
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
        }

        ~SamePressCheckerNew()
        {
            Dispose();
        }
    }
}