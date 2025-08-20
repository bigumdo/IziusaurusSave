using UnityEngine;
using YUI.PatternModules;

namespace YUI.Agents.Bosses
{
    public class TutorialBoss : Boss
    {
        private void Start()
        {
            BossManager.Instance.SetBoss(this);
            StartBT();
        }
    }
}
