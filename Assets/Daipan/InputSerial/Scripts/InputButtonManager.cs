#nullable enable
using Daipan.InputSerial.Interfaces;
using UnityEngine;

namespace Daipan.InputSerial.Scripts;

public class InputButtonManager : MonoBehaviour, IInputSerialManager
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
        // Add listeners for button events if needed
        redButton.onPointerDown += () => RedButton = true;
        redButton.onPointerUp += () => RedButton = false;

        blueButton.onPointerDown += () => BlueButton = true;
        blueButton.onPointerUp += () => BlueButton = false;

        yellowButton.onPointerDown += () => YellowButton = true;
        yellowButton.onPointerUp += () => YellowButton = false;

        menuButton.onPointerDown += () => MenuButton = true;
        menuButton.onPointerUp += () => MenuButton = false;
    }
}