using System.Collections;
using System.Collections.Generic;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;

public class PlayerMono : MonoBehaviour
{
    PlayerAttack _playerAttack;
    
    [Inject]
    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAttack.Attack();
        } 
    }
}
