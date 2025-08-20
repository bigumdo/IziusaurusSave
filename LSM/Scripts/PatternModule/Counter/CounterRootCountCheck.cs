using System.Collections;
using UnityEngine;
using YUI.Agents;
using YUI.Agents.Bosses;
using YUI.Agents.players;

namespace YUI.PatternModules
{
    [CreateAssetMenu(fileName = "CounterRootCountCheck", menuName = "SO/Boss/Module/Counter/CounterRootCountCheck")]
    public class CounterRootCountCheck : PatternModule
    {
        [SerializeField] private int checkCnt;
        public override IEnumerator Execute()
        {
            if (BossManager.Instance.counterObjList.Count >= checkCnt)
            {
                PlayerManager.Instance.Player.GetCompo<AgentHealth>(true).ApplyDamage(99999999);
            }

            CompleteActionExecute();
            yield return null;
        }
    }
}
