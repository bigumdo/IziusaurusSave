using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YUI.PatternModules
{
    [Serializable]
    public struct BossPatternData
    {
        public string patternName;
        [SerializeField] private List<PatternGroupSO> patternGroups;

        public List<PatternGroupSO> GetPatternGroup()
        {
            return  patternGroups.Select(x => x.Clone() as PatternGroupSO).ToList();
        }
    }

    [Serializable]
    public struct BossDialogData
    {
        public string dialogName;
        public List<string> bossDialogKeyList;
    }

    [CreateAssetMenu(fileName = "BossSO", menuName = "SO/Boss/BossSO")]
    public class BossSO : ScriptableObject
    {
        public string bossName;
        public List<BossPatternData> patternDatas;
        public List<BossDialogData> dialogDatas;
    }
}
