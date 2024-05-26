using Daipan.Comment.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts;

public class EnemySlowDefeatChecker
{
    readonly CommentSpawner _commentSpawner;
    readonly int _slowDefeatThreshold = 5; // todo : パラメータで設定できるようにする

    public EnemySlowDefeatChecker(CommentSpawner commentSpawner)
    {
        _commentSpawner = commentSpawner;
    }

    public float SlowDefeatCoordinate { get; } = 2;

    int SlowDefeatCounter { get; set; }


    public void IncrementSlowDefeatCounter()
    {
        SlowDefeatCounter++;
        if (SlowDefeatCounter > _slowDefeatThreshold)
        {
            Debug.Log("Spawn comment");
            _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
            SlowDefeatCounter = 0;
        }
    }
}