#nullable enable
using System;

namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamDTO
    {
        public required Func<int> CurrentHp { get; init; } = () => 10;
    }
}