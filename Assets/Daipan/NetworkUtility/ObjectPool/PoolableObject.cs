using Fusion;

namespace Daipan.NetworkUtility
{
    public abstract class PoolableObject : NetworkBehaviour
    {
        void OnDisable()
        {
            OnInactive();
        }

        protected abstract void OnInactive();
    }
}