#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyMono : IMonoBehaviour 
    {
        public EnemyEnum EnemyEnum { get; protected set; }
        public Hp Hp { get; protected set; }
        public bool IsReachedPlayer { get; protected set; }
        public event EventHandler<DiedEventArgs>? OnDiedEvent;
        public event EventHandler<IPlayerParamData>? OnAttackedEvent;
        public void OnAttacked(IPlayerParamData playerParamData);
        public void OnDaipaned();
        public void Highlight(bool isHighlighted);
    }
    
}