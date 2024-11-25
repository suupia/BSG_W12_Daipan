#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Daipan.LevelDesign.Net
{
    public class NetworkParamsServer : ICostValueParam, IEnemySpawnedCostParam, IAntiCommentParam, IAntiStateParam
    {
        readonly NetworkPlayerParamsManager _playerParamsManager;

        [Inject]
        public NetworkParamsServer(NetworkPlayerParamsManager playerParamsManager)
        {
            _playerParamsManager = playerParamsManager;
        }

        public int NormalEnemyCost { get => _playerParamsManager.enemySpawnedCostParam.normalEnemyCost; }
        public int BossEnemyCost { get => _playerParamsManager.enemySpawnedCostParam.bossEnemyCost; }
        public int MaxCostValue { get => _playerParamsManager.costValueParam.maxCostValue; }
        public int InitializedCostValue { get => _playerParamsManager.costValueParam.initializedCostValue; }
        public int IncreaseCostTime { get => _playerParamsManager.costValueParam.increaseCostTime; }
        public int IncreasedCostValue { get => _playerParamsManager.costValueParam.increasedCostValue; }
        public int MaxCharacterNumber { get => _playerParamsManager.antiCommentParam.maxCharacterNumber; }
        public int BanCount { get => _playerParamsManager.antiCommentParam.banCount; }
        public int SpecialAntiCommentHp { get => _playerParamsManager.antiCommentParam.specialAntiCommentHp; }
        public float BanTime { get => _playerParamsManager.antiStateParam.banTime; }
        public float FeverTime { get => _playerParamsManager.antiStateParam.feverTime; }


    }
}
