#nullable enable
using System;

namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamDTO
    {
        public required Func<int> GetWAttack { get; init; }
        public required Func<int> GetAAttack { get; init; }
        public required Func<int> GetSAttack { get; init; }
    }
}