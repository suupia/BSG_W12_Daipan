#nullable enable
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Stream.MonoScripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts
{
    public sealed class CommentSpawnPointContainer : IStart
    {
        public bool _isInitialized;
        Vector3 _spawnPosition = new(1, 1, 1);

        [Inject]
        public CommentSpawnPointContainer()
        {
        }

        public Vector3 SpawnPosition
        {
            get
            {
                Debug.Log($"Spawn position requested: {_spawnPosition}");
                return _spawnPosition;
            }
            private set
            {
                Debug.Log($"Spawn position set to {value}");
                _spawnPosition = value;
            }
        }

        public Vector3 DespawnPosition { get; private set; }

        void IStart.Start()
        {
            SetSpawnPositions();
            _isInitialized = true;
            Debug.Log("");
        }

        void SetSpawnPositions()
        {
            var spawnPoints = Object.FindObjectsByType<CommentSpawnPointMono>(FindObjectsSortMode.None);
            if (spawnPoints == null)
            {
                Debug.LogWarning("No CommentSpawnPointMono found");
                return;
            }

            SpawnPosition = spawnPoints.FirstOrDefault(c => c.isSpawnPoint)?.transform.position ?? default;
            if (SpawnPosition == default) Debug.LogWarning("No start point found");
            DespawnPosition = spawnPoints.FirstOrDefault(c => !c.isSpawnPoint)?.transform.position ?? default;
            if (DespawnPosition == default) Debug.LogWarning("No despawn point found");

            Debug.Log($"Spawn position: {SpawnPosition}, Despawn position: {DespawnPosition}");
        }
    }
}