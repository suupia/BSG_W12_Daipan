using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemySlowDefeatChecker
    {
        readonly CommentSpawner _commentSpawner;
        readonly EnemyDefeatConfig _enemyDefeatConfig;

        public EnemySlowDefeatChecker(
            EnemyDefeatConfig enemyDefeatConfig,
            CommentSpawner commentSpawner)
        {
            _enemyDefeatConfig = enemyDefeatConfig;
            _commentSpawner = commentSpawner;
        }

        int SlowDefeatThreshold => _enemyDefeatConfig.GetSlowDefeatThreshold();

        public float SlowDefeatCoordinate => _enemyDefeatConfig.GetEnemyDefeatSlowPosition().x;

        int SlowDefeatCounter { get; set; }


        public void IncrementSlowDefeatCounter()
        {
            SlowDefeatCounter++;
            if (SlowDefeatCounter > SlowDefeatThreshold)
            {
                Debug.Log("Spawn comment");
                _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                SlowDefeatCounter = 0;
            }
        }
    } 
}

