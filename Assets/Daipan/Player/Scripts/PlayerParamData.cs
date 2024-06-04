#nullable enable
using System;

namespace Daipan.Player.Scripts 
{
    /// <summary>
    /// このクラスは、IPlayerHp, IPlayerAttackなどを実装するクラスとして捉えている
    /// </summary>
    public class PlayerParamData
    {
        // Hp
        public  Func<int> GetCurrentHp { get; init; } = () => 100;
        public  Action<int> SetCurrentHp { get; init; } = _ => { };
        
        // Attack
        public  Func<int> GetWAttack { get; init; } = () => 10;
        public  Func<int> GetAAttack { get; init; } = () => 10;
        public  Func<int> GetSAttack { get; init; } = () => 10;
    }
}