#nullable enable
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerHolder
    {
        IPlayerMono? _player;

        public IPlayerMono PlayerMono
        {
            get
            {
                if (_player == null)
                {
                    Debug.LogWarning($"_player is null");
                    return null!;
                }

                return _player;
            }
            set => _player = value;
        }
    }
}