#nullable enable
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Scripts;
using Daipan.Tower.MonoScripts;
using JetBrains.Annotations;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using VContainer;

public class TowerMono : MonoBehaviour
{
    [SerializeField] TowerViewMono? towerViewMono;
    TowerParamsConfig _towerParamsConfig = null!;
    PlayerHolder _playerHolder = null!;
    SpriteRenderer _spriteRenderer = null!;

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
        towerViewMono?.SetRatio(_playerHolder.PlayerMono.CurrentHp / (float)_playerHolder.PlayerMono.MaxHp);
        _spriteRenderer.sprite = _towerParamsConfig.GetCurrentSprite(_playerHolder.PlayerMono.CurrentHp);
    }


}
