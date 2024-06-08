#nullable enable
using System;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Scripts 
{
    public class PlayerParamData
    {
        public Func<PlayerColor> PlayerEnum { get; init; } = () => PlayerColor.None;
        public  Func<int> GetAttack { get; init; } = () => 10;
    }

    public class PlayerHpParamData
    {
        public Func<int> GetCurrentHp { get; init; } = () => 0;
        public Action<int> SetCurrentHp { get; init; } = (value) => { };
    }
}