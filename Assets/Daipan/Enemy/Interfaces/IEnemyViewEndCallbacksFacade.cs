#nullable enable
using System;

namespace Daipan.Enemy.Interfaces;

public interface IEnemyViewEndCallbacksFacade
{
   public void SetOnDiedCallback(Action onDied);
   public void SetOnDaipanedCallback(Action onDaipaned);
   public void Died();
   public void Daipaned();
}