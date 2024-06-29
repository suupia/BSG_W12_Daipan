#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class TotemEnemyHp : EnemyHp
    {
        // todo パラメータ化する？
        double _allowableSec = 0.1f;
        double _redLatestAttackedTime;
        double _blueLatestAttackedTime;
        double _yellowLatestAttackedTime;
        readonly StreamTimer _streamTimer;

        public TotemEnemyHp(int maxHp, EnemyMono enemyMono, EnemyCluster enemyCluster , StreamTimer streamTimer)
            : base(maxHp, enemyMono, enemyCluster)
        {
            _streamTimer = streamTimer; 
        }


        public override void DecreaseHp(EnemyDamageArgs enemyDamageArgs)
        {
            switch (enemyDamageArgs.playerColor)
            {
                case Player.MonoScripts.PlayerColor.Red:
                    _redLatestAttackedTime = _streamTimer.CurrentTime;
                    break;
                case Player.MonoScripts.PlayerColor.Blue:
                    _blueLatestAttackedTime = _streamTimer.CurrentTime;
                    break;
                case Player.MonoScripts.PlayerColor.Yellow:
                    _yellowLatestAttackedTime = _streamTimer.CurrentTime;
                    break;
                default:
                    break;
            }

            if (!isAttackable()) return;
            base.DecreaseHp(enemyDamageArgs);
        }

        bool isAttackable()
        {
            return (_streamTimer.CurrentTime - _redLatestAttackedTime < _allowableSec)
                & (_streamTimer.CurrentTime - _blueLatestAttackedTime < _allowableSec)
                & (_streamTimer.CurrentTime - _yellowLatestAttackedTime < _allowableSec);
        }
    }
}