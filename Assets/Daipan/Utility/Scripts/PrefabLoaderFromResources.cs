using UnityEngine;
using Object = UnityEngine.Object;

namespace Daipan.Utility.Scripts
{
    // Other classes can be created by implementing IPrefabLoader, such as PrefabLoaderFromAssetBundle and PrefabLoaderFromStreamingAssets.
    public class PrefabLoaderFromResources<T> : IPrefabLoader<T> where T : Object
    {
        readonly string _folderPath;
        readonly string _prefabName;

        public PrefabLoaderFromResources(string folderPath, string prefabName)
        {
            _folderPath = folderPath;
            _prefabName = prefabName;
        }

        public T Load()
        {
            var result = Resources.Load<T>(_folderPath + "/" + _prefabName);
            if (result == null)
            {
                Debug.LogError(
                    $"Failed to load prefab. folderPath ={_folderPath + "/" + _prefabName} prefabName = {_prefabName}");
                return null;
            }

            return result;
        }
    }
}