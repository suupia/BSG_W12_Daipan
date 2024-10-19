#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;

namespace Daipan.Player.Interfaces;

public interface IPlayerMono : IMonoBehaviour
{
    public Hp Hp { get; }

    public void SetHpMax();
    public void OnAttacked(IEnemyParamData enemyParamData);

}