using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using YUI.Bullets.WordBullets;
using YUI.Cores;
using YUI.PatternModules;

namespace YUI.Agents.Bosses
{
    public class PatternManager : MonoSingleton<PatternManager>
    {
        [HideInInspector] public bool isExecutingPattern;

        [SerializeField] private WordBulletSpawner _leftWordBulletSpawner;
        [SerializeField] private WordBulletSpawner _rightWordBulletSpawner;
        private BossSO _bossSO;
        private Dictionary<string, List<PatternGroupSO>> _patternGroupDictionary;
        private Dictionary<string, List<string>> _patternDialogKeyList;
        private PatternGroupSO[] patternGroups;

        public void PattternSetting(BossSO bossSO)
        {
            _bossSO = bossSO;
            _patternGroupDictionary = new Dictionary<string, List<PatternGroupSO>>();
            _patternDialogKeyList = new Dictionary<string, List<string>>();
            _bossSO.patternDatas.ForEach(x => _patternGroupDictionary.Add(x.patternName, x.GetPatternGroup()));
            _bossSO.dialogDatas.ForEach(x => _patternDialogKeyList.Add(x.dialogName, x.bossDialogKeyList));
            ModuleInit();
        }

        private void ModuleInit()
        {
            
        }

        public PatternSO RandomPattern(string patternName)
        {
            List<PatternGroupSO> groups = _patternGroupDictionary[patternName];
            float totalWeight = groups.Where(g => g.isUnlockedPattern && g.canPattern).Sum(g => g.weight);

            if (totalWeight == 0)
            {
                return null;
            }
            float rand = Random.Range(0f, totalWeight);
            float sum = 0f;
            foreach (var g in groups)
            {
                sum += g.weight;
                if (rand <= sum && g.canPattern)
                {
                    return g.RandomSelectPattern();
                }
            }
            return null; // fallback
        }


        public List<string> GetDialog(string dialogName)
        {
            return _patternDialogKeyList[dialogName];
        }

        public void SetWordSpawner(bool value)
        {
            _leftWordBulletSpawner.SetShootable(value);
            _rightWordBulletSpawner.SetShootable(value);
        }
    }
}
