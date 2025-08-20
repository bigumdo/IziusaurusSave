using System;
using Unity.Behavior;
using UnityEngine;
using YUI.Agents.Bosses;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossPhaseEnumChangeAction", story: "[Boss] [PhaseEnum]", category: "Action", id: "69639fd575f9cd58b28823ad4c8d8e64")]
public partial class BossPhaseEnumChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<Boss> Boss;
    [SerializeReference] public BlackboardVariable<BossStateEnum> PhaseEnum;

    protected override Status OnStart()
    {
        Boss.Value.CurrentPage = PhaseEnum.Value;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

