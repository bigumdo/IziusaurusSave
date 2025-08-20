using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YUI.Agents.Bosses;

namespace YUI.PatternModules
{
    [System.Serializable]
    public class PatternStep
    {
        [SerializeField] private List<PatternModule> selectModules;
        public bool isSequential = true;
        public float delay;

        public IEnumerator Execute(Boss boss)
        {
            if (isSequential)
            {
                //PatternManager.Instance.isExecutingPattern = true;
                foreach (PatternModule m in selectModules)
                {
                    boss.StartCoroutine(m.Execute());
                    yield return new WaitUntil(() => m.isComplete);
                    m.isComplete = false;
                }

                if (delay > 0)
                    yield return new WaitForSeconds(delay);
            }
            else
            {
                List<PatternModule> toRemove = new List<PatternModule>();
                foreach (PatternModule t in selectModules)
                {
                    boss.StartCoroutine((t.Execute()));
                }

                while (selectModules.Count > toRemove.Count)
                {
                    toRemove.Clear();
                    foreach (PatternModule t in selectModules)
                    {
                        if (t.isComplete)
                        {
                            toRemove.Add(t);
                        }
                    }

                    yield return null;
                }
                
                foreach(PatternModule m in toRemove)
                {
                    m.isComplete = false;
                }

                if (delay > 0)
                    yield return new WaitForSeconds(delay);
            }

        }
    }

    [System.Serializable]
    public class PatternRepeat
    {
        public bool shouldExecute = true;
        public List<PatternStep> patternSteps;
        public int repeatCnt;
    }

    [CreateAssetMenu(fileName = "PatternSO", menuName = "SO/Boss/PatternSO")]
    public class PatternSO : ScriptableObject
    {
        public float coolTime;
        public int weight;
        public List<PatternRepeat> patternRepeats;

        [SerializeField] private float _delay;
        public bool canPattern
        {
            get
            {
                if(_checkTime > 0)
                {
                    return _checkTime + coolTime < Time.time;
                }
                else
                    return true;
            }
        }

        private float _checkTime = 0;

        private void OnDisable()
        {
            _checkTime = 0;
        }

        public IEnumerator Execute(Boss boss)
        {
            PatternManager.Instance.isExecutingPattern = true;
            foreach (PatternRepeat pr in patternRepeats)
            {
                if (!pr.shouldExecute)
                    continue;

                if (pr.repeatCnt > 1)
                {
                    for (int i = 0; i < pr.repeatCnt; ++i)
                    {
                        foreach (PatternStep ps in pr.patternSteps)
                        {
                            yield return ps.Execute(boss);
                        }
                    }
                }
                else
                {
                    foreach (PatternStep ps in pr.patternSteps)
                    {
                        yield return ps.Execute(boss);
                    }
                }
            }

            yield return new WaitForSeconds(_delay);
            _checkTime = Time.time;
            PatternManager.Instance.isExecutingPattern = false;
        }
    }
}
