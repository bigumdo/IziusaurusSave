using UnityEngine;
using YUI.Cores;

namespace YUI.Agents.Bosses
{
    public class BossHealth : AgentHealth
    {
        private Boss _boss;
        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _boss = agent as Boss;
        }

        public override void ApplyDamage(float damage)
        {
            if (!_boss.IsDamageable)
                return;

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            _boss.OnHitEvent?.Invoke();

            UIManager.Instance.UpdateBossHp();
            if (_currentHealth <= 0)
            {
                switch (_boss.CurrentPage)
                {
                    case BossStateEnum.Phase1:
                        _boss.CurrentPage = BossStateEnum.Phase2;
                        _boss.GetVariable<BossHit>("BossHit").Value.SendEventMessage(_boss, this);
                        UIManager.Instance.GetUI<CombatUI>().PauseUpdate((int)BossStateEnum.Phase2);
                        break;
                    case BossStateEnum.Phase2:
                        _boss.CurrentPage = BossStateEnum.FinalPhase;
                        _boss.GetVariable<BossHit>("BossHit").Value.SendEventMessage(_boss, this);
                        UIManager.Instance.GetUI<CombatUI>().PauseUpdate((int)BossStateEnum.FinalPhase);
                        break;
                    case BossStateEnum.FinalPhase:
                        if(_boss.IsDamageable)
                        {
                            _boss.SetDamageable(false);
                            SoundManager.Instance.PlaySound("SFX_Boss_DeadStart");
                            BossManager.Instance.BossDeadPlay();
                        }
                        break;
                }
            }
        }

        public void ResetHp()
        {
            _currentHealth = _maxHealth;
            UIManager.Instance.UpdateBossHp();
        }
    }
}
