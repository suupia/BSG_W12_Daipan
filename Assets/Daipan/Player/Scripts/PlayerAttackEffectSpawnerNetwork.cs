#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectSpawnerNetwork
    {
        readonly IPrefabLoader<PlayerAttackEffectNet> _effectLoader;
        readonly IPlayerAttackEffectBuilder _playerAttackEffectBuilder;
        
        public PlayerAttackEffectSpawnerNetwork(
            IPrefabLoader<PlayerAttackEffectNet> effectLoader,
            IPlayerAttackEffectBuilder playerAttackEffectBuilder
            )
        {
            _effectLoader = effectLoader;
            _playerAttackEffectBuilder = playerAttackEffectBuilder;
        }

        public IPlayerAttackEffectMono SpawnEffect(IMonoBehaviour playerMono ,List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor, Vector3 position, Quaternion rotation)
        {
            var effectPrefab = _effectLoader.Load();
            var effectObject = Object.Instantiate(effectPrefab, position, rotation);
            return _playerAttackEffectBuilder.Build(effectObject, playerMono, playerViewMonos, playerColor);
        }
    }
}