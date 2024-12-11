#nullable enable
using System;

namespace Daipan.Enemy.Interfaces;

public interface IEnemyViewEndCallbacks
{
    public void Died(Action onDied);
    public void Daipaned(Action onDaipaned); 
}