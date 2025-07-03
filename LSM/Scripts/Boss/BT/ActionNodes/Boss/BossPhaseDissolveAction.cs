using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using YUI.Agents.Bosses;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossPhaseDissolve", story: "[Boss] PhaseDissolve [Duration]", category: "Action", id: "d15778d1289a48298ec60b85a67e0c12")]
public partial class BossPhaseDissolveAction : Action
{
    [SerializeReference] public BlackboardVariable<Boss> Boss;
    [SerializeReference] public BlackboardVariable<float> Duration;

    private bool _isFinished;

    protected override Status OnStart()
    {
        Boss.Value.StartCoroutine(Dissolve());
        return Status.Running;
    }

    private IEnumerator Dissolve()
    {
        float time = 0;
        _isFinished = false;
        SpriteRenderer renderer = Boss.Value.GetCompo<BossRenderer>().SpriteRenderer;
        switch (Boss.Value.CurrentPage)
        {
            case BossStateEnum.Phase2:
                {
                    while(time <= Duration.Value)
                    {
                        time += Time.deltaTime;
                        renderer.material.SetFloat("_PhaseDissolveValue", Mathf.Lerp(0, 0.5f, time / Duration.Value));
                        Debug.Log(time / Duration.Value);
                        yield return null;
                    }
                    _isFinished = true;
                }
                break;
            case BossStateEnum.FinalPhase:
                {
                    while (time <= Duration.Value)
                    {
                        time += Time.deltaTime;
                        renderer.material.SetFloat("_PhaseDissolveValue", Mathf.Lerp(0.5f,1,time/Duration.Value));
                        yield return null;
                    }
                    _isFinished = true;
                }
                break;
        }
    }

    protected override Status OnUpdate()
    {
        if(_isFinished)
            return Status.Success;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

