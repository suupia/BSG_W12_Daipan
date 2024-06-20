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
        public double CurrentTime { get; private set; }
        
       public double CurrentProgressRatio => CurrentTime / MaxTime;

        bool IsTicking { get; set; }
        
        double MaxTime { get; }

        public StreamTimer(StreamData data)
        {
            MaxTime = data.GetMaxTime();
        }
        

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
        public void SetTime(double time)
        {
            CurrentTime = time;
        }
    }
}