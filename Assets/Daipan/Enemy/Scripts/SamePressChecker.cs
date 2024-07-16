#nullable enable
using System;
using System.Linq;
using R3;
using TMPro;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class SamePressChecker : IDisposable
    {
        readonly double _allowableSec;
        readonly bool[] _flags;
        readonly Action _onSuccess;
        readonly Action _onFailure;
        readonly IDisposable?[] _buttonDisposables; 
        IDisposable? _initPushDisposable;

        public SamePressChecker(
            double allowableSec
            , int buttonCount
            , Action onSuccess
            , Action onFailure
            )
        {
            _allowableSec = allowableSec;
            _flags = new bool[buttonCount];
            _buttonDisposables = new IDisposable[buttonCount];
            _onSuccess = onSuccess;
            _onFailure = onFailure;
        }
        
        public void SetOn(int index)
        {
            _flags[index] = true;
        
            // Dispose the previous timer for this button if it exists and trigger failure action
            if (_buttonDisposables[index] != null)
            {
                _onFailure();
                _buttonDisposables[index]?.Dispose();
            }

            // Start a new timer for this button to reset its flag after the allowable time
            _buttonDisposables[index] = Observable.Timer(TimeSpan.FromSeconds(_allowableSec))
                .Subscribe(_ => { _flags[index] = false; });

            // If this is the first button press, start a global failure timer
            if (_initPushDisposable == null)
            {
                _initPushDisposable = Observable.Timer(TimeSpan.FromSeconds(_allowableSec))
                    .Subscribe(_ => { _onFailure(); });
            }
        
            // Check if all flags are on, indicating success
            if (IsAllOn())
            {
                _onSuccess();
                _initPushDisposable?.Dispose();
            }
        }
        
        public bool IsAllOn()
        {
            return _flags.All(flag => flag);
        }
        
        public void Dispose()
        {
            Array.Fill(_flags, false);
            _initPushDisposable?.Dispose();
            foreach (var disposable in _buttonDisposables)
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