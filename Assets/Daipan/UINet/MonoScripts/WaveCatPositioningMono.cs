#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;
using Daipan.Transporter.Scripts;

namespace Daipan.UINet.MonoScripts
{
    public class WaveCatPositioningMono : MonoBehaviour
    {
        [SerializeField] GameObject cat = null!;
        [SerializeField] GameObject progressBar = null!;
        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
        )
        {
            if (playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                SetUp();
            }
        }

        void SetUp()
        {
            var barTransform = progressBar.GetComponent<RectTransform>();
            barTransform.anchoredPosition = new Vector2(335f, -462f);
            barTransform.rotation = Quaternion.identity;
            barTransform.localScale = Vector3.one;

            cat.transform.position = new Vector3(6.6f, -3.3f, 0f);
        }
    }
}