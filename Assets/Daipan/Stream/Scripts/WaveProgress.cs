#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public sealed class WaveProgress : IStart, IUpdate
    {
        readonly WaveState _waveState;
        readonly EnemyWaveSpawnerCounter _enemyWaveSpawnerCounter;
        public double CurrentTime { get; private set; }
        public double CurrentProgressRatio => CurrentTime / MaxTime;
        double MaxTime { get; }

        public WaveProgress(StreamData data)
        {
            MaxTime = data.GetMaxTime();
        }

        void IStart.Start()
        {
            Start();
        }

        void IUpdate.Update()
        {
            CurrentTime += Time.deltaTime;
        }

        public void Start()
        {
            CurrentTime = 0f;
        }

        public void SetTime(double time)
        {
            CurrentTime = time;
        }
    }
}