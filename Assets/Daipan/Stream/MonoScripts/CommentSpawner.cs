#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Stream.MonoScripts
{
    public class CommentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject commentSection = null!; // [Prerequisite
        [SerializeField] GameObject commentPrefab = null!;

        [SerializeField] float commentSpeed = 0.01f;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;

        IObjectResolver _container = null!;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(
                    $"_commentSpawnPointConatiner._isInitialized: {_commentSpawnPointContainer._isInitialized}"); // [Prerequisite
                Debug.Log(
                    $"spawn position: {_commentSpawnPointContainer.SpawnPosition}, despawn position: {_commentSpawnPointContainer.DespawnPosition}");
                _container.Instantiate(commentPrefab, _commentSpawnPointContainer.SpawnPosition, Quaternion.identity,
                    commentSection.transform);
            }
        }

        [Inject]
        public void Initialized(
            IObjectResolver container,
            CommentSpawnPointContainer commentSpawnPointContainer)
        {
            _container = container;
            _commentSpawnPointContainer = commentSpawnPointContainer;
            Debug.Log("Initialized CommentSpawner");
        }
    }
}