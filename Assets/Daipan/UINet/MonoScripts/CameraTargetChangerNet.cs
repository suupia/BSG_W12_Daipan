#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;
using UnityEngine.Rendering.Universal;

namespace Daipan.UINet.MonoScripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraTargetChangerNet : MonoBehaviour
    {
        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper)
        {
            if (playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Streamer)
            {
                var cameraData = GetComponent<Camera>().GetUniversalAdditionalCameraData();
                GetComponent<Camera>().targetTexture = null;
            }
        }
    }
}