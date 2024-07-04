#nullable enable
using Daipan.Player.MonoScripts;
using VContainer;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerPositionMonoBuilder
    {
        public PlayerPositionMonoBuilder(
            IContainerBuilder builder,
            PlayerPositionMono playerPositionMono
        )
        {
            var playerSpawnPointData = new PlayerSpawnPointData()
            {
                GetPlayerSpawnedPointX = () => playerPositionMono.playerSpawnedPoint,
            };
            builder.RegisterInstance(playerSpawnPointData);
            var playerAttackEffectPointData = new PlayerAttackEffectPointData(playerPositionMono);
            builder.RegisterInstance(playerAttackEffectPointData);
        }
    }
}