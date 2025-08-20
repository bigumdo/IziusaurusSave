using System;
using System.Collections;
using UnityEngine;
using YUI.Agents.Bosses;

namespace YUI.PatternModules
{
    public abstract class PatternModule : ScriptableObject
    {
        [HideInInspector] public bool isComplete;
        public event Action OnComplete;

        protected Boss _boss;
        public abstract IEnumerator Execute();

        protected virtual void CompleteActionExecute()
        {
            isComplete = true;
            if(OnComplete != null)
            {
                OnComplete?.Invoke();
                isComplete = false;
            }
        }
    }
}
