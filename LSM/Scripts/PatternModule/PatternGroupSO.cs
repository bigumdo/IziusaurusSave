using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YUI.PatternModules
{
    [CreateAssetMenu(fileName = "PatternGroupSO", menuName = "SO/Boss/PatternGroupSO")]
    public class PatternGroupSO : ScriptableObject, ICloneable
    {
        public bool canPattern
        {
            get
            {
                return _checkTime > 3 ? _checkTime + coolTime < Time.time : true;
            }
        }

        public bool isUnlockedPattern
        {
            get
            {
                return startCoolTime < Time.time;
            }
        }

        public List<PatternSO> patterns;
        public int weight;

        [SerializeField] private float coolTime;
        [SerializeField] private float startCoolTime;

        private float _checkTime = 0;

        private void OnEnable()
        {
            _checkTime = 0;
        }

        public PatternSO RandomSelectPattern()
        {
            float totalWeight = patterns.Where(g => g.canPattern).Sum(g => g.weight);
            if (totalWeight == 0)
            {
                return null;
            }
            float rand = Random.Range(0f, totalWeight);
            float sum = 0f;

            foreach (var g in patterns)
            {
                if (!g.canPattern)
                    continue;

                sum += g.weight;

                if (rand <= sum)
                {
                    _checkTime = Time.time;
                    return g;
                }
            }
            return null; // fallback
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
