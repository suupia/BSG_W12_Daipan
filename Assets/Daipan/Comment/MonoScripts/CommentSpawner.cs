#nullable enable
using Daipan.Comment.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.MonoScripts
{
    public class CommentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject commentSection = null!; // [Prerequisite
        [SerializeField] GameObject commentPrefab = null!;

        IObjectResolver _container = null!;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
        }
    }
}