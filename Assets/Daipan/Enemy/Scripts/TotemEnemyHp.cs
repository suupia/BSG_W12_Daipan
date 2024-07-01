#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class TotemEnemyHp : IEnemyHp
    {
        // todo パラメータ化する？
        double _allowableSec = 0.1f;
        double _redLatestAttackedTime;
        double _blueLatestAttackedTime;
        double _yellowLatestAttackedTime;
        readonly IEnemyHp _enemyHp;
        readonly StreamTimer _streamTimer;

        public TotemEnemyHp(IEnemyHp enemyHp , StreamTimer streamTimer)
        {
            _enemyHp = enemyHp;
            _streamTimer = streamTimer; 
        }
        
        public int CurrentHp => _enemyHp.CurrentHp;

        public  void DecreaseHp(EnemyDamageArgs enemyDamageArgs)
        {
            switch (enemyDamageArgs.PlayerColor)
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
            

            if (!IsAttackable()) return;
            _enemyHp.DecreaseHp(enemyDamageArgs);
        }

        bool IsAttackable()
        {
            return (_streamTimer.CurrentTime - _redLatestAttackedTime < _allowableSec)
                & (_streamTimer.CurrentTime - _blueLatestAttackedTime < _allowableSec)
                & (_streamTimer.CurrentTime - _yellowLatestAttackedTime < _allowableSec);
        }
    }
}