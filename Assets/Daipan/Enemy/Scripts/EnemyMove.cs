#nullable enable
using Daipan.Enemy.Interfaces;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemyMove
    {
        readonly Transform _transform;

        public EnemyMove(Transform transform)
        {
            _transform = transform;
        }

        public bool MoveUpdate(Transform targetTransform, IEnemyParamData enemyParamData, IEnemyViewMono? enemyViewMono)
        {
            // 攻撃範囲よりプレイヤーとの距離が大きいときだけ動く
            bool isReachedPlayer;
            if (_transform.position.x - targetTransform.position.x >=
                enemyParamData.GetAttackRange())
            {
                var moveSpeed = (float)enemyParamData.GetMoveSpeedPerSec();
                _transform.position += Time.deltaTime * moveSpeed * Vector3.left;
                enemyViewMono?.Move();
                isReachedPlayer = false;
            }
            else
            {
                isReachedPlayer = true;
            }

            return isReachedPlayer;
        }
    }
}