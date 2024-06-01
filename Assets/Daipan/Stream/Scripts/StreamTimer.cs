#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public class StreamTimer : IStart, IUpdate
    {
        double CurrentTime { get; set; }

        bool IsTicking { get; set; } 

        void IStart.Start()
        {
            Start();
        }

        void IUpdate.Update()
        {
            if (!IsTicking) return;
            CurrentTime += Time.deltaTime;
        }

        public void Start()
        {
            IsTicking = true;
            CurrentTime = 0f;
        }

        public void Stop()
        {
            IsTicking = false;
        }

        public void Resume()
        {
            IsTicking = true;
        }

        public double GetCurrentTime()
        {
            return CurrentTime;
        }
    }
}