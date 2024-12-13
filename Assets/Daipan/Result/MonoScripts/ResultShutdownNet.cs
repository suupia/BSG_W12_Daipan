#nullable enable
using Daipan.Transporter;
using Daipan.Transporter.Scripts;
using Fusion;
using UnityEngine;

namespace Daipan.Result.MonoScripts
{
    public class ResultShutdownNet : NetworkBehaviour
    {
        [Networked]
        [OnChangedRender(nameof(OnInResultPlayerChanged))]
        NetworkDictionary<PlayerRef, NetworkBool> InResultSceneDictionary => default;

        PlayerDataTransporterNet _playerDataTransporterNet = null!;
   
        public override void Spawned()
        {
            _playerDataTransporterNet = FindObjectOfType<PlayerDataTransporterNet>();
            InResultSceneDictionary.Set(Runner.LocalPlayer, true);
        }
   
        void OnInResultPlayerChanged()
        {
            // Check if the count matches
            var condition1 = InResultSceneDictionary.Count == _playerDataTransporterNet.GetPlayerCount();

            // Check if all values are true
            var condition2 = true;
            foreach (var kvp in InResultSceneDictionary)
            {
                if (!kvp.Value)
                {
                    condition2 = false;
                    break;
                }
            }

            if (condition1 && condition2)
            {
                Runner.Shutdown();
            }
        }

    } 
}

