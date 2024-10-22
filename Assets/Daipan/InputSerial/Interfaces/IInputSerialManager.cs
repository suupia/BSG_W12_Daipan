#nullable enable
namespace Daipan.InputSerial.Interfaces;

public interface IInputSerialManager
{
    public bool GetButtonRed();

    public bool GetButtonBlue();

    public bool GetButtonYellow();

    public bool GetButtonMenu();

}