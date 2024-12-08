#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;
using UnityEngine.Rendering.VirtualTexturing;
using Daipan.Transporter.Scripts;

namespace Daipan.UINet.MonoScripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraTargetChangerNet : MonoBehaviour
    {
        void Awake()
        {
            var playerDataTransporterNet = Object.FindObjectOfType<PlayerDataTransporterNet>();
            var runner = Object.FindObjectOfType<NetworkRunner>();
            if (playerDataTransporterNet.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Streamer)
            {
                GetComponent<Camera>().targetTexture = null;
            }
        }
    }
}