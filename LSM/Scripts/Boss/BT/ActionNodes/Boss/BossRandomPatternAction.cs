using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using YUI.Agents.Bosses;
using YUI.PatternModules;
using System.Collections;
using YUI.Cores;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossRandomPattern", story: "[boss] [PatternName] RandomPattern", category: "Action", id: "cc179344cb30d07e102416167cba7a4e")]
public partial class BossRandomPatternAction : Action
{
    [SerializeReference] public BlackboardVariable<Boss> Boss;
    [SerializeReference] public BlackboardVariable<string> PatternName;

    private bool _isEnd;
    private PatternSO _pattern;

    protected override Status OnStart()
    {   
        _pattern = PatternManager.Instance.RandomPattern(PatternName.Value);
        Boss.Value.StartCoroutine(PatternExecute());
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_isEnd)
        {
            _isEnd = false;
            return Status.Success;
        }
        return Status.Running;
    }

    private IEnumerator PatternExecute()
    {
        yield return new WaitUntil(() => !GameManager.Instance.IsGmaeStop);
        if (_pattern == null)
        {
            yield return new WaitForSeconds(3f);
            _isEnd = true;
        }
        else
        {
            yield return _pattern.Execute(Boss.Value);
            if (Boss.Value.CurrentPage == BossStateEnum.FinalPhase)
                yield return Boss.Value.GetCompo<BossRenderer>().Cracked();
            _isEnd = true;
        }
    }
}

