#nullable enable
using System;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.Player.MonoScripts
{
    public class AntiInputMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] CustomButton button1 = null!;
        [SerializeField] CustomButton button2 = null!;
        [SerializeField] CustomButton button3 = null!;
        

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
        )
        {
            var isStreamer = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Anti;
            viewObject.SetActive(isStreamer);
        }

        void Start()
        {
            button1.onClick += () =>  Debug.Log("Button1"); // todo : 敵を出現させる 
            button2.onClick += () => Debug.Log("Button2"); //
            button3.onClick += () => Debug.Log("Button3"); // 
        }
    } 
}

