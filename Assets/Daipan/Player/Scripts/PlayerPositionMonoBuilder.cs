#nullable enable
using Daipan.Player.MonoScripts;
using VContainer;

namespace Daipan.Player.Scripts
{
    public class PlayerPositionMonoBuilder
    {
        public PlayerPositionMonoBuilder(
            IContainerBuilder builder,
            PlayerPositionMono playerPositionMono
        )
        {
            var data = new PlayerSpawnPointData()
            {
                GetPlayerSpawnedPointX = () => playerPositionMono.playerSpawnedPoint,
                GetAttackEffectDespawnedPoint = () => playerPositionMono.attackEffectDespawnedPoint.position,
            };
            builder.RegisterInstance(data);
        }
    }
}