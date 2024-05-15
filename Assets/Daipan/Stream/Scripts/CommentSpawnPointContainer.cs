#nullable enable
using System.Linq;
using Daipan.Stream.MonoScripts;
using UnityEngine;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public class CommentSpawnPointContainer: IStartable
    {
        public Vector3 spawnPosition { get; private set; }
        public Vector3 despawnPosition { get; private set; }

        void IStartable.Start()
        {
            SetSpawnPositions();
        }

        void SetSpawnPositions()
        {
           var spawnPoints = Object.FindObjectsByType<CommentSpawnPointMono>(FindObjectsSortMode.None);
           if (spawnPoints == null)
           {
               Debug.LogWarning ("No CommentSpawnPointMono found");
               return;
           }
           spawnPosition = spawnPoints.FirstOrDefault( c => c.isSpawnPoint)?.transform.position ?? default;
           if(spawnPosition == default) Debug.LogWarning("No start point found");
           despawnPosition = spawnPoints.FirstOrDefault( c => !c.isSpawnPoint)?.transform.position ?? default;
           if (despawnPosition == default) Debug.LogWarning("No despawn point found");
        }
        
    }
}