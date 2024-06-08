#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectSpawner
    {
        readonly IPrefabLoader<PlayerAttackEffectMono> _effectLoader;
        
        public PlayerAttackEffectSpawner(IPrefabLoader<PlayerAttackEffectMono> effectLoader)
        {
            _effectLoader = effectLoader;
        }

        public PlayerAttackEffectMono Spawn(Vector3 position, Quaternion rotation)
        {
            var effectPrefab = _effectLoader.Load();
            var effectObject = Object.Instantiate(effectPrefab, position, rotation);
            return effectObject;
        }
    }
}