#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Scripts;

public class EnemyQuickDefeatChecker
{
    
    readonly float _quickDefeatCoordinate = 2; // todo : パラメータで設定できるようにする
    
    public bool IsQuickDefeat(Vector3 currentPosition)
    {
        return _quickDefeatCoordinate < currentPosition.x;
    }
}