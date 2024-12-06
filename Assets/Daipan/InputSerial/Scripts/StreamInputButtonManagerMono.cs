#nullable enable
using Daipan.InputSerial.Interfaces;
using Daipan.Transporter;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.InputSerial.Scripts
{
    public class StreamInputButtonManagerMono : MonoBehaviour, IInputSerialManager
    {
        [SerializeField] GameObject viewObject = null!;
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

        [Inject]
        public void Initialize(
            NetworkRunner runner,
            PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            )
        {
            var isStreamer = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Streamer;
            viewObject.SetActive(isStreamer);
        }

        void Start()
        {
            // onClickでボタンが押されたときに状態を更新
            redButton.onClick += () => RedButton = true;
            blueButton.onClick += () => BlueButton = true;
            yellowButton.onClick += () => YellowButton = true;
            menuButton.onClick += () => MenuButton = true;

            redButton.onClick += () => Debug.Log("Red Button is Clicked");
            blueButton.onClick += () => Debug.Log("Blue Button is Clicked");
            yellowButton.onClick += () => Debug.Log("Yellow Button is Clicked");
            menuButton.onClick += () => Debug.Log("Menu Button is Clicked");
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKey(KeyCode.W)) RedButton = true;
            if (Input.GetKey(KeyCode.A)) YellowButton = true;
            if (Input.GetKey(KeyCode.S)) BlueButton = true;
        }
#endif
        
        void LateUpdate()
        {
            // Debug.Log($"GetButtonRed: {RedButton}, blue: {BlueButton}, yellow: {YellowButton}, menu: {MenuButton}");
            // ボタンの状態を毎フレームリセット
            RedButton = false;
            BlueButton = false;
            YellowButton = false;
            MenuButton = false;
        }

    }
}

