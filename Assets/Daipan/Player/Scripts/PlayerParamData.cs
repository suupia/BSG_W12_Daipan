#nullable enable
using System;

namespace Daipan.Player.Scripts 
{
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