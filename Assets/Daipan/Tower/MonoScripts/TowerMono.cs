using System.Collections;
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(SpriteRenderer))]
public class TowerMono : MonoBehaviour
{
    TowerParamsConfig _towerParamsConfig = null!;
    PlayerHolder _playerHolder = null!;
    SpriteRenderer _spriteRenderer = null;

    [Inject]
    public void Initialize(
        TowerParamsConfig towerParamsConfig,
        PlayerHolder playerHolder)
    {
        _towerParamsConfig = towerParamsConfig;
        _playerHolder = playerHolder;
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        _spriteRenderer.sprite = _towerParamsConfig.GetCurrentSprite(_playerHolder.PlayerMono.CurrentHp);
    }


}
