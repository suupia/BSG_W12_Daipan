#nullable enable
namespace Daipan.Enemy.Scripts
{
    public class FinalBossDefeatTracker
    {
        public bool IsFinalBossDefeated { get; private set; }
        
        public void SetFinalBossDefeated()
        {
            IsFinalBossDefeated = true;
        }
    } 
}

