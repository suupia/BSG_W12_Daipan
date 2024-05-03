using UnityEngine;
using Object = UnityEngine.Object;

namespace Daipan.Utility.Scripts
{
    // Other classes can be created by implementing IPrefabLoader, such as PrefabLoaderFromAssetBundle and PrefabLoaderFromStreamingAssets.
    public class PrefabLoaderFromResources<T> : IPrefabLoader<T> where T : Object
    {
        readonly string _path;

        public PrefabLoaderFromResources(string path)
        {
            _path = path;
            
        }

        public T Load()
        {
            var result = Resources.Load<T>(_path);
            if (result == null)
            {
                Debug.LogError($"Failed to load prefab. Path = {_path}");
                return null;
            }

            return result;
        }
    }
}