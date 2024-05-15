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
        [Inject]
        public CommentSpawnPointContainer()
        {
        }

        public Vector3 SpawnPosition { get; private set; }

        public Vector3 DespawnPosition { get; private set; }

        void IStart.Start()
        {
            SetSpawnPositions();
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