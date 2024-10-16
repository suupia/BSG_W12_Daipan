#nullable enable
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyMono 
    {
        public GameObject GameObject { get; }
        public Transform Transform { get; }
        public EnemyEnum EnemyEnum { get; protected set; }
        public Hp Hp { get; protected set; }
        public bool IsReachedPlayer { get; protected set; }
        public void OnAttacked(IPlayerParamData playerParamData);
        public void OnDaipaned();
        public void Highlight(bool isHighlighted);
    }
    
}