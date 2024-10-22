#nullable enable
using System;
using Daipan.Player.Interfaces;
using UnityEngine;

namespace Daipan.Player.MonoScripts
{
    public class StreamInputEnterMono : MonoBehaviour, IGetEnterKey
    {
        [SerializeField] CustomButton enterButton = null!;
        bool EnterButton { get; set; }

        public bool GetEnterKeyDown() => EnterButton;
        
        void Start()
        {
            // onClickでボタンが押されたときに状態を更新
            enterButton.onClick += () =>  EnterButton = true;
        }

        void LateUpdate()
        {
            // Debug.Log($"GetEnterKeyDown: {EnterButton}");
            // ボタンの状態を毎フレームリセット
            EnterButton = false; 
        }
    } 
}

