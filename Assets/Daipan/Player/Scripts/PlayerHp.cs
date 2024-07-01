#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Battle.scripts;
using Daipan.Player.LevelDesign.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Daipan.Player.Scripts
{
    // public class PlayerHp : IPlayerHp 
    // {
    //     public int MaxHp { get; }
    //     public event EventHandler<DamageArgs>? OnDamage;
    //
    //     public PlayerHp(IPlayerHpParamData paramData)
    //     {
    //         MaxHp = paramData.GetCurrentHp();
    //         CurrentHp = MaxHp; 
    //     }
    //
    //     public int CurrentHp { get; private set; }
    //
    //     public void SetHp(DamageArgs damageArgs)
    //     {
    //         CurrentHp -= damageArgs.DamageValue;
    //         OnDamage?.Invoke(this, damageArgs);
    //         if (CurrentHp <= 0)
    //         {
    //             Debug.Log($"Player died");
    //             // todo: コールバックで次のシーンへの遷移を挟みたい
    //             //SceneTransition.TransitioningScene(SceneName.ResultScene);
    //         }
    //     }
    //
    // }

    
}