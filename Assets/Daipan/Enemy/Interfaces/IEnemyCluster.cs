#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using JetBrains.Annotations;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyCluster
    {
        public IEnumerable<IEnemyMono?> Enemies { get; }
        public void Add(IEnemyMono enemy);
        public void Remove(IEnemyMono enemy);
        public IEnemyMono? NearestEnemy(Vector3 position);
        public void UpdateHighlight(Vector3 position);
        public void Daipaned();
    }
}