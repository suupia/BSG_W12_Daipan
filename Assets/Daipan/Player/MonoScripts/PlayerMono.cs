using Stream.Player.Scripts;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour
{
    PlayerAttack _playerAttack;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _playerAttack.Attack();
    }

    // [Inject]を付けないと、VContainerからのInjectが行われないことに注意
    [Inject]
    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
    }
}