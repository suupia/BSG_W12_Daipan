#nullable enable
using System.Collections.Generic;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectSpawner
    {
        readonly IPrefabLoader<PlayerAttackEffectMono> _effectLoader;
        readonly IPlayerAttackEffectBuilder _playerAttackEffectBuilder;
        
        public PlayerAttackEffectSpawner(
            IPrefabLoader<PlayerAttackEffectMono> effectLoader,
            IPlayerAttackEffectBuilder playerAttackEffectBuilder
            )
        {
            _effectLoader = effectLoader;
            _playerAttackEffectBuilder = playerAttackEffectBuilder;
        }

        public PlayerAttackEffectMono SpawnEffect(PlayerMono playerMono ,List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor, Vector3 position, Quaternion rotation)
        {
            var effectPrefab = _effectLoader.Load();
            var effectObject = Object.Instantiate(effectPrefab, position, rotation);
            return _playerAttackEffectBuilder.Build(effectObject, playerMono, playerViewMonos, playerColor);
        }
    }
}