#nullable enable
using UnityEngine;

namespace Daipan.Utility
{
    public interface IPrefabLoader<out T> where T : Object
    {
        T Load();
    }
}