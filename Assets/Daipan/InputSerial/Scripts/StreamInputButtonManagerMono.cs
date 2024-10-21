#nullable enable
using Daipan.InputSerial.Interfaces;
using UnityEngine;

namespace Daipan.InputSerial.Scripts
{
    public class StreamInputButtonManagerMono : MonoBehaviour, IInputSerialManager
    {
        [SerializeField] CustomButton redButton = null!;
        [SerializeField] CustomButton blueButton = null!;
        [SerializeField] CustomButton yellowButton = null!;
        [SerializeField] CustomButton menuButton = null!;

        bool RedButton { get; set; }
        bool BlueButton { get; set; }
        bool YellowButton { get; set; }
        bool MenuButton { get; set; }

        public bool GetButtonRed() => RedButton;
        public bool GetButtonBlue() => BlueButton;
        public bool GetButtonYellow() => YellowButton;
        public bool GetButtonMenu() => MenuButton;

        void Start()
        {
            // onClickでボタンが押されたときに状態を更新
            redButton.onClick += () =>  RedButton = true;
            blueButton.onClick += () => BlueButton = true;
            yellowButton.onClick += () => YellowButton = true;
            menuButton.onClick += () => MenuButton = true;
        }

        void LateUpdate()
        {
            Debug.Log($"GetButtonRed: {RedButton}, blue: {BlueButton}, yellow: {YellowButton}, menu: {MenuButton}");
            // ボタンの状態を毎フレームリセット
            RedButton = false;
            BlueButton = false;
            YellowButton = false;
            MenuButton = false;
        }

    } 
}

