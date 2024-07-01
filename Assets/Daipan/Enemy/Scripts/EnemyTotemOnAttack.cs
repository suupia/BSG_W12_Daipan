#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyTotemOnAttack
    {
        // todo パラメータ化する？
        double _allowableSec = 0.1f;
        double _redLatestAttackedTime;
        double _blueLatestAttackedTime;
        double _yellowLatestAttackedTime;
        readonly StreamTimer _streamTimer;

        public EnemyTotemOnAttack(StreamTimer streamTimer)
        {
            _streamTimer = streamTimer; 
        }
        

        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData )
        {
            switch (playerParamData.PlayerEnum())
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

            if (!IsAttackable()) return hp;
            return PlayerAttackModule.Attack(hp, playerParamData); 
        }

        bool IsAttackable()
        {
            return (_streamTimer.CurrentTime - _redLatestAttackedTime < _allowableSec)
                   & (_streamTimer.CurrentTime - _blueLatestAttackedTime < _allowableSec)
                   & (_streamTimer.CurrentTime - _yellowLatestAttackedTime < _allowableSec);
        } 
    }
}