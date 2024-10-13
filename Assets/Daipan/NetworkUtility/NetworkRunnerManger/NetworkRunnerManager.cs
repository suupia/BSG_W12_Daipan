using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Daipan.NetworkUtility
{
    // 全てのシーンにこれを配置しておけば、NetworkRunnerを使える
    // シーン上にNetworkRunnerがないならインスタンス化し、runner.StartGame()を実行
    public class NetworkRunnerManager : MonoBehaviour
    {
        [SerializeField] NetworkRunner networkRunner;
        [SerializeField] NetworkSceneManagerDefault networkSceneManagerDefault;
        public NetworkRunner Runner { get; private set; }

        public async UniTask AttemptStartScene(string sessionName = default,
            GameMode gameMode = GameMode.AutoHostOrClient)
        {
            sessionName ??= RandomString(5);
            Runner = FindObjectOfType<NetworkRunner>();
            if (Runner == null)
            {
                // Set up NetworkRunner
                Runner = Instantiate(networkRunner);
                DontDestroyOnLoad(Runner);
                // 以下はCarryBlockのみ必要？
                // var inputActionMap = InputActionMapLoader.GetInputActionMap(InputActionMapLoader.ActionMapName.Default);
                // Runner.AddCallbacks(new NetworkLocalInputPoller(inputActionMap));

                // Set up SceneMangerDefault
                var sceneMangerDefault = Instantiate(networkSceneManagerDefault);
                DontDestroyOnLoad(sceneMangerDefault);

                await Runner.StartGame(new StartGameArgs
                {
                    GameMode = gameMode,
                    SessionName = sessionName,
                    Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
                    SceneManager = sceneMangerDefault,
                });

            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Create random char
        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[length];
            for (var i = 0; i < length; i++) result[i] = chars[random.Next(chars.Length)];

            return new string(result);
        }
    }
}