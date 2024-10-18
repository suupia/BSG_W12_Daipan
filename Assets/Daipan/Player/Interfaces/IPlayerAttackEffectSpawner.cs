#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Interfaces;

public interface IPlayerAttackEffectSpawner
{
    public IPlayerAttackEffectMono SpawnEffect(IMonoBehaviour playerMono, List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor, Vector3 position, Quaternion rotation);

}